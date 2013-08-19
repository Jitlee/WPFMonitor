using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Text.RegularExpressions;
using WPFMonitor.DAL;
using WPFMonitor.Library.Controls.ImagesManager;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Imaging;

namespace MonitorSystem.Controls
{
    public class ImageVaueEditor : ValueEditorBase
    {
        private Grid _grid = new Grid();
        private Image _image = new Image() { Height = 16d, Width = 16d, HorizontalAlignment = HorizontalAlignment.Center };
        private TextBox _text = new TextBox();
        private Button _broswerButton = new Button() { Width = 30, Content="..." };
        private Button _removeButton = new Button() { Width = 30, Content="×", IsEnabled = false, Foreground=new SolidColorBrush(Colors.Red) };
        private ImageAttribute _attribute;
        private readonly static Brush _grayBrush = new SolidColorBrush(Colors.Gray);
        private readonly static Brush _blackBrush = new SolidColorBrush(Colors.Black);

        public ImageVaueEditor(PropertyGridLabel label, PropertyItem property)
            : base(label, property)
        {

            property.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(property_PropertyChanged);
            property.ValueError += new EventHandler<ExceptionEventArgs>(property_ValueError);

            _attribute = property.GetAttribute<ImageAttribute>();

            if(null == _attribute)
            {
                _attribute = new ImageAttribute();
                _attribute.OnlyImage = false;
            }
            _grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1d, GridUnitType.Auto) });
            _grid.ColumnDefinitions.Add(new ColumnDefinition());
            _grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1d, GridUnitType.Auto) });
            _grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1d, GridUnitType.Auto) });
            _grid.Children.Add(_image);
            _grid.Children.Add(_text);
            _grid.Children.Add(_removeButton);
            _grid.Children.Add(_broswerButton);
            _image.SetValue(Grid.ColumnProperty, 0);
            _text.SetValue(Grid.ColumnProperty, 1);
            _removeButton.SetValue(Grid.ColumnProperty, 2);
            _broswerButton.SetValue(Grid.ColumnProperty, 3);
            this.Content = _grid;
            _broswerButton.Click += BroswerButton_Click;
            _removeButton.Click += RemoveButton_Click;
            UpdateLabel(property.Value);
            _text.GotFocus += Text_GotFocus;
            _text.Background = null;
            _text.BorderBrush = null;
            _text.BorderThickness = new Thickness();
        }

        private void Text_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_text.Text.Length > 0)
            {
                _text.Select(0, _text.Text.Length);
            }
        }

        private void UpdateLabel(object value)
        {
            if (null != value)
            {
                string url = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _attribute.Path, value.ToString());
                _text.Text = value.ToString();
                _text.Foreground = _blackBrush;
                _removeButton.IsEnabled = true;
                if (!string.IsNullOrEmpty(_attribute.Path) && File.Exists(url))
                {
                    // 文件夹固定
                    _image.Source = new BitmapImage(new Uri(url, UriKind.Absolute));
                    return;
                }
            }
            else
            {
                _text.Text = "没有图片";
                _text.Foreground = _grayBrush;
                _removeButton.IsEnabled = false;
            }
            _image.Source = ImagePathConverter.Convert(value);
        }

        void property_ValueError(object sender, ExceptionEventArgs e)
        {
            MessageBox.Show(e.EventException.Message);
        }

        void property_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                UpdateLabel(Property.Value);
            }
        }

        void BroswerButton_Click(object sender, RoutedEventArgs e)
        {
            //new ImagesBrowseWindow(ImageSelection_Changed, _attribute.Path, _attribute.OnlyImage).Show();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "图片(*.jpg;*.bmp;*.png)|*.jpg;*.bmp;*.png";
            if (dlg.ShowDialog() == true)
            {
                Property.Value = CopyFile(dlg.FileName, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _attribute.Path, new FileInfo(dlg.FileName).Name));
            }
            _removeButton.IsEnabled = true;
        }

        public static string CopyFile(string from, string to)
        {
            FileInfo info = new FileInfo(to);
            if (info.Exists)
            {
                string patten = @"(?<=\()[0-9]+(?=\)\.\w+$)";
                Match match = Regex.Match(info.Name, patten);
                if (match.Success)
                {
                    to = Regex.Replace(to, patten, (Convert.ToInt32(match.Value) + 1).ToString());
                }
                else
                {
                    to = to.Insert(to.Length - info.Extension.Length, "(1)");
                }
                return CopyFile(from, to);
            }
            try
            {
                if (!Directory.Exists(info.DirectoryName))
                {
                    info.Directory.Create();
                }
                File.Copy(from, to);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "设置图片出错", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return info.Name;
        }

        void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Property.Value = string.Empty;
            _removeButton.IsEnabled = false;
        }

        //private void ImageSelection_Changed(FileModel file)
        //{
        //    // 固定文件夹,只需要图片名称，否则需要整个Url
        //    if (_attribute.OnlyImage)
        //    {
        //        Property.Value = file.Name;
        //    }
        //    else
        //    {
        //        Property.Value = file.Url.Remove(0, _attribute.Path.Length).Trim('/');
        //    }
        //    _removeButton.IsEnabled = true;
        //}
    }
}
