using System.Threading.Tasks;
using System.Windows.Input;
using KBCFoodAndGo.Interfaces.Services;
using Xamarin.Forms;

namespace KBCFoodAndGo.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public MenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public ICommand LogOutCommand => new Command(async () => await NavigateToLoginView());

        private async Task NavigateToLoginView()
        {
            await _navigationService.NavigateToAsync<LoginViewModel>();
        }
    }
}
