using Prism.Mvvm;
using Prism.Navigation;

namespace BattleshipFrontend.ViewModels
{
    public class DisplayNamePageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public DisplayNamePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}