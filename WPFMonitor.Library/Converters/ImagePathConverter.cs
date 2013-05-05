using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WPFMonitor.Library.Converters
{
    public class ImagePathConverter : IValueConverter
    {
        private static readonly ImagePathConverter _instance = new ImagePathConverter();
        public static ImagePathConverter Instance { get { return _instance; } }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter is string)
            {
                try
                {
                    return new BitmapImage(new Uri(string.Format(parameter.ToString(), value), UriKind.RelativeOrAbsolute));
                }
                catch
                {
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}