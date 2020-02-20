using KBCFoodAndGo.Interfaces.Services;
using KBCFoodAndGo.Shared.Interfaces.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KBCFoodAndGo.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public HomeViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
        }
        public ICommand LogOutCommand => new Command(async () => await NavigateToLoginView());

        private async Task NavigateToLoginView()
        {
            if (await _dialogService.ShowDialogTwoButtons("Bent u zeker dat u wilt afmelden?", "Afmelden", "Ja", "Nee"))
            {
                await _navigationService.NavigateToAsync<LoginViewModel>();
            }
        }
    }
}
