using Xamarin.Forms;

namespace KBCFoodAndGo.Helpers
{
    public class CustomMealViewCell : ViewCell
    {
        public static readonly BindableProperty SelectedItemBackgroundColorProperty =
            BindableProperty.Create("SelectedItemBackgroundColor", typeof(Color), typeof(CustomMealViewCell));

        public Color SelectedItemBackgroundColor
        {
            get => (Color)GetValue(SelectedItemBackgroundColorProperty);
            set => SetValue(SelectedItemBackgroundColorProperty, value);
        }
    }
}