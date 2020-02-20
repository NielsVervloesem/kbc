using System;
using System.Globalization;
using Xamarin.Forms;

namespace KBCFoodAndGo.Shared.Converters
{
    public class MealCurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "€" + value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (((string) value).Contains("€"))
            {
                var convertedValue = ((string)value).Substring(1, ((string)value).Length - 1);
                return convertedValue;
            }
            return value;
        }
    }
}
