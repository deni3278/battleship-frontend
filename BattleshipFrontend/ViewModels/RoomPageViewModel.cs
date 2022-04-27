using System.Diagnostics;
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

        public DelegateCommand ReadyCommand { get; }

        public RoomPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            ReadyCommand = new DelegateCommand(OnReady);
        }

        private void OnReady()
        {
            Debug.WriteLine("Ready.");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            // TODO: Notify.
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            // TODO: Set relevant properties and register handlers.
        }
    }
}