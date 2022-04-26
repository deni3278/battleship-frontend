using BattleshipFrontend.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Prism.Navigation;

namespace BattleshipFrontend.ViewModels
{
    public class RoomPageViewModel : INavigationAware
    {
        private Room _room;
        
        public async void OnNavigatedFrom(INavigationParameters parameters)
        {
            await App.HubConnection.InvokeAsync("LeaveRoom");
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _room = (Room)parameters["room"];
        }
    }
}