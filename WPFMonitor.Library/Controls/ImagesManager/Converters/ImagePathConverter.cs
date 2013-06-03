using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace WPFMonitor.Library.Controls.ImagesManager
{
    public class ImagePathConverter : IValueConverter
    {
        //private static readonly string _root = (System.Windows.Browser.HtmlPage.IsEnabled == true) ? Application.Current.Host.Source.AbsoluteUri.Remove(Application.Current.Host.Source.AbsoluteUri.IndexOf(Application.Current.Host.Source.AbsolutePath)) : string.Empty;
        //private static readonly ImageSource _defaultFileImage = new BitmapImage(new Uri("/WPFMonitor.Library;component/Controls/ImagesManager/Images/file.jpg", UriKind.RelativeOrAbsolute));
        //private static readonly ImageSource _defaultDirectoryImage = new BitmapImage(new Uri("/WPFMonitor.Library;component/Controls/ImagesManager/Images/drectory.png", UriKind.RelativeOrAbsolute));
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Convert(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Convert(value);
        }

        public static ImageSource Convert(object value)
        {
            return null;
        //    if (null != value && !string.IsNullOrEmpty(value.ToString()) && null != _root)
        //    {
        //        try
        //        {
        //            if (value.ToString() == "Directory")
        //            {
        //                return _defaultDirectoryImage;
        //            }
        //            else
        //            {
        //                return new BitmapImage(new Uri(Application.Current.Host.Source, string.Concat("../Upload/", value.ToString().Replace("\\", "/").Trim('/'))));
        //            }
        //        }
        //        catch { };
        //    }
        //    return _defaultFileImage;
        }
    }
}
