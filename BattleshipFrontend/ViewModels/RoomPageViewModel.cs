using System.Diagnostics;
using BattleshipFrontend.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace BattleshipFrontend.ViewModels
{
    public class RoomPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        
        private string _owner = string.Empty;
        private string _opponent = string.Empty;
        private string _readyText = "Ready";
        private bool isReady = false;

        public string Owner
        {
            get => _owner;
            set => SetProperty(ref _owner, value);
        }

        public string Opponent
        {
            get => _opponent;
            set => SetProperty(ref _opponent, value);
        }

        public string ReadyText
        {
            get => _readyText;
            set => SetProperty(ref _readyText, value);
        }

        public DelegateCommand ReadyCommand { get; }

        public RoomPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            ReadyCommand = new DelegateCommand(OnReady);
        }

        private async void OnReady()
        {
            if (!isReady)
            {
                Debug.WriteLine("Ready.");
                
                await App.HubConnection.InvokeAsync("Ready");

                isReady = true;
                ReadyText = "Unready";
            }
            else
            {
                Debug.WriteLine("Unready.");
                
                await App.HubConnection.InvokeAsync("Unready");

                isReady = false;
                ReadyText = "Ready";
            }
        }

        private async void OnStart()
        {
            Debug.WriteLine("Starting.");

            await _navigationService.NavigateAsync("/PreparationPage");
        }

        private async void OnRefresh(Room room)
        {
            Debug.WriteLine("Refresh room.");
            
            Owner = room.Owner.DisplayName;
            Opponent = room.Opponent?.DisplayName ?? string.Empty;

            if (room.Opponent != null) return;
            
            Debug.WriteLine("Unready.");
                
            await App.HubConnection.InvokeAsync("Unready");

            isReady = false;
            ReadyText = "Ready";
        }

        private async void OnOwnerLeft()
        {
            Debug.WriteLine("Owner left room.");
            
            await _dialogService.DisplayAlertAsync("", "Room owner has left.", "Ok");
            await _navigationService.GoBackAsync();
        }

        public async void OnNavigatedFrom(INavigationParameters parameters)
        {
            Debug.WriteLine("Leaving room.");

            await App.HubConnection.InvokeAsync("LeaveRoom");
            
            App.HubConnection.Remove("Refresh");
            App.HubConnection.Remove("OwnerLeft");
            App.HubConnection.Remove("Start");
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            App.HubConnection.On<Room>("Refresh", OnRefresh);
            App.HubConnection.On("OwnerLeft", OnOwnerLeft);
            App.HubConnection.On("Start", OnStart);
            
            var room = (Room)parameters["room"];
            
            OnRefresh(room);
        }
    }
}