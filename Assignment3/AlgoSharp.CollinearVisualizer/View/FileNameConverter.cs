using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace AlgoSharp.CollinearVisualizer.View
{
    public class FileNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var fileName = value as string;
            return fileName == null ? string.Empty : Path.GetFileName(fileName);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
