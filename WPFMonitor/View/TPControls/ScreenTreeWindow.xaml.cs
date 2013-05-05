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
using WPFMonitor.Core.TPControls;

namespace WPFMonitor.View.TPControls
{
    /// <summary>
    /// ScreenTreeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ScreenTreeWindow : DockableContent
    {
        public ScreenTreeWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ScreenTreeWindow_Loaded);
        }

        void ScreenTreeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ScreenTreeVM.Instance;
        }
    }
}
