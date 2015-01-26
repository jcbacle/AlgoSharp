using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using AlgoSharp.PercolationVisualizer.Model;

namespace AlgoSharp.PercolationVisualizer.View
{
    public class SiteColorConverter : IValueConverter
    {
        private readonly SolidColorBrush _defaultSiteColor = new SolidColorBrush(Colors.Black);
        private readonly SolidColorBrush _openSiteColor = new SolidColorBrush(Colors.Cornsilk);
        private readonly SolidColorBrush _fullSiteColor = new SolidColorBrush(Colors.CornflowerBlue);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var site = value as SiteModel;
            if (site == null) return _defaultSiteColor;
            if (site.IsFull) return _fullSiteColor;
            if (site.IsOpen) return _openSiteColor;
            return _defaultSiteColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
