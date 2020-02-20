using KBCFoodAndGo.Helpers;
using KBCFoodAndGo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KBCFoodAndGo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuCreateView : ContentPage
    {
        public MenuCreateView()
        {
            InitializeComponent();
            BindingContext = AppContainer.Resolve<MenuCreateViewModel>();
        }
    }
}