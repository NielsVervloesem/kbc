using KBCFoodAndGoResto.Helpers;
using KBCFoodAndGoResto.Interfaces;
using KBCFoodAndGoResto.ViewModels;
using KBCFoodAndGoResto.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KBCFoodAndGoResto.Services
{
    public class NavigationService : INavigationService
    {
        private Application CurrentApplication => Application.Current;

        private readonly Dictionary<Type, Type> _viewModelViewMappings;

        public NavigationService()
        {
            _viewModelViewMappings = new Dictionary<Type, Type>
            {
                {typeof(ScanResultViewModel), typeof(ScanResultView)},
                {typeof(ScanViewModel), typeof(ScanView)},
                {typeof(EmployeeHelpViewModel), typeof(EmployeeHelpView)},
                {typeof(NumPadViewModel), typeof(NumPadView)}
            };
        }

        public async Task PopBackAsync()
        {
            if (CurrentApplication.MainPage != null)
            {
                await CurrentApplication.MainPage.Navigation.PopAsync();
            }
        }

        public async Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            if (CurrentApplication.MainPage != null)
            {
                Page view = CreateViewFor(typeof(TViewModel));
                await CurrentApplication.MainPage.Navigation.PushAsync(view);
            }
        }

        private Page CreateViewFor(Type viewModelType)
        {
            var viewType = GetViewTypeForViewModel(viewModelType);
            Page view = AppContainer.Resolve(viewType) as Page;

            BaseViewModel viewModel = AppContainer.Resolve(viewModelType) as BaseViewModel;
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
