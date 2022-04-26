using System.Collections.ObjectModel;
using System.Diagnostics;
using BattleshipFrontend.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Prism.Commands;

namespace BattleshipFrontend.ViewModels
{
    public class MenuPageViewModel
    {
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

        public MenuPageViewModel()
        {
            Rooms = new ObservableCollection<Room>();
            JoinCommand = new DelegateCommand(OnJoinAsync, () => SelectedRoom != null);
            RefreshCommand = new DelegateCommand(OnRefreshAsync);
        }

        private void OnJoinAsync()
        {
            Debug.WriteLine("Joining game.");
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