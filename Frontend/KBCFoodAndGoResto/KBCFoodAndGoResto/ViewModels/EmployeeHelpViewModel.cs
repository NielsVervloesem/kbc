using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGoResto.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KBCFoodAndGoResto.ViewModels
{
    public class EmployeeHelpViewModel: BaseViewModel
    {
        private readonly IMealDataService _mealService;
        private readonly INavigationService _navigationService;
        private readonly IUserDataService _userService;

        private List<User> _users;

        public List<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        private List<Meal> meals;
        public List<Meal> Meals
        {
            get { return meals; }
            set
            {
                meals = value;
                OnPropertyChanged();
            }
        }

        public User SelectedUser { get; set; }
        public Meal SelectedMeal { get; set; }

        public List<ScannedMeal> ScanMeal { get; set; }
        public List<ScannedUser> ScanUser { get; set; }

        public Command SelectionMealChanged => new Command<Meal>(OnSelectionMealChange);

        private void OnSelectionMealChange(Meal selectMeal)
        {
            foreach (Meal meal in Meals)
            {
                if (meal.Equals(selectMeal))
                {
                    meal.BackgroundColor = Color.FromHex("#00AEEF");
                    SelectedMeal = selectMeal;
                }
                else
                {
                    meal.BackgroundColor = Color.Transparent;
                }
            }
        }

        public Command SelectionUserChanged => new Command<User>(OnSelectionUserChange);

        private void OnSelectionUserChange(User selectUser)
        {
            foreach (User user in Users)
            {
                if (user.Equals(selectUser))
                {
                    user.BackgroundColor = Color.FromHex("#00AEEF");
                    SelectedUser = selectUser;
                }
                else
                {
                    user.BackgroundColor = Color.Transparent;
                }
            }
        }

        public Command ConfirmCommand => new Command(async () => await AddMealToHistoryAndNavigateBack());
        private async Task AddMealToHistoryAndNavigateBack()
        {
            List<Meal> mealList = new List<Meal>();

            mealList.Add(SelectedMeal);

            await _userService.AddMealHistory(SelectedUser.Id, mealList);
            await _navigationService.NavigateToAsync<ScanViewModel>();
        }

        public EmployeeHelpViewModel(INavigationService navigationService, IMealDataService mealDataService, IUserDataService userDataService)
        {
            _navigationService = navigationService;
            _mealService = mealDataService;
            _userService = userDataService;
            Users = new List<User>();
            Meals = new List<Meal>();
            ScanMeal = new List<ScannedMeal>();
            ScanUser = new List<ScannedUser>();
            
            MessagingCenter.Subscribe<NumPadViewModel, List<ScannedMeal>>(this, "sendScannedMeals",
                (sender, filterdScannedMeals) =>
                {
                    this.ScanMeal = filterdScannedMeals;
                    GetMealsCommand.Execute(null);
                });


            MessagingCenter.Subscribe<NumPadViewModel, List<ScannedUser>>(this, "sendScannedUsers",
                (sender, scannedUsers) =>
                {
                    //this.ScanUser = scannedUsers;
                    GetUserCommand.Execute(null);
                });
        }

        public Command GetUserCommand => new Command(async () => await GetUsers());
        private async Task GetUsers()
        {
            /*
            List<long> idList = new List<long>();
            foreach (var scannedUser in this.ScanUser)
            {
                idList.Add(scannedUser.Id);
            }
            if (idList.Count != 0)
            {
                idList = idList.Take(5).ToList();
                Users = await _userService.GetUsersByIdList(idList);
                Users[0].BackgroundColor = Color.FromHex("#00AEEF");
                SelectedUser = Users[0];
            }
            */
            Users = await _userService.GetAllUsers();
            Users[0].BackgroundColor = Color.FromHex("#00AEEF");
            SelectedUser = Users[0];
        }

        public Command GetMealsCommand => new Command(async () => await getMeals());

        private async Task getMeals()
        {
            List<long> idList = new List<long>();
            foreach (var scannedMeal in this.ScanMeal)
            {
                idList.Add(scannedMeal.Id);
            }
            if(idList.Count != 0)
            {
                Meals = (await _mealService.GetMealByIdList(idList)).ToList();
                Meals[0].BackgroundColor = Color.FromHex("#00AEEF");
                SelectedMeal = Meals[0];
            }
        }
    }
}
