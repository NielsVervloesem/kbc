using Autofac;
using KBCFoodAndGoResto.ViewModels;
using KBCFoodAndGoResto.Views;
using KBCFoodAndGo.Shared.Interfaces.Repository;
using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Repository;
using KBCFoodAndGo.Shared.Services.Data;
using System;
using KBCFoodAndGoResto.Services;
using KBCFoodAndGoResto.Interfaces;

namespace KBCFoodAndGoResto.Helpers
{
    public class AppContainer
    {
        protected AppContainer()
        {

        }
        private static IContainer _container;
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            //ViewModels
            builder.RegisterType<ScanViewModel>();
            builder.RegisterType<ScanResultViewModel>();
            builder.RegisterType<NumPadViewModel>();
            builder.RegisterType<EmployeeHelpViewModel>();

            //Views
            builder.RegisterType<ScanView>();
            builder.RegisterType<ScanResultView>();
            builder.RegisterType<NumPadView>();
            builder.RegisterType<EmployeeHelpView>();

            //services - data
            builder.RegisterType<DataRepository>().As<IDataRepository>();
            builder.RegisterType<ScanService>().As<IScanService>();
            builder.RegisterType<MealDataService>().As<IMealDataService>();
            builder.RegisterType<UserDataService>().As<IUserDataService>();
            builder.RegisterType<MenuDataService>().As<IMenuDataService>();

            //services
            builder.RegisterType<NavigationService>().As<INavigationService>();

            _container = builder.Build();
        }

        public static object Resolve(Type typeName)
        {
            return _container.Resolve(typeName);
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}