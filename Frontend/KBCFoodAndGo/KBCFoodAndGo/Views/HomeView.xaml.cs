using KBCFoodAndGo.Helpers;
using KBCFoodAndGo.ViewModels;
using Xamarin.Forms.Xaml;

namespace KBCFoodAndGo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : TabbedBottomPage
    {
        public HomeView()
        {
            InitializeComponent();
            BindingContext = AppContainer.Resolve<HomeViewModel>();
        }
    }
}