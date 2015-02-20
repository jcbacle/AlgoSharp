using System;
using System.Globalization;
using System.Windows.Data;
using AlgoSharp.Collision.Service;

namespace AlgoSharp.Collision.View
{
    public class RyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var particule = value as Particule;
            if (particule == null) return null;

            return (particule.Ry - particule.Radius)*100;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}