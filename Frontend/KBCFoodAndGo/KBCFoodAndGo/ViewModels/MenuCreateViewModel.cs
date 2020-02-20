using KBCFoodAndGo.Interfaces.Services;
using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KBCFoodAndGo.ViewModels
{
    public class MenuCreateViewModel : ViewModelBase
    {
        private readonly IMealDataService _mealDataService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly IMenuDataService _menuDataService;


        private ObservableCollection<Meal> _meals;
        public ObservableCollection<Meal> Meals
        {
            get => _meals;
            set
            {
                _meals = value;
                OnPropertyChanged();
            }
        }

        private string _day;

        public string Day
        {
            get { return _day; }
            set
            {
                _day = value;
                OnPropertyChanged();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                if (_searchText != "")
                {
                    ChangeMealsCommand.Execute(null);
                }
                else
                {
                    GetAllMealsCommand.Execute(null);
                    OnDaySelectionChange(Day);

                }
            }
        }

        private ObservableCollection<Meal> _selectedMeals;
        public ObservableCollection<Meal> SelectedMeals
        {
            get => _selectedMeals;
            set
            {
                _selectedMeals = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Meal> SelectedMealsMonday { get; set; }
        public ObservableCollection<Meal> SelectedMealsTuesday { get; set; }
        public ObservableCollection<Meal> SelectedMealsWednesday { get; set; }
        public ObservableCollection<Meal> SelectedMealsThursday { get; set; }
        public ObservableCollection<Meal> SelectedMealsFriday { get; set; }

        public List<string> Days { get; set; }

        public Command ConfirmCommand => new Command(async () => await AddMenuAndNavigateBack());
        public Command DaySelectionChanged => new Command<string>((day) => OnDaySelectionChange(day));
        public Command ChangeMealsCommand => new Command(async () => await ChangeMeals());
        public Command GetAllMealsCommand => new Command(async () => await GetMeals());
        

        private async Task AddMenuAndNavigateBack()
        {
            if (await _dialogService.ShowDialogTwoButtons("Bent u zeker dat u deze wijzingen aan het menu wilt aanbrengen?", "Menu aanpassen", "Ja", "Nee"))
            {
                await _menuDataService.UpdateMenu(SelectedMealsMonday, 1);
                await _menuDataService.UpdateMenu(SelectedMealsTuesday, 2);
                await _menuDataService.UpdateMenu(SelectedMealsWednesday, 3);
                await _menuDataService.UpdateMenu(SelectedMealsThursday, 4);
                await _menuDataService.UpdateMenu(SelectedMealsFriday, 5);
                MessagingCenter.Instance.Send(this, "reloadList", SelectedMeals);
                await _navigationService.PopBackAsync();
            }
        }

        public Command CancelCommand => new Command(async () => await NavigateBack());

        private async Task NavigateBack()
        {
            if (await _dialogService.ShowDialogTwoButtons("Uw wijzigingen zullen verloren gaan. Bent u zeker?", "Annuleren", "Ja", "Nee"))
            {
                await _navigationService.PopBackAsync();
            }
        }

        public Command SelectionChanged => new Command<Meal>(OnSelectionChange);

        private void OnDaySelectionChange(string day)
        {
            Day = day;
            switch (Day)
            {
                case "Maandag":
                    HighlightSelectedMeals(SelectedMealsMonday);
                    break;
                case "Dinsdag":
                    HighlightSelectedMeals(SelectedMealsTuesday);
                    break;
                case "Woensdag":
                    HighlightSelectedMeals(SelectedMealsWednesday);
                    break;
                case "Donderdag":
                    HighlightSelectedMeals(SelectedMealsThursday);
                    break;
                case "Vrijdag":
                    HighlightSelectedMeals(SelectedMealsFriday);
                    break;
            }
        }

        private void OnSelectionChange(Meal meal)
        {
            switch (Day)
            {
                case "Maandag":
                    UpdateSelection(SelectedMealsMonday, meal);
                    HighlightSelectedMeals(SelectedMealsMonday);
                    break;
                case "Dinsdag":
                    UpdateSelection(SelectedMealsTuesday, meal);
                    HighlightSelectedMeals(SelectedMealsTuesday);
                    break;
                case "Woensdag":
                    UpdateSelection(SelectedMealsWednesday, meal);
                    HighlightSelectedMeals(SelectedMealsWednesday);
                    break;
                case "Donderdag":
                    UpdateSelection(SelectedMealsThursday, meal);
                    HighlightSelectedMeals(SelectedMealsThursday);
                    break;
                case "Vrijdag":
                    UpdateSelection(SelectedMealsFriday, meal);
                    HighlightSelectedMeals(SelectedMealsFriday);
                    break;
            }

        }

        private void UpdateSelection(ObservableCollection<Meal> selectedMeals, Meal meal)
        {
            if (selectedMeals.Contains(meal))
            {
                selectedMeals.Remove(meal);
            }
            else
            {
                selectedMeals.Add(meal);
            }
        }

        public MenuCreateViewModel(IMenuDataService menuDataService, IMealDataService mealDataService, INavigationService navigationService, IDialogService dialogService)
        {
            _mealDataService = mealDataService;
            _menuDataService = menuDataService;
            _navigationService = navigationService;

            SelectedMealsMonday = new ObservableCollection<Meal>();
            SelectedMealsTuesday = new ObservableCollection<Meal>();
            SelectedMealsWednesday = new ObservableCollection<Meal>();
            SelectedMealsThursday = new ObservableCollection<Meal>();
            SelectedMealsFriday = new ObservableCollection<Meal>();

            Day = "Maandag";
            Days = new List<string>(){
            "Maandag",
            "Dinsdag",
            "Woensdag",
            "Donderdag",
            "Vrijdag"
            };
            _dialogService = dialogService;
            ICommand setupCommand = new Command(async () => await Setup());

            setupCommand.Execute(null);
        }

        private async Task ChangeMeals()
        {
            Meals = new ObservableCollection<Meal>(await _mealDataService.GetMealsByText(SearchText));
            OnDaySelectionChange(Day);
        }

        private async Task Setup()
        {
            await GetMealsOfLastMenu();
            await GetMeals();
            OnDaySelectionChange(Day);
        }

        public void HighlightSelectedMeals(ObservableCollection<Meal> selectedMeals)
        {
            foreach (Meal meal in Meals)
            {
                if (selectedMeals.Contains(meal))
                {
                    meal.BackgroundColor = Color.FromHex("#00AEEF");
                    meal.IsChecked = true;
                }
                else
                {
                    meal.BackgroundColor = Color.Transparent;
                    meal.IsChecked = false;
                }
            }
        }

        public async Task GetMeals()
        {
            try
            {
                Meals = new ObservableCollection<Meal>(await _mealDataService.GetAllMeals());
                OnDaySelectionChange(Day);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async Task GetMealsOfLastMenu()
        {
            try
            {
                SelectedMealsMonday = new ObservableCollection<Meal>((await _menuDataService.GetMealsOfDay("Maandag")).Meals.ToList());
                SelectedMealsTuesday = new ObservableCollection<Meal>((await _menuDataService.GetMealsOfDay("Dinsdag")).Meals.ToList());
                SelectedMealsWednesday = new ObservableCollection<Meal>((await _menuDataService.GetMealsOfDay("Woensdag")).Meals.ToList());
                SelectedMealsThursday = new ObservableCollection<Meal>((await _menuDataService.GetMealsOfDay("Donderdag")).Meals.ToList());
                SelectedMealsFriday = new ObservableCollection<Meal>((await _menuDataService.GetMealsOfDay("Vrijdag")).Meals.ToList());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}

