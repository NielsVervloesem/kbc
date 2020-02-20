using KBCFoodAndGo.Helpers;
using KBCFoodAndGo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KBCFoodAndGo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticsView : TabbedSwipePage
    {
        public StatisticsView()
        {
            InitializeComponent();
            BindingContext = AppContainer.Resolve<StatisticsViewModel>();
        }
    }
}