using KBCFoodAndGo.Helpers;
using KBCFoodAndGo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KBCFoodAndGo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogsView : ContentPage
    {
        public LogsView()
        {
            InitializeComponent();
            BindingContext = AppContainer.Resolve<LogViewModel>();

        }
    }
}