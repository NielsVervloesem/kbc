using KBCFoodAndGoResto.Interfaces;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KBCFoodAndGoResto.ViewModels
{
    public class ScanViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        public ScanViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand ScanCommand => new Command(async () => await MakeScan());

        private async Task MakeScan()
        {
            await _navigationService.NavigateToAsync<ScanResultViewModel>();
        }
    }
}
