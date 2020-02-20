using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using KBCFoodAndGo.Droid.Dependencies;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]

namespace KBCFoodAndGo.Droid.Dependencies
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            IntPtr intTextView= JNIEnv.FindClass(typeof(TextView));
            IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(intTextView, "mCursorDrawableRes", "I");

            JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Drawable.custom_cursor);

            Control?.SetBackgroundColor(global::Android.Graphics.Color.White);
            Control.Background = Android.App.Application.Context.GetDrawable(Resource.Drawable.custom_entry);
            Control.Gravity = GravityFlags.CenterVertical;
            Control.SetPadding(30, 5, 0, 5);
            Control.SetHeight(75);
        }
    }
}