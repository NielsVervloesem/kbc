using KBCFoodAndGoResto.Helpers;
using KBCFoodAndGoResto.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KBCFoodAndGoResto.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanResultView : ContentPage
    {
        public ScanResultView()
        {
            InitializeComponent();
            BindingContext = AppContainer.Resolve<ScanResultViewModel>();
        }
    }
}