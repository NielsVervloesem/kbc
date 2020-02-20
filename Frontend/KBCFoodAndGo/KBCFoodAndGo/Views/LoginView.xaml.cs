using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KBCFoodAndGo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            
            InitializeComponent();
        }
    }
}