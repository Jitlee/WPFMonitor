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
using WPFMonitor.Core.ViewModel.TPControls;
using WPFMonitor.Model.ZTControls;

namespace WPFMonitor.View.TPControls
{
    /// <summary>
    /// ScreenEdit.xaml 的交互逻辑
    /// </summary>
    public partial class ScreenEdit : Window
    {
        public ScreenEdit(t_Screen _mScreen,OpType mType)
        {
            InitializeComponent();
            this.DataContext = new ScreenViewModel(_mScreen, mType,this);
        }
        //public ScreenEdit(t_Screen _mScreenObj, t_Screen _mParentScreenObj)
        //{
        //    InitializeComponent();
        //    this.DataContext = new ScreenViewModel(_mScreenObj, _mParentScreenObj);
        //}
    }
}
