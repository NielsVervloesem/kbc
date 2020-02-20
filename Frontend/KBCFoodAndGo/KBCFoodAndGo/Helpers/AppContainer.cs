using Autofac;
using FluentValidation;
using KBCFoodAndGo.Interfaces.Services;
using KBCFoodAndGo.Services;
using KBCFoodAndGo.Shared.Interfaces.Repository;
using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.Shared.Repository;
using KBCFoodAndGo.Shared.Services;
using KBCFoodAndGo.Shared.Services.Data;
using KBCFoodAndGo.Validators;
using KBCFoodAndGo.ViewModels;
using KBCFoodAndGo.Views;
using System;

namespace KBCFoodAndGo.Helpers
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
            builder.RegisterType<ViewModelBase>();
            builder.RegisterType<MealsViewModel>();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<HomeViewModel>();
            builder.RegisterType<ManageViewModel>();
            builder.RegisterType<MealDetailViewModel>();
            builder.RegisterType<MealEditViewModel>();
            builder.RegisterType<MealCreateViewModel>();
            builder.RegisterType<LogViewModel>();
            builder.RegisterType<MenuCreateViewModel>();
            builder.RegisterType<MenuViewModel>();
            builder.RegisterType<MealHistoryViewModel>();
            builder.RegisterType<StatisticsViewModel>();
            builder.RegisterType<CreateUserViewModel>();

            //Views
            builder.RegisterType<HomeView>();
            builder.RegisterType<LoginView>();
            builder.RegisterType<MealsView>();
            builder.RegisterType<LogsView>();
            builder.RegisterType<MealsHistoryView>();
            builder.RegisterType<MenuView>();
            builder.RegisterType<StatisticsView>();
            builder.RegisterType<ManageView>();
            builder.RegisterType<MealDetailView>();
            builder.RegisterType<MealEditView>();
            builder.RegisterType<MenuCreateView>();
            builder.RegisterType<CreateUserView>();

            //services - data
            builder.RegisterType<MealDataService>().As<IMealDataService>();
            builder.RegisterType<LogDataService>().As<ILogDataService>();
            builder.RegisterType<MenuDataService>().As<IMenuDataService>();
            builder.RegisterType<UserDataService>().As<IUserDataService>();
            builder.RegisterType<MealHistoryDataService>().As<IMealHistoryDataService>();

            //services
            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<MediaFileService>().As<IMediaFileService>();

            //General
            builder.RegisterType<DataRepository>().As<IDataRepository>();
            builder.RegisterType<DialogService>().As<IDialogService>();

            //Validators
            builder.RegisterType<MealValidator>().As<AbstractValidator<Meal>>();
            builder.RegisterType<UserValidator>().As<AbstractValidator<User>>();

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