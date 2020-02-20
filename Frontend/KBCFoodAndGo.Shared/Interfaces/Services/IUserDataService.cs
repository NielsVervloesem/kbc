using System.Collections.Generic;
using System.Threading.Tasks;
using KBCFoodAndGo.Shared.Models;

namespace KBCFoodAndGo.Shared.Interfaces.Services
{
    public interface IUserDataService
    {
        Task<User> LoginAsync(User user);
        Task<User> CreateAsync(User user);
        Task<User> AddMealHistory(long userId, List<Meal> meals);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<List<User>> GetUsersByIdList(List<long> idList);
    }
}
