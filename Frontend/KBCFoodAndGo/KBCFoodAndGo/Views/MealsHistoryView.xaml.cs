using KBCFoodAndGo.Helpers;
using KBCFoodAndGo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KBCFoodAndGo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealsHistoryView : ContentPage
    {
        public MealsHistoryView()
        {
            InitializeComponent();
            BindingContext = AppContainer.Resolve<MealHistoryViewModel>();
        }
    }
}