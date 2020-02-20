using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KBCFoodAndGo.Shared.Models
{
    public class MealHistoryGroup : ObservableCollection<Meal>
    {
        public string MealHistoryData { get; }

        public MealHistoryGroup(string mealHistoryData)
        {
            MealHistoryData = mealHistoryData;
        }

        public MealHistoryGroup(string mealHistoryData, IList<Meal> source)
            : base(source)
        {
            MealHistoryData = mealHistoryData;
        }
    }
}