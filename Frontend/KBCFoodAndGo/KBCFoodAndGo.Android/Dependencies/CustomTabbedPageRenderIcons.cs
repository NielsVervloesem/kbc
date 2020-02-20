

using Android.Content;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using KBCFoodAndGo.Droid.Dependencies;
using KBCFoodAndGo.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TabbedBottomPage), typeof(CustomTabbedPageRenderIcons))]
namespace KBCFoodAndGo.Droid.Dependencies
{
    public class CustomTabbedPageRenderIcons : TabbedPageRenderer, TabLayout.IOnTabSelectedListener
    {
        public CustomTabbedPageRenderIcons(Context context) : base(context)
        {
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            InvertLayoutThroughScale();
            base.OnLayout(changed, l, t, r, b);
        }

        private void InvertLayoutThroughScale()
        {
            ViewGroup.ScaleY = -1;
            TabLayout tabLayout = null;
            ViewPager viewPager = null;
            for (int i = 0; i < ChildCount; ++i)
            {
                Android.Views.View view = GetChildAt(i);
                if (view is TabLayout tabs)
                {
                    tabLayout = tabs;
                    tabLayout.SetSelectedTabIndicatorColor(Android.Graphics.Color.White);
                    tabLayout.SetBackgroundColor(Android.Graphics.Color.White);
                }
                else if (view is ViewPager pager)
                {
                    viewPager = pager;
                }
            }
            if (tabLayout != null)
            {
                tabLayout.ViewTreeObserver.AddOnGlobalLayoutListener(new GlobalLayoutListener(viewPager, tabLayout));
                if (viewPager != null) tabLayout.ScaleY = viewPager.ScaleY = -1;
            }
        }

        private class GlobalLayoutListener : Java.Lang.Object, Android.Views.ViewTreeObserver.IOnGlobalLayoutListener
        {
            private readonly ViewPager _viewPager;
            private readonly TabLayout _tabLayout;

            public GlobalLayoutListener(ViewPager viewPager, TabLayout tabLayout)
            {
                _viewPager = viewPager;
                _tabLayout = tabLayout;
            }

            public void OnGlobalLayout()
            {
                _viewPager.SetPadding(0, -_tabLayout.MeasuredHeight, 0, 0);
                _tabLayout.ViewTreeObserver.RemoveOnGlobalLayoutListener(this);
            }
        }



        void TabLayout.IOnTabSelectedListener.OnTabSelected(TabLayout.Tab tab)
        {
            if (tab == null)
            {
                return;
            }

            switch (tab.Text)
            {
                case "Home":
                    tab.SetIcon(Resource.Drawable.home);
                    break;
                case "Stats":
                    tab.SetIcon(Resource.Drawable.statistics);
                    break;
                case "Beheer":
                    tab.SetIcon(Resource.Drawable.manage);
                    break;
                case "Logs":
                    tab.SetIcon(Resource.Drawable.logs);
                    break;
            }
        }

        void TabLayout.IOnTabSelectedListener.OnTabUnselected(TabLayout.Tab tab)
        {
            if (tab == null)
            {
                return;
            }
            switch (tab.Text)
            {
                case "Home":
                    tab.SetIcon(Resource.Drawable.home2);
                    break;
                case "Stats":
                    tab.SetIcon(Resource.Drawable.statistics2);
                    break;
                case "Beheer":
                    tab.SetIcon(Resource.Drawable.manage2);
                    break;
                case "Logs":
                    tab.SetIcon(Resource.Drawable.logs2);
                    break;
            }
        }
    }
}