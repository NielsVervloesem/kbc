using Xamarin.Forms;

namespace KBCFoodAndGo.Helpers
{
    public class TabbedBottomPage : TabbedPage
    {
        public TabbedBottomPage()
        {
            Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetIsSwipePagingEnabled(this, false);
        }
    }
}