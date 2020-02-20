using KBCFoodAndGo.ViewModels;
using System;
using System.Threading.Tasks;

namespace KBCFoodAndGo.Interfaces.Services
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task PopBackAsync();
        Task PopUntilDestination(Type destinationPage);
    }
}
