using System.Collections.ObjectModel;
using System.Threading.Tasks;
using KBCFoodAndGo.Shared.Models;

namespace KBCFoodAndGo.Shared.Interfaces.Services
{
    public interface IMenuDataService
    {
        Task<Menu> AddMenu(Menu menu);
        Task<Menu> GetLastMenu();
        Task<Menu> GetMealsOfDay(string day);
        Task UpdateMenu(ObservableCollection<Meal> selectedMeals, long id);

    }
}
