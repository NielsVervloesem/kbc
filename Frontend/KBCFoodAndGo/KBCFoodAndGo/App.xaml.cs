using KBCFoodAndGo.Helpers;
using KBCFoodAndGo.ViewModels;
using KBCFoodAndGo.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace KBCFoodAndGo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            AppContainer.RegisterDependencies();

            MainPage = new NavigationPage(new LoginView
            {
                BindingContext = AppContainer.Resolve<LoginViewModel>()
            });
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            AppCenter.Start("ios=80ff67b4-201b-4327-b977-272d6bee66af;"+
            "android=f38023be-fcca-48e9-a04d-9520f1a86625;" +
            "uwp=e3490ca6-e35a-40fc-95e2-86514e5b7c82;",typeof(Analytics),typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}