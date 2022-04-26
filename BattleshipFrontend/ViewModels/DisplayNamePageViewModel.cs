using BattleshipFrontend.Services;
using Microsoft.AspNetCore.SignalR.Client;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace BattleshipFrontend.ViewModels
{
    public class DisplayNamePageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly DatabaseService _databaseService;

        private bool _isConfirmEnabled;
        private string _displayName = string.Empty;
        private bool _isDisplayNameEnabled;

        public string DisplayName
        {
            get => _displayName;
            set
            {
                SetProperty(ref _displayName, value);
                IsConfirmEnabled = value.Length > 3;
            }
        }

        public bool IsDisplayNameEnabled
        {
            get => _isDisplayNameEnabled;
            set => SetProperty(ref _isDisplayNameEnabled, value);
        }

        public bool IsConfirmEnabled
        {
            get => _isConfirmEnabled;
            set => SetProperty( ref _isConfirmEnabled, value);
        }

        public DelegateCommand AppearingCommand { get; }
        public DelegateCommand ConfirmCommand { get; }

        public DisplayNamePageViewModel(INavigationService navigationService, DatabaseService databaseService)
        {
            _navigationService = navigationService;
            _databaseService = databaseService;

            AppearingCommand = new DelegateCommand(OnAppearingAsync);
            ConfirmCommand = new DelegateCommand(OnConfirmAsync);
        }

        private async void OnAppearingAsync()
        {
            DisplayName = await _databaseService.GetDisplayNameAsync();
            IsDisplayNameEnabled = true;
        }

        private async void OnConfirmAsync()
        {
            IsDisplayNameEnabled = false;
            IsConfirmEnabled = false;
            
            await _databaseService.SetDisplayNameAsync(DisplayName);
            await App.HubConnection.InvokeAsync("SetDisplayName", DisplayName);
            await _navigationService.NavigateAsync("/MenuPage");
        }
    }
}