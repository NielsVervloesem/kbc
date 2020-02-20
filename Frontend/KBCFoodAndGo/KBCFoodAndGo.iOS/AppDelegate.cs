using ContextMenu.iOS;
using FFImageLoading.Forms.Platform;
using Foundation;
using UIKit;

namespace KBCFoodAndGo.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            global::Xamarin.Forms.Forms.Init();
            CachedImageRenderer.InitImageSourceHandler();
            ContextMenuViewRenderer.Preserve();
            LoadApplication(new App());
            return base.FinishedLaunching(uiApplication, launchOptions);
        }
    }
}
