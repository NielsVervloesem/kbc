using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using KBCFoodAndGo.Droid.Dependencies;
using System.ComponentModel;
using KBCFoodAndGo.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(CustomMealViewCell), typeof(CustomViewCellRenderer))]

namespace KBCFoodAndGo.Droid.Dependencies
{
    public class CustomViewCellRenderer : ViewCellRenderer
    {
        private Android.Views.View _cellCore;
        private Drawable _unselectedBackground;
        private bool _selected;
        protected override View GetCellCore(Cell item, View convertView, ViewGroup parent, Context context)
        {
            _cellCore = base.GetCellCore(item, convertView, parent, context);
            _selected = false;
            _unselectedBackground = _cellCore.Background;
            return _cellCore;
        }
        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnCellPropertyChanged(sender, e);
            if (e.PropertyName == "IsSelected")
            {
                _selected = !_selected;
                if (_selected)
                {
                    if (sender is CustomMealViewCell extendedViewCell)
                        _cellCore.SetBackgroundColor(extendedViewCell.SelectedItemBackgroundColor.ToAndroid());
                }
                else
                {
                    _cellCore.SetBackground(_unselectedBackground);
                }
            }
        }
    }
}