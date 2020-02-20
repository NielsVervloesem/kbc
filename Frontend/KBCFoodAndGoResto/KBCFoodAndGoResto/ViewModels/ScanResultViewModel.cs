using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGoResto.Interfaces;
using Xamarin.Forms;

namespace KBCFoodAndGoResto.ViewModels
{
    public class ScanResultViewModel : BaseViewModel
    {

        private readonly IMealDataService _mealService;
        private readonly INavigationService _navigationService;
        private readonly IScanService _scanService;
        private readonly IUserDataService _userService;
        private readonly IMenuDataService _menuService;

        public List<ScannedUser> ScannedUsers { get; set; }
        public List<ScannedMeal> ScannedMeals { get; set; }
        public List<ScannedMeal> FilterdScannedMeals { get; set; }

        private Meal _scannedMeal;

        public Meal ScannedMeal
        {
            get { return _scannedMeal; }
            set
            {
                _scannedMeal = value;
                OnPropertyChanged();
            }
        }

        private User _scannedPerson;

        public User ScannedPerson
        {
            get { return _scannedPerson; }
            set
            {
                _scannedPerson = value;
                OnPropertyChanged();
            }
        }

        private Boolean confirmClickAble;

        public Boolean ConfirmClickAble
        {
            get { return confirmClickAble; }
            set { confirmClickAble = value;
                OnPropertyChanged();
            }
        }

        public Command ConfirmCommand => new Command(async () => await AddMenuAndNavigateBack());

        private async Task AddMenuAndNavigateBack()
        {
            List<Meal> mealList = new List<Meal>();
            mealList.Add(ScannedMeal);
            await _userService.AddMealHistory(ScannedPerson.Id, mealList);

            await _navigationService.NavigateToAsync<ScanViewModel>();
        }

        public Command GetHelpCommand => new Command(async () => await NavigateToNumPad());
        public Command ReScanCommand => new Command(async () => await ScanMealAndPerson());

        private async Task NavigateToNumPad()
        {
            await _navigationService.NavigateToAsync<NumPadViewModel>();
            MessagingCenter.Send(this, "Code", "1234");
            MessagingCenter.Send(this, "UsersToNumPad", ScannedUsers);
            MessagingCenter.Send(this, "MealsToNumPad", FilterdScannedMeals);       
        }

        public ScanResultViewModel(IScanService scanService, INavigationService navigationService, IMealDataService mealService, IUserDataService userService, IMenuDataService menuService)
        {
            _scanService = scanService;
            _mealService = mealService;
            _navigationService = navigationService;
            _userService = userService;
            _menuService = menuService;

            ScannedUsers = new List<ScannedUser>();
            ScannedMeals = new List<ScannedMeal>();
            FilterdScannedMeals = new List<ScannedMeal>();

            ScannedPerson = new User();

            ICommand setupCommand = new Command(async () => await ScanMealAndPerson());
            setupCommand.Execute(null);
        }

        private string getCurrentDay()
        {
            DateTime today = DateTime.Today;
            string weekday = today.ToString("dddd", new CultureInfo("nl-BE"));
            weekday = char.ToUpper(weekday.First()) + weekday.Substring(1).ToLower();

            if (weekday == "Zondag" || weekday == "Zaterdag")
            {
                weekday = "Maandag";
            }

            return weekday;

        }


        public async Task ScanMealAndPerson()
        {
            ScannedUsers.Clear();
            ScannedMeals.Clear();
            FilterdScannedMeals.Clear();

            ScannedMeals = await _scanService.MakeMealScan();
            ScannedUsers = await _scanService.MakePersonScan();

            KBCFoodAndGo.Shared.Models.Menu menuMeals = await _menuService.GetMealsOfDay(getCurrentDay());

            foreach (ScannedMeal scanMeal in ScannedMeals)
            {
                foreach (Meal menuMeal in menuMeals.Meals)
                {
                    if (menuMeal.Id == scanMeal.Id)
                    {
                        FilterdScannedMeals.Add(scanMeal);
                    }
                }
            }
            ScannedMeal = await _mealService.GetMealById(FilterdScannedMeals[0].Id);
            if (ScannedUsers.Count != 0)
            {
                ScannedPerson = await _userService.GetUserById(ScannedUsers[0].Id);
                ConfirmClickAble = true;
            } else
            {
                ScannedPerson.LastName = "";
                ScannedPerson.FirstName = "Onbekend";
                ScannedPerson.ImageUrl = "https://kbc-cdn.s3.eu-central-1.amazonaws.com/Onbekend-persoon-300x300.png";
                ConfirmClickAble = false;
            }
        }
    }
}