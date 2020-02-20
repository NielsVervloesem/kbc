using KBCFoodAndGo.Shared.Constants;
using KBCFoodAndGo.Shared.Exceptions;
using KBCFoodAndGo.Shared.Interfaces.Repository;
using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KBCFoodAndGo.Shared.Services.Data
{
    public class MenuDataService : IMenuDataService
    {
        private readonly IDataRepository _repository;
        private readonly Menu _emptyMenu = new Menu
        {
            Meals = new ObservableCollection<Meal>()
        };

        public MenuDataService(IDataRepository genericRepository)
        {
            _repository = genericRepository;
        }

        public async Task<Menu> AddMenu(Menu menu)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.MenuEndpoint
            };

            try
            {
                var returnedMenu = await _repository.OnPostAsync<Menu>(builder.ToString(), menu, "");
                return returnedMenu;
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return _emptyMenu;
            }
        }

        public async Task<Menu> GetLastMenu()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.MenuEndpoint + "last"
            };

            try
            {
                var menu = await _repository.OnGetAsync<Menu>(builder.ToString());
                return menu;
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return _emptyMenu;
            }
        }

        public async Task<Menu> GetMealsOfDay(string day)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.DayMenuEndpoint + day
            };

            try
            {
                var dayMealList = await _repository.OnGetAsync<Menu>(builder.ToString());
                return dayMealList;
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return _emptyMenu;
            }
        }

        public async Task UpdateMenu(ObservableCollection<Meal> selectedMeals, long id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.MenuEndpoint + id
            };

            try
            {
                await _repository.OnPutAsync<List<Meal>>(builder.ToString(), new List<Meal>(selectedMeals), "");
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
