using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using WPFMonitor.Core.Util;
using System.Windows.Media.Imaging;

namespace WPFMonitor.Controls
{
   public class QhQueueInfo : UserControl
    {
       Grid g = new Grid();
       TextBlock txt = new TextBlock();
       /// <summary>
       /// 当前队列
       /// </summary>
       Image _imgCurrent = null;
       public QhQueueInfo()
       {
           g.ColumnDefinitions.Add(new ColumnDefinition());
           g.ColumnDefinitions.Add(new ColumnDefinition());
           g.RowDefinitions.Add(new RowDefinition());
           g.RowDefinitions.Add(new RowDefinition());


           txt.SetValue(Grid.ColumnProperty, 1);
           txt.Text = "txt";
           txt.SetValue(Grid.RowProperty, 1);
           txt.SetValue(Grid.ColumnProperty, 1);
           
           string url = "/WPFMonitor;component/Resources/Images/person.png";
           BitmapImage bitmap = new BitmapImage(new Uri(url, UriKind.Relative));
           ImageSource mm = bitmap;
           Image _img = new Image();
           _img.Source = mm;
           _img.SetValue(Grid.RowProperty, 1);
           
           this.Content = g;

           g.Children.Add(_img);
           g.Children.Add(txt);
           this.Background = new SolidColorBrush();
       }

       #region  属性

       #region ShowContent
       private static readonly DependencyProperty ShowContentProperty =
           DependencyProperty.Register("ShowContent",
           typeof(string), typeof(QhQueueInfo), new PropertyMetadata("", ShowContentPropertyChanged));
       public string ShowContent
       {
           set
           {
               txt.Text = value;
           }

       }
       private static void ShowContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
       {
           var element = d as QhQueueInfo;
           if (null != element)
           {
               element.ShowContent = e.NewValue.ToString();
           }
       }
       #endregion

       #region IsNowQueue
       private static readonly DependencyProperty IsNowQueueProperty =
           DependencyProperty.Register("IsNowQueue",
           typeof(string), typeof(QhQueueInfo), new PropertyMetadata("", IsNowQueuePropertyChanged));
       public bool IsNowQueue
       {
           set
           {
               if (value)
               {
                   if (_imgCurrent == null)
                   {
                       _imgCurrent = new Image();
                       string url = "/WPFMonitor;component/Resources/Images/arrow.png";
                       BitmapImage bitmap = new BitmapImage(new Uri(url, UriKind.Relative));
                       ImageSource mm = bitmap;
                       
                       _imgCurrent.Source = mm;
                       _imgCurrent.SetValue(Grid.ColumnProperty, 1);
                       g.Children.Add(_imgCurrent);
                   }
                   _imgCurrent.Visibility = Visibility.Visible;
               }
               else
               {
                   
                   if (_imgCurrent != null)
                   {
                       _imgCurrent.Visibility = Visibility.Collapsed;
                   }
               }
           }

       }
       private static void IsNowQueuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
       {
           var element = d as QhQueueInfo;
           if (null != element)
           {
               element.IsNowQueue =Convert.ToBoolean( e.NewValue);
           }
       }
       #endregion

       #endregion
    }
}
