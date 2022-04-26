using System.Diagnostics;
using BattleshipFrontend.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace BattleshipFrontend.ViewModels
{
    public class CreateRoomPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        
        private bool _isRoomNameEnabled = true;
        private bool _isCreateEnabled = true;
        private bool _isErrorVisible;

        public string RoomName { get; set; } = string.Empty;

        public bool IsRoomNameEnabled
        {
            get => _isRoomNameEnabled;
            set => _isRoomNameEnabled = value;
        }

        public bool IsCreateEnabled
        {
            get => _isCreateEnabled;
            set => _isCreateEnabled = value;
        }

        public bool IsErrorVisible
        {
            get => _isErrorVisible;
            set => SetProperty(ref _isErrorVisible, value);
        }

        public DelegateCommand CreateCommand { get; }

        public CreateRoomPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            
            CreateCommand = new DelegateCommand(OnCreateAsync);
        }

        private async void OnCreateAsync()
        {
            Debug.WriteLine("Attempting to create a room.");
            
            IsRoomNameEnabled = false;
            IsCreateEnabled = false;

            var room = await App.HubConnection.InvokeAsync<Room?>("CreateRoom", RoomName);

            if (room != null)
            {
                Debug.WriteLine("Created a room with name '" + RoomName + "'.");

                await _navigationService.NavigateAsync("RoomPage", new NavigationParameters
                {
                    {"room", room}
                });
                
                return;
            }
            
            IsRoomNameEnabled = true;
            IsCreateEnabled = true;
            IsErrorVisible = true;
        }
    }
}