using KBCFoodAndGo.Helpers;
using KBCFoodAndGo.ViewModels;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;


namespace KBCFoodAndGo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateUserView : ContentPage
    {
        public CreateUserView()
        {
            InitializeComponent();
            BindingContext = AppContainer.Resolve<CreateUserViewModel>();
        }
    }
}