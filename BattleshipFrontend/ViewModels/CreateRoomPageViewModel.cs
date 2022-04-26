using Prism.Commands;
using Prism.Mvvm;

namespace BattleshipFrontend.ViewModels
{
    public class CreateRoomPageViewModel : BindableBase
    {
        private bool _isRoomNameEnabled;
        private bool _isCreateEnabled;
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

        public CreateRoomPageViewModel()
        {
            CreateCommand = new DelegateCommand(OnCreateAsync);
        }

        private async void OnCreateAsync()
        {
            /*
            
            IsRoomNameEnabled = false;
            IsCreateEnabled = false;
            
            IsRoomNameEnabled = true;
            IsCreateEnabled = true;
            IsErrorVisible = true;
            
             */
        }
    }
}