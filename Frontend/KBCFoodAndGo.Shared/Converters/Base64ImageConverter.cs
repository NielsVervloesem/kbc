using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace KBCFoodAndGo.Shared.Converters
{
    public class Base64ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(value as string))
            {
                string base64Image = (string)value;
                var imageBytes = System.Convert.FromBase64String(base64Image);
                return ImageSource.FromStream(() => new MemoryStream(imageBytes));
            }
            return ImageSource.FromFile("defaultImage.png");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}