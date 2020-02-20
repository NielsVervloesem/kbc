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
    public class MealHistoryDataService : IMealHistoryDataService
    {
        private readonly IDataRepository _repository;
        private readonly  List<ChartPoint> _emptyChartPoints = new List<ChartPoint>();
        public MealHistoryDataService(IDataRepository genericRepository)
        {
            _repository = genericRepository;
        }

        public async Task<List<ChartPoint>> GetAllMealsFromToday()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.MealHistoryEndpoint + "today"
            };

            try
            {
                var chartPoints = await _repository.OnGetAsync<List<ChartPoint>>(builder.ToString());
                return chartPoints;
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return _emptyChartPoints;
            }
        }

        public async Task<List<ChartPoint>> GetProfitFromLastFiveDays()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.MealHistoryEndpoint + "profits/five"
            };

            try
            {
                var chartPoints = await _repository.OnGetAsync<List<ChartPoint>>(builder.ToString());
                return chartPoints;
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return _emptyChartPoints;
            }
        }

        public async Task<List<ChartPoint>> GetAllTimeFavoriteMeals()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.MealHistoryEndpoint + "favorite"
            };

            try
            {
                var chartPoints = await _repository.OnGetAsync<List<ChartPoint>>(builder.ToString());
                return chartPoints;
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return _emptyChartPoints;
            }
        }
    }
}
