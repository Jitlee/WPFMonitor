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
    /// ControlWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ControlWindow : DockableContent
    {
        public ControlWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ControlWindow_Loaded);
        }

        void ControlWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ControlVM.Instance;
        }

        void Control_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
