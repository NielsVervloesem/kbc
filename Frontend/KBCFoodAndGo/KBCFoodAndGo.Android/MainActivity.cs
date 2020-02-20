using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using ContextMenu.Droid;
using FFImageLoading.Forms.Platform;

namespace KBCFoodAndGo.Droid
{
    [Activity(Label = "KBCFoodAndGo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait,MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);


            base.OnCreate(savedInstanceState);
            Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            Xamarin.Forms.Forms.SetFlags("CarouselView_Experimental");

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CachedImageRenderer.InitImageViewHandler();
            UserDialogs.Init(this);
            ContextMenuViewRenderer.Preserve();

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}