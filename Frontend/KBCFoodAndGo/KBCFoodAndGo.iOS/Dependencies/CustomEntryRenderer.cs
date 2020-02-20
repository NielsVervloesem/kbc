using KBCFoodAndGo.iOS.Dependencies;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace KBCFoodAndGo.iOS.Dependencies
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            Control.TintColor = Color.FromHex("#00AEEF").ToUIColor();
        }
    }
}