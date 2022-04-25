using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using BattleshipFrontend.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace BattleshipFrontend.ViewModels
{
    public class SplashPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly DatabaseService _databaseService;

        private bool _isRetryVisible;
        private string _message = string.Empty;

        public bool IsRetryVisible
        {
            get => _isRetryVisible;
            set => SetProperty(ref _isRetryVisible, value);
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public DelegateCommand AppearingCommand { get; }
        public DelegateCommand RetryCommand { get; }

        public SplashPageViewModel(INavigationService navigationService, DatabaseService databaseService)
        {
            _navigationService = navigationService;
            _databaseService = databaseService;
            
            AppearingCommand = new DelegateCommand(OnAppearingAsync);
            RetryCommand = new DelegateCommand(OnRetryAsync);
        }

        private async void OnAppearingAsync()
        {
            Debug.WriteLine("\nInitializing database.");

            IsRetryVisible = false;
            Message = "Initializing...";
            
            await _databaseService.InitializeTablesAsync();

            var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            try
            {
                Debug.WriteLine("\nEstablishing server connection.");

                await App.HubConnection.StartAsync(tokenSource.Token);

                App.HubConnection.Closed += OnClosedAsync;

                await _navigationService.NavigateAsync("/DisplayNamePage");
            }
            catch
            {
                IsRetryVisible = true;
                Message = "Couldn't establish a connection to the server.";
            }
            finally
            {
                tokenSource.Dispose();
            }
        }

        private async void OnRetryAsync()
        {
            await OnClosedAsync();
        }

        private Task OnClosedAsync(Exception exception = null)
        {
            Debug.WriteLine("\nRestarting application.");
            
            App.HubConnection.Closed -= OnClosedAsync;

            return Task.Run(() => App.Current.Dispatcher.BeginInvokeOnMainThread(() => _navigationService.NavigateAsync("/SplashPage")));
        }
    }
}