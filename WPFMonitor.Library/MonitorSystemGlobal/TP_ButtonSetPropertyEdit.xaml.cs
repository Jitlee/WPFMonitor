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
using WPFMonitor.Model;
using WPFMonitor.Model.ZTControls;

namespace WPFMonitor.Library.MonitorSystemGlobal
{
    public partial class TP_ButtonSetPropertyEdit : Window
    {
        TP_ButtonSetProperty _ContentX;
        OpType _OpType;
        ScreenAddShowName OpItem;
        public TP_ButtonSetPropertyEdit(TP_ButtonSetProperty _BaseWindow,OpType mopType)
        {
            InitializeComponent();
            _OpType = mopType;

            _ContentX = _BaseWindow;

            InitProperty();
            if (mopType == OpType.Alert)
            {
                OpItem = (ScreenAddShowName)_ContentX.gvList.SelectedItem;
                txtShowName.Text = OpItem.ScreenShowName;
                cbScreenList.SelectedItem = OpItem.Screen;
            }
        }
        private void InitProperty()
        {
            this.cbScreenList.ItemsSource = MonitorControl.ScreenList;
            this.cbScreenList.DisplayMemberPath = "ScreenName";
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtShowName.Text))
            {
                MessageBox.Show("请输入显示名称。");
                return;
            }

            if (cbScreenList.SelectedItem == null)
            {
                MessageBox.Show("请选择场景。");
                return;
            }
            
            if (_OpType == OpType.Add)
            {
                ScreenAddShowName sshow = new ScreenAddShowName();
                t_Screen _Screen = (t_Screen)cbScreenList.SelectedItem;

                sshow.ScreenShowName = txtShowName.Text;               
                sshow.ScreenName = _Screen.ScreenName;
                sshow.Screen = _Screen;
                _ContentX.ListScreenShow.Add(sshow);
            }
            else
            {
              var v=  _ContentX.ListScreenShow.Single(a => a.Screen == OpItem.Screen &&
                    a.ScreenName == OpItem.ScreenName && a.ScreenShowName == OpItem.ScreenShowName);


                t_Screen _Screen = (t_Screen)cbScreenList.SelectedItem;
                v.ScreenShowName = txtShowName.Text;
                v.ScreenName = _Screen.ScreenName;
                v.Screen = _Screen;

                _ContentX.BindList();
                //_ContentX.gvList.sh
            }
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

