

using Android.Content;
using Android.Support.Design.Widget;
using KBCFoodAndGo.Droid.Dependencies;
using KBCFoodAndGo.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TabbedSwipePage), typeof(CustomTabbedPageRenderSwipe))]

namespace KBCFoodAndGo.Droid.Dependencies
{
    public class CustomTabbedPageRenderSwipe : TabbedPageRenderer
    {
        public CustomTabbedPageRenderSwipe(Context context) : base(context)
        {
        }

        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);
            if (child is TabLayout tabLayout)
            {
                tabLayout.TabMode = TabLayout.ModeScrollable;
            }
        }
    }
}