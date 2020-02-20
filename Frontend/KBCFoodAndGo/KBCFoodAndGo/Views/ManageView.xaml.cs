using KBCFoodAndGo.Helpers;
using KBCFoodAndGo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KBCFoodAndGo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageView : ContentPage
    {
        public ManageView()
        {
            InitializeComponent();
            BindingContext = AppContainer.Resolve<ManageViewModel>();

        }
    }
}