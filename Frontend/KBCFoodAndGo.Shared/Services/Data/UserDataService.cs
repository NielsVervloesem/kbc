
using KBCFoodAndGo.Shared.Constants;
using KBCFoodAndGo.Shared.Exceptions;
using KBCFoodAndGo.Shared.Interfaces.Repository;
using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KBCFoodAndGo.Shared.Services.Data
{
    public class UserDataService : IUserDataService
    {

        private readonly IDataRepository _repository;

        public UserDataService(IDataRepository genericRepository)
        {
            _repository = genericRepository;
        }
        public async Task<User> AddMealHistory(long userId, List<Meal> meals)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.UserEndpoint + userId
            };

            try
            {
                User returnUser = await _repository.OnPutAsyncReturnUser(builder.ToString(), meals, "");
                return returnUser;
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return new User();
            }
       
        }

        public async Task<User> CreateAsync(User user)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.UserEndpoint + "create"
            };
            try
            {
                var returnedUser = await _repository.OnPostAsync<User>(builder.ToString(), user, "");
                return returnedUser;
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return new User();
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.UserEndpoint
            };

            try
            {
                var returnAllUsers = await _repository.OnGetAsync<List<User>>(builder.ToString());
                return returnAllUsers;
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return new List<User>();
            }
        }

        public async Task<User> GetUserById(int id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.UserEndpoint + id
            };

            try
            {
                var returnUser = await _repository.OnGetAsync<User>(builder.ToString());
                return returnUser;
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return new User();
            }
        }

        public async Task<List<User>> GetUsersByIdList(List<long> idList)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.UserEndpoint + "list"
            };

            try
            {
                var returnAllUsers = await _repository.OnPostAsyncReturnsListOfMeals<List<User>>(builder.ToString(), idList, "");
                return returnAllUsers;
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return new List<User>();
            }
        }

        public async Task<User> LoginAsync(User user)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.UserEndpoint + "login"
            };

            try
            {
                var returnedUser = await _repository.OnPostAsync<User>(builder.ToString(), user, "");
                return returnedUser;
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return new User();
            }

        }
    }
}
