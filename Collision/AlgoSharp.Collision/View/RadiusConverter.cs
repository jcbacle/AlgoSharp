using System;
using System.Globalization;
using System.Windows.Data;

namespace AlgoSharp.Collision.View
{
    public class RadiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((double)value)*2*100;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}