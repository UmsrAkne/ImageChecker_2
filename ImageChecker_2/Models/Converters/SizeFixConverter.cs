using System;
using System.Globalization;
using System.Windows.Data;

namespace ImageChecker_2.Models.Converters.Converters
{
    public class SizeFixConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? (double)value - 40 : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}