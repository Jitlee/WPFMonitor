using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DockingLibrary;

namespace WPFMonitor.View.TPControls
{
    /// <summary>
    /// LoadScreen.xaml 的交互逻辑
    /// </summary>
    public partial class LoadScreen : DockableContent
    {
        public LoadScreen()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(LoadScreen_Loaded);
        }

        void LoadScreen_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void ThumbnailBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
