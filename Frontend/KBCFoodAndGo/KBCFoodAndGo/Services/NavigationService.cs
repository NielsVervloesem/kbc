using KBCFoodAndGo.Helpers;
using KBCFoodAndGo.Interfaces.Services;
using KBCFoodAndGo.ViewModels;
using KBCFoodAndGo.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KBCFoodAndGo.Services
{
    public class NavigationService : INavigationService
    {
        private Application CurrentApplication => Application.Current;

        private readonly Dictionary<Type, Type> _viewModelViewMappings;

        public NavigationService()
        {
            _viewModelViewMappings = new Dictionary<Type, Type>
            {
                {typeof(LoginViewModel), typeof(LoginView)},
                {typeof(HomeViewModel), typeof(HomeView)},
                {typeof(MealsViewModel), typeof(MealsView)},
                {typeof(ManageViewModel), typeof(ManageView)},
                {typeof(MealDetailViewModel), typeof(MealDetailView)},
                {typeof(MealEditViewModel), typeof(MealEditView)},
                {typeof(MealCreateViewModel), typeof(MealEditView)},
                {typeof(LogViewModel), typeof(LogsView)},
                {typeof(MenuCreateViewModel), typeof(MenuCreateView)},
                {typeof(MenuViewModel), typeof(MenuView)},
                {typeof(MealHistoryViewModel), typeof(MealsHistoryView)},
                {typeof(StatisticsViewModel), typeof(StatisticsView)},
                {typeof(CreateUserViewModel), typeof(CreateUserView)}
            };
        }

        public async Task PopBackAsync()
        {
            if (CurrentApplication.MainPage != null)
            {
                await CurrentApplication.MainPage.Navigation.PopAsync();
            }
        }

        public async Task PopUntilDestination(Type destinationPage)
        {
            int leastFoundIndex = 0;
            int pagesToRemove = 0;

            for (int index = CurrentApplication.MainPage.Navigation.NavigationStack.Count - 1; index > 0; index--)
            {
                if (CurrentApplication.MainPage.Navigation.NavigationStack[index].GetType() == destinationPage)
                {
                    break;
                }

                leastFoundIndex = index;
                pagesToRemove++;
            }

            for (int index = 0; index < pagesToRemove - 1; index++)
            {
                CurrentApplication.MainPage.Navigation.RemovePage(CurrentApplication.MainPage.Navigation.NavigationStack[leastFoundIndex]);
            }

            await CurrentApplication.MainPage.Navigation.PopAsync();
        }



        public async Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            if (CurrentApplication.MainPage != null)
            {
                Page view = CreateViewFor(typeof(TViewModel));
                if (typeof(TViewModel) == typeof(HomeViewModel))
                {
                    CurrentApplication.MainPage = new NavigationPage(view);
                }
                else if (typeof(TViewModel) == typeof(MenuViewModel))
                {
                    CurrentApplication.MainPage = new NavigationPage(view);
                }
                else if (typeof(TViewModel) == typeof(CreateUserViewModel))
                {
                    CurrentApplication.MainPage = new NavigationPage(view);
                }
                else if (typeof(TViewModel) == typeof(LoginViewModel))
                {
                    CurrentApplication.MainPage = view;
                }
                else
                {
                    await CurrentApplication.MainPage.Navigation.PushAsync(view);
                }
            }
        }

        private Page CreateViewFor(Type viewModelType)
        {
            var viewType = GetViewTypeForViewModel(viewModelType);
            Page view = AppContainer.Resolve(viewType) as Page;

            ViewModelBase viewModel = AppContainer.Resolve(viewModelType) as ViewModelBase;
            if (view != null)
            {
                view.BindingContext = viewModel;
                return view;
            }
            throw new InvalidOperationException("View was not found");
        }

        protected Type GetViewTypeForViewModel(Type viewModelType)
        {
            if (!_viewModelViewMappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No matching view type as found for ${viewModelType}");
            }

            return _viewModelViewMappings[viewModelType];
        }
    }
}
