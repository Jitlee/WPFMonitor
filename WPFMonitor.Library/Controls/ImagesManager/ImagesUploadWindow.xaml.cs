using System.Windows;

namespace WPFMonitor.Library.Controls.ImagesManager
{
    public partial class ImagesUploadWindow : Window
    {
        public ImagesUploadWindow()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

