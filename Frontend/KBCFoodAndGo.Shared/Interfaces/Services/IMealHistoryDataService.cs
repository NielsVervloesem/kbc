using System.Collections.Generic;
using System.Threading.Tasks;
using KBCFoodAndGo.Shared.Models;

namespace KBCFoodAndGo.Shared.Interfaces.Services
{
    public interface IMealHistoryDataService
    {
        Task<List<ChartPoint>> GetAllMealsFromToday();
        Task<List<ChartPoint>> GetProfitFromLastFiveDays();
        Task<List<ChartPoint>> GetAllTimeFavoriteMeals();
    }
}