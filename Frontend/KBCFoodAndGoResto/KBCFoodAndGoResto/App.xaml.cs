using KBCFoodAndGoResto.Helpers;
using KBCFoodAndGoResto.ViewModels;
using KBCFoodAndGoResto.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace KBCFoodAndGoResto
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            AppContainer.RegisterDependencies();
            AppContainer.Resolve<EmployeeHelpViewModel>();

            MainPage = new NavigationPage(new ScanView()
            {
                BindingContext = AppContainer.Resolve<ScanViewModel>()
            });
        }
        protected override void OnStart()
        {
            // Handle when your app starts
            AppCenter.Start("ios=84d24841-4053-4b70-a1dd-4d50dedea8db;" +
                            "android=fc68aab8-d35f-4e75-998e-1847bc7784b4;" +
                            "uwp=ec038561-bdc9-41d0-9267-101c209609b1;", typeof(Analytics), typeof(Crashes));
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
