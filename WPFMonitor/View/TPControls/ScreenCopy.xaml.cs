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
using WPFMonitor.Model.ZTControls;
using WPFMonitor.DAL.Sys;
using System.Collections.ObjectModel;
using WPFMonitor.Model.Sys;
using WPFMonitor.Core.TPControls;
using WPFMonitor.DAL.ZTControls;

namespace WPFMonitor.View.TPControls
{
    /// <summary>
    /// ScreenCopy.xaml 的交互逻辑
    /// </summary>
    public partial class ScreenCopy : Window
    {
        public ScreenCopy()
        {
            InitializeComponent();

            Init();
        }

        t_Screen _oldScreen;
        /// <summary>
        /// 场景对象 
        /// </summary>
        public t_Screen oldScreen
        {
            set { _oldScreen = value; }
        }

        ObservableCollection<t_Screen> _StationORList = new ObservableCollection<t_Screen>();
        public ObservableCollection<t_Screen> StationORList
        {
            set { _StationORList = value; }
            get { return _StationORList; }
        }
        public t_Screen SelectStationOR { get; set; }

        private void Init()
        {
            StationORList = new ObservableCollection<t_Screen>();
            foreach (t_Screen obj in ScreenTreeVM.Instance.AllScreens)
            {
                StationORList.Add(obj);               
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectStationOR == null)
            {
                GlobalData.ShowMsgError("请选择目标场景!");
                return;
            }
            try
            {
                new ScreenDA().CopyScreen(SelectStationOR.ScreenID, _oldScreen.ScreenID);
                this.Close();
            }
            catch (Exception ex)
            {
                GlobalData.ShowMsgError(ex.Message);
            }
        }



    }
}
