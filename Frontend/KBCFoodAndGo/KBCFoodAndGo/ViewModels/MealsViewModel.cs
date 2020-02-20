using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.Shared.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KBCFoodAndGo.ViewModels
{
    public class MealsViewModel : ViewModelBase
    {
        private List<Meal> _meals;
        private readonly IMenuDataService _menuDataService;
        public List<Meal> EmptyMealList { get; set; }
        public List<string> WeekDays { get; set; }

        private string _day;
        public string CurrentDay
        {
            get { return _day; }
            set
            {
                _day = value;
                OnPropertyChanged();
            }
        }

        public Command DaySelectionChanged => new Command<string>(async (day) => await OnDaySelectionChange(day));

        private async Task OnDaySelectionChange(string day)
        {
            CurrentDay = day;
            try
            {
                Meals = (await _menuDataService.GetMealsOfDay(CurrentDay)).Meals.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private readonly ICommand _setupCommand;
        public MealsViewModel(IMenuDataService menuDataService)
        {
            WeekDays = new List<string>(){
            "Maandag",
            "Dinsdag",
            "Woensdag",
            "Donderdag",
            "Vrijdag"
            };
            CurrentDay = getCurrentDay();

            _menuDataService = menuDataService;
            _setupCommand = new Command(async () => await GetMeals());
            _setupCommand.Execute(null);
            
            PusherService.Pusher.Subscribe("menuChannel");
            PusherService.Pusher.Bind("createMenu", UpdateMenu);
        }

        private string getCurrentDay()
        {
            DateTime today = DateTime.Today;
            string weekday = today.ToString("dddd", new CultureInfo("nl-BE"));
            weekday = char.ToUpper(weekday.First()) + weekday.Substring(1).ToLower();

            if(weekday == "Zondag" || weekday == "Zaterdag")
            {
                weekday = "Maandag";
            }

            return weekday;

        }

        private void UpdateMenu(dynamic menu)
        {
            _setupCommand.Execute(null);
        }


        public async Task GetMeals()
        {
            if (IsBusy)
            {
                return;
            }
              

            IsBusy = true;

            try
            {
                Meals = (await _menuDataService.GetMealsOfDay(CurrentDay)).Meals.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public List<Meal> Meals
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
