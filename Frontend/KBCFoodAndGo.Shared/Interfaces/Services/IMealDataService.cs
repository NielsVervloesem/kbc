using KBCFoodAndGo.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KBCFoodAndGo.Shared.Interfaces.Services
{
    public interface IMealDataService
    {
        Task<IEnumerable<Meal>> GetAllMeals();
        Task<Meal> AddMeal(Meal meal);
        Task DeleteMeal(long id);
        Task<Meal> UpdateMeal(long id, Meal meal);
        Task<Meal> GetMealById(long id);
        Task<List<Meal>> GetMealByIdList(List<long> id);
        Task<IEnumerable<Meal>> GetMealsByText(string searchText);
    }
}
