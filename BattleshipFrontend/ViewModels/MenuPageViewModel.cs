using System.Collections.ObjectModel;
using System.Diagnostics;
using BattleshipFrontend.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace BattleshipFrontend.ViewModels
{
    public class MenuPageViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

        private Room? _selectedRoom;

        public ObservableCollection<Room> Rooms { get; }

        public Room? SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                JoinCommand.RaiseCanExecuteChanged();

                Debug.WriteLine("Selected room with name '" + value?.Name + "'.");
            }
        }

        public DelegateCommand JoinCommand { get; }
        public DelegateCommand RefreshCommand { get; }

        public MenuPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            Rooms = new ObservableCollection<Room>();
            JoinCommand = new DelegateCommand(OnJoinAsync, () => SelectedRoom != null);
            RefreshCommand = new DelegateCommand(OnRefreshAsync);
        }

        private async void OnJoinAsync()
        {
            Debug.WriteLine("Joining room with name '" + SelectedRoom!.Name + "'.");

            var room = await App.HubConnection.InvokeAsync<Room?>("JoinRoom", SelectedRoom.Name);

            if (room == null)
            {
                RefreshCommand.Execute();
                
                await _dialogService.DisplayAlertAsync("", "That room doesn't exist anymore.", "Ok");
            }
            else
            {
                await _navigationService.NavigateAsync("RoomPage", new NavigationParameters
                {
                    { "room", room }
                });
            }
        }

        private async void OnRefreshAsync()
        {
            Debug.WriteLine("Refreshing list of rooms.");

            Rooms.Clear();

            var rooms = await App.HubConnection.InvokeAsync<Room[]>("GetRooms");

            foreach (var room in rooms)
            {
                Rooms.Add(room);
            }
        }
    }
}