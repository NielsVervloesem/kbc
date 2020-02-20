using KBCFoodAndGoResto.ViewModels;
using System.Threading.Tasks;

namespace KBCFoodAndGoResto.Interfaces
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task PopBackAsync();
    }
}
