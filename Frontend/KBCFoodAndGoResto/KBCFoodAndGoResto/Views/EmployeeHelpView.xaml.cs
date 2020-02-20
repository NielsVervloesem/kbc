using KBCFoodAndGoResto.Helpers;
using KBCFoodAndGoResto.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KBCFoodAndGoResto.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeHelpView : ContentPage
    {
        public EmployeeHelpView()
        {
            InitializeComponent();
            BindingContext = AppContainer.Resolve<EmployeeHelpViewModel>();
        }
    }
}