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
    public class LogDataService : ILogDataService
    {
        private readonly IDataRepository _repository;

        public LogDataService(IDataRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Log>> GetAllLogs()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.LogEndPoint
            };

            try
            {
                var logs = await _repository.OnGetAsync<List<Log>>(builder.ToString());
                return logs;
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return new List<Log>();
            }
        }
    }
}
