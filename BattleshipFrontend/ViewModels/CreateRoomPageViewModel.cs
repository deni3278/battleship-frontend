using System.Diagnostics;
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
            set => SetProperty(ref _isRoomNameEnabled, value);
        }

        public bool IsCreateEnabled
        {
            get => _isCreateEnabled;
            set => SetProperty(ref _isCreateEnabled, value);
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
            
            // TODO: Attempt to create a room.

            IsRoomNameEnabled = true;
            IsCreateEnabled = true;
            IsErrorVisible = true;
        }
    }
}