using KBCFoodAndGo.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KBCFoodAndGo.Shared.Interfaces.Services
{
    public interface IScanService
    {
        Task<List<ScannedMeal>> MakeMealScan();
        Task<List<ScannedUser>> MakePersonScan();
    }
}
