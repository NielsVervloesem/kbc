using KBCFoodAndGo.Shared.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KBCFoodAndGo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuView : TabbedPage
    {
        public MenuView()
        {
            InitializeComponent();

            if (LocalStorage.Get("UserRole").ToString() != "CAFETARIA_EMPLOYEE")
            {
                this.ToolbarItems.RemoveAt(1);
            }
            if (LocalStorage.Get("UserRole").ToString() == "ADMIN")
            {
                this.ToolbarItems.Clear();
            }
        }
    }
}