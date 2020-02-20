using KBCFoodAndGo.Shared.Constants;
using KBCFoodAndGo.Shared.Interfaces.Repository;
using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KBCFoodAndGo.Shared.Exceptions;

namespace KBCFoodAndGo.Shared.Services.Data
{
    public class ScanService : IScanService
    {
        private readonly IDataRepository _repository;

        public ScanService(IDataRepository genericRepository)
        {
            _repository = genericRepository;
        }

        public async Task<List<ScannedMeal>> MakeMealScan()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseAwsApiUrl)
            {
                Path = ApiConstants.ScanMealEndPoint
            };

            try
            {
                var value = await _repository.OnGetAsync<List<ScannedMeal>>(builder.ToString());
                return value;
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return new List<ScannedMeal>();
            }
        }

        public async Task<List<ScannedUser>> MakePersonScan()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.PythonBaseApiUrl)
            {
                Path = ""
            };

            try
            {
                var value = await _repository.OnGetAsync<List<ScannedUser>>(builder.ToString());
                return value;
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return new List<ScannedUser>();
            }
        }
    }
}
    