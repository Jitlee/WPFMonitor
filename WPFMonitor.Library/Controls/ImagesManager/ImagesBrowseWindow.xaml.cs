using System;
using System.Windows;
using WPFMonitor.Model;

namespace WPFMonitor.Library.Controls.ImagesManager
{
    public partial class ImagesBrowseWindow : Window
    {
        private Action<FileModel> _callback;
        private string _path;
        private bool _onlyImage;
        public ImagesBrowseWindow(Action<FileModel> callback, string path = "", bool onlyImage = false)
        {
            _callback = callback;
            _path = path;
            _onlyImage = onlyImage;
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            this.Loaded -= OnLoaded;
            this.DataContext = new ImagesBrowseViewModel(Callback, _path, _onlyImage);
        }

        private void Callback(FileModel file)
        {
            if (null != _callback)
            {
                _callback(file);
            }
            this.DialogResult = true;
        }

        //private void OKButton_Click(object sender, RoutedEventArgs e)
        //{
        //    this.DialogResult = true;
        //}

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

