using BattleshipFrontend.Services;
using BattleshipFrontend.ViewModels;
using BattleshipFrontend.Views;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace BattleshipFrontend
{
    public partial class App
    {
        public static readonly HubConnection HubConnection = new HubConnectionBuilder().WithUrl(
                "http://10.0.2.2:5070/hubs/battleship",
                HttpTransportType.WebSockets,
                options => options.SkipNegotiation = true)
            .Build();

        public App() : this(null)
        {
        }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("/SplashPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<DatabaseService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SplashPage, SplashPageViewModel>();
            containerRegistry.RegisterForNavigation<DisplayNamePage, DisplayNamePageViewModel>();
            containerRegistry.RegisterForNavigation<MenuPage, MenuPageViewModel>();
            containerRegistry.RegisterForNavigation<CreateRoomPage, CreateRoomPageViewModel>();
        }
    }
}