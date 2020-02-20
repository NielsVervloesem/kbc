using KBCFoodAndGo.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KBCFoodAndGo.Shared.Interfaces.Repository
{
    public interface IDataRepository
    {
        Task<T> OnGetAsync<T>(string uri, string authToken = "");
        Task<T> OnPostAsyncReturnsListOfMeals<T>(string uri, List<long> id, string authToken = "");
        Task<T> OnPostAsync<T>(string uri, T data, string authToken = "");
        Task<T> OnPutAsync<T>(string uri, T data, string authToken = "");
        Task OnDeleteAsync(string uri, string authToken = "");
        Task<User> OnPutAsyncReturnUser(string uri, List<Meal> data, string authToken = "");

    }

}
