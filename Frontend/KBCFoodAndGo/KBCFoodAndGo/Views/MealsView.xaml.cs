using KBCFoodAndGo.Helpers;
using KBCFoodAndGo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KBCFoodAndGo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealsView : ContentPage
    {
        public MealsView()
        {
            InitializeComponent();
            BindingContext = AppContainer.Resolve<MealsViewModel>();
        }
    }
}