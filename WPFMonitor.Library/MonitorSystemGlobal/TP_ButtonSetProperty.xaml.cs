using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace WPFMonitor.Library.MonitorSystemGlobal
{
    public partial class TP_ButtonSetProperty : Window
    {
        MonitorControl BaseCtl;
        public ObservableCollection<ScreenAddShowName> ListScreenShow { get; set; }
        public TP_ButtonSetProperty(MonitorControl _baseCtl)
        {
            InitializeComponent();

            BaseCtl = _baseCtl;
            ListScreenShow=_baseCtl.GetChildScreenObj();
            if (ListScreenShow == null)
                ListScreenShow = new ObservableCollection<ScreenAddShowName>();
            BindList();
            //gvList.DataContext = ListScreenShow;
        }
      
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            BaseCtl.SetChildScreen(ListScreenShow);
            this.DialogResult = true;
        }

        public void BindList()
        {
            gvList.ItemsSource = ListScreenShow;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            TP_ButtonSetPropertyEdit _Win = new TP_ButtonSetPropertyEdit(this,OpType.Add);
            _Win.Show();
        }

        private void btnAlert_Click(object sender, RoutedEventArgs e)
        {
            if (gvList.SelectedItem == null)
            {
                MessageBox.Show("请选择要编辑的场景！");
                return;
            }
            TP_ButtonSetPropertyEdit _Win = new TP_ButtonSetPropertyEdit(this, OpType.Alert);
            _Win.Show();
        }



        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (gvList.SelectedItem == null)
            {
                MessageBox.Show("请选择要编辑的场景！");
                return;
            }
            ScreenAddShowName mobj = (ScreenAddShowName)gvList.SelectedItem;
            if (MessageBox.Show(string.Format("你确定要删除关联：{0}", mobj.ScreenName),
                  "询问", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                ListScreenShow.Remove(mobj);
            }

        }
    }
}

