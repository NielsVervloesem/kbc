using KBCFoodAndGo.Interfaces.Services;
using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KBCFoodAndGo.ViewModels
{
    public class ManageViewModel : ViewModelBase
    {
        private readonly IMealDataService _mealDataService;
        private ObservableCollection<Meal> _meals;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        private bool _showAddOptions;

        public bool ShowAddOptions
        {
            get => _showAddOptions;
            set
            {
                _showAddOptions = value;
                OnPropertyChanged();
            }
        }

        public Command ShowOptions => new Command(OnShowOptions);
        public Command AddMealCommand => new Command(async () => await NavigateToMealCreateViewModel());
        public Command AddMenuCommand => new Command(async () => await NavigateToCreateMenu());

        public Command OnDelete => new Command<Meal>(async (Meal meal) =>
        {
            await DeleteMeal(meal);
        });

        private async Task NavigateToCreateMenu()
        {
            ShowAddOptions = false;
            MessagingCenter.Instance.Send(this, "sendMealList", _meals);
            await _navigationService.NavigateToAsync<MenuCreateViewModel>();
        }

        public Command SelectionChanged => new Command<Meal>(async (Meal meal) =>
        {
            await OnSelectionChange(meal);
        });
        private async Task NavigateToMealCreateViewModel()
        {
            ShowAddOptions = false;
            await _navigationService.NavigateToAsync<MealCreateViewModel>();
        }

        private async Task DeleteMeal(Meal meal)
        {
            if (await _dialogService.ShowDialogTwoButtons("Bent u zeker dat u dit item wilt verwijderen?", "Verwijderen", "Ja", "Nee"))
            {
                _meals.Remove(meal);
                await _mealDataService.DeleteMeal(meal.Id);
            }
        }

        public ManageViewModel(IMealDataService mealDataService, INavigationService navigationService, IDialogService dialogService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            _mealDataService = mealDataService;
            ICommand setupCommand = new Command(async () => await GetMeals());
            setupCommand.Execute(null);
            SubscribeMessages();
        }
        private void SubscribeMessages()
        {
            MessagingCenter.Instance.Subscribe<MealCreateViewModel, bool>(this, "reloadList",
                (sender, value) =>
                {
                    if (value.Equals(true))
                    {
                        _ = GetMeals();
                    }
                });
            MessagingCenter.Instance.Subscribe<MealEditViewModel, bool>(this, "reloadList",
                (sender, value) =>
                {
                    if (value.Equals(true))
                    {
                        _ = GetMeals();
                    }
                });
        }
        private async Task OnSelectionChange(Meal meal)
        {
            await _navigationService.NavigateToAsync<MealDetailViewModel>();
            MessagingCenter.Instance.Send(this, "sendMeal", meal.Id);
        }
        private void OnShowOptions()
        {
            ShowAddOptions = !ShowAddOptions;
        }

        public async Task GetMeals()
        {
            Meals = new ObservableCollection<Meal>((await _mealDataService.GetAllMeals()).ToList());
        }
        public ObservableCollection<Meal> Meals
        {
            get => _meals;
            set
            {
                _meals = value;
                OnPropertyChanged();
            }
        }
    }
}
