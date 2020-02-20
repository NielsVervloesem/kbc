using KBCFoodAndGo.Interfaces.Services;
using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KBCFoodAndGo.ViewModels
{
    public class MealDetailViewModel : ViewModelBase
    {
        private Meal _meal;
        private readonly INavigationService _navigationService;

        public MealDetailViewModel(INavigationService navigationService, IMealDataService mealDataService)
        {
            _navigationService = navigationService;
            MessagingCenter.Instance.Subscribe<ManageViewModel, long>(this, "sendMeal",
                async (sender, id) =>
                {
                    Meal = await (mealDataService.GetMealById(id));
                });
        }
        public ICommand NavigateToCommand => new Command(async () => await NavigateToMealEditViewModel());

        private async Task NavigateToMealEditViewModel()
        {
            await _navigationService.NavigateToAsync<MealEditViewModel>();
            MessagingCenter.Instance.Send(this, "sendMeal", _meal);

        }
        public Meal Meal
        {
            get => _meal;
            set
            {
                if (_meal == value) return;
                _meal = value;
                OnPropertyChanged();
            }
        }
    }
}
