using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.Shared.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KBCFoodAndGo.ViewModels
{
    public class MealHistoryViewModel : ViewModelBase
    {
        private readonly IUserDataService _userService;

        private ObservableCollection<MealHistoryGroup> _mealHistoryGroup;

        public ObservableCollection<MealHistoryGroup> MealHistoryGroup
        {
            get => _mealHistoryGroup;
            set
            {
                _mealHistoryGroup = value;
                OnPropertyChanged();
            }
        }

        private Command setupCommand;

        public MealHistoryViewModel(IUserDataService userService)
        {
            _userService = userService;
            MealHistoryGroup = new ObservableCollection<MealHistoryGroup>();
            setupCommand = new Command(async () => await GetUserById());
            setupCommand.Execute(null);

            PusherService.Pusher.Subscribe("mealHistory");
            PusherService.Pusher.Bind("mealHistory", UpdateCharts);
        }
    

        private void UpdateCharts(dynamic obj)
        {
            setupCommand.Execute(null);
        }
    

        public async Task GetUserById()
        {
            int id = Convert.ToInt32(LocalStorage.Get("Id"));
            User user = await _userService.GetUserById(id);
            if (user.MealHistory.Count != 0)
            {
                CreateMealGroup(user.MealHistory);
            }
        }

        private void CreateMealGroup(List<MealHistory> mealHistories)
        {
            List<MealHistoryGroup> mealHistoryList = new List<MealHistoryGroup>();
            foreach (var mealHistory in mealHistories)
            {
                var dateEndingIndex = mealHistory.Date.LastIndexOf(":", StringComparison.Ordinal);
                var date = mealHistory.Date.Substring(0, dateEndingIndex);
                string data = "Datum: " + date + " Totale kosten: €" + mealHistory.TotalPrice;
                List<Meal> mealList = new List<Meal>();
                foreach (var meal in mealHistory.MealList)
                {
                    mealList.Add(meal);
                }
                mealHistoryList.Add(new MealHistoryGroup(data, mealList));
            }

            MealHistoryGroup = new ObservableCollection<MealHistoryGroup>(mealHistoryList);
        }
    }
}
