using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;

namespace MonitorSystem.Controls
{
    public class SceneBackgroundPanel : Grid
    {
        private const string PATH = "ImageMap";
        public static DependencyProperty BgImagePathProperty =
           DependencyProperty.Register("BgImagePath", typeof(string), typeof(SceneBackgroundPanel), new PropertyMetadata(null, OnBgImagePathPropertyChanged));

        [Image(PATH, OnlyImage=true)]
        public string BgImagePath
        {
            get { return (string)GetValue(BgImagePathProperty); }
            set { SetValue(BgImagePathProperty, value); }
        }

        static void OnBgImagePathPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var canvas = d as SceneBackgroundPanel;
            canvas.OnBgImagePath_Changed((string)e.OldValue, (string)e.NewValue);
        }

        private ImageBrush _backgroundBrush = new ImageBrush() { Stretch = Stretch.None, AlignmentX = AlignmentX.Left, AlignmentY = AlignmentY.Top };
        void OnBgImagePath_Changed(string oldValue, string newValue)
        {
            //_backgroundBrush.ImageSource = ImagePathConverter.Convert(PATH + "\\" + newValue);
            //if (this.Background != _backgroundBrush)
            //{
            //    this.Background = _backgroundBrush;
            //}
        }
    }
}
