using System.Collections.Generic;
using System.Threading.Tasks;
using KBCFoodAndGo.Shared.Models;

namespace KBCFoodAndGo.Shared.Interfaces.Services
{
    public interface ILogDataService
    {
        Task<IEnumerable<Log>> GetAllLogs();
    }
}
