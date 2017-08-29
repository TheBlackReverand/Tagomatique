using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Tagomatique.Tools.Extensions;
using Tagomatique.Supplies;

namespace Tagomatique.View.Converters
{
    public class BitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(ImageSource))
                return null;

            if (!(value is string) || String.IsNullOrEmpty((string)value))
                return null;

            Bitmap bitmap = Pictures.ResourceManager.GetObject((string)value, culture) as Bitmap;

            return bitmap != null ? bitmap.ToBitmapSource() : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}