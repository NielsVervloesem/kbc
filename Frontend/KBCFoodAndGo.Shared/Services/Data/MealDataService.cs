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
    public class MealDataService : IMealDataService
    {
        private readonly IDataRepository _repository;
        private readonly Meal _emptyMeal = new Meal();
        public MealDataService(IDataRepository genericRepository)
        {
            _repository = genericRepository;
        }
        public async Task<IEnumerable<Meal>> GetAllMeals()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.MealEndpoint
            };
            try
            {
                var meals = await _repository.OnGetAsync<List<Meal>>(builder.ToString());
                return meals;
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return new List<Meal>();
            }
        }

        public async Task<Meal> AddMeal(Meal meal)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.MealEndpoint
            };
            try
            {
                var returnedMeal = await _repository.OnPostAsync<Meal>(builder.ToString(), meal, "");
                return returnedMeal;
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return _emptyMeal;
            }
        }

        public async Task<Meal> GetMealById(long id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.MealEndpoint + id
            };
            try
            {
                var meal = await _repository.OnGetAsync<Meal>(builder.ToString());
                return meal;

            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return _emptyMeal;
            }
        }

        public async Task DeleteMeal(long id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.MealEndpoint + id
            };

            await _repository.OnDeleteAsync(builder.ToString(), "");

        }

        public async Task<Meal> UpdateMeal(long id,Meal meal)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.MealEndpoint + id
            };
            try
            {
                var updatedMeal = await _repository.OnPutAsync<Meal>(builder.ToString(), meal, "");
                return updatedMeal;
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return _emptyMeal;
            }
        }

        public async Task<IEnumerable<Meal>> GetMealsByText(string searchText)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.MealSearchEndpoint + searchText
            };

            try
            {
                var meal = await _repository.OnGetAsync<List<Meal>>(builder.ToString());
                return meal;
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return new List<Meal>();
            }

        }

        public async Task<List<Meal>> GetMealByIdList(List<long> id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.MealEndpoint + "list"
            };

            var meals = await _repository.OnPostAsyncReturnsListOfMeals<List<Meal>>(builder.ToString(), id,"");
            return meals;
        }
    }
}
