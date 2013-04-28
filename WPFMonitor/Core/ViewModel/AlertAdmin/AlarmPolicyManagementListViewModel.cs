using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using WPFMonitor.Model.AlertAdmin;
using WPFMonitor.DAL.AlertAdmin;
using WPFMonitor.Core.ViewModel;
using WPFMonitor.View.AlertAdmin;
using WPFMonitor.DAL.Sys;
using WPFMonitor.Model.Sys;
using System.Windows.Controls;
using System.Windows.Media;


namespace WPFMonitor.Core.ViewModel.AlertAdmin
{
    public class AlarmPolicyManagementListViewModel : ListBaseViewModel
    {
        ObservableCollection<AlarmPolicyManagementOR> _AlarmPolicyManagementORList = null;

        public ObservableCollection<AlarmPolicyManagementOR> AlarmPolicyManagementORList
        {
            get { return _AlarmPolicyManagementORList; }
        }

        AlarmPolicyManagementListView _Window;
        public AlarmPolicyManagementListViewModel(AlarmPolicyManagementListView mWin)
        {
            _Window = mWin;
            Init();
        }


        public void Init()
        {
            _Window.cbStation.SelectionChanged += new SelectionChangedEventHandler(cbStation_SelectionChanged);
            StationDA _staDA = new StationDA();
            StationORList = _staDA.selectAllStation();
            if (StationORList != null && StationORList.Count> 0)
            {
                SelectStationOR = StationORList.First();
            }
        }

        #region 树处理
        DeviceDA _DeviceDA = new DeviceDA();
        //站点
        ObservableCollection<StationOR> _StationORList = new ObservableCollection<StationOR>();
        public ObservableCollection<StationOR> StationORList
        {
            set { _StationORList = value;  }
            get { return _StationORList; }
        }
        public StationOR SelectStationOR { get; set; }

        public void cbStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectStationOR != null)
                Load(SelectStationOR.Stationid.ToString());
           
        }
        private void Load(string stationID)
        {
            var v = _DeviceDA.GetAllGenerdDevice(stationID);
            if (v != null)
            {
                _Window.listDevices.Items.Clear();
                _Window._ManagementList.DataContext = new AlarmPolicyManagementEditViewModel();
                foreach (DeviceAddTypeOR obj in v)
                {
                    TreeViewItem tvi = new TreeViewItem();
                    tvi.Header = obj.Devicename;
                    LoadChannce(obj, tvi);
                    _Window.listDevices.Items.Add(tvi);
                }
            }
        }
        SolidColorBrush RedSolid = new SolidColorBrush(Colors.Red);
        SolidColorBrush BlueSolid = new SolidColorBrush(Colors.Blue);
        private void LoadChannce(DeviceAddTypeOR mDevice, TreeViewItem tvi)
        {
            var arrs = _DeviceDA.SelectChannelManagements(mDevice.Deviceid, mDevice.Stationid, mDevice.DeviceTypeID);
            foreach (ChannelManagementOR obj in arrs)
            {
                TreeViewItem tvChanncel = new TreeViewItem();
                tvChanncel.Header = obj.Channelname;
                tvChanncel.Selected+=new RoutedEventHandler(tvChanncel_Selected);
                tvChanncel.Tag = obj;
                if (obj.ISHavePolice)
                {
                    tvChanncel.Foreground = BlueSolid;
                }
                else
                {
                    tvChanncel.Foreground = RedSolid;
                }
                tvi.Items.Add(tvChanncel);
            }
        }
        private TreeViewItem SelectTreeItem;
        protected void tvChanncel_Selected(object sender, RoutedEventArgs e)
        {
           TreeViewItem itemv= sender as TreeViewItem;
           if (itemv != null)
           {
               SelectTreeItem = itemv;
               InitData(itemv.Tag as ChannelManagementOR);
           }
        }

        public void SetSelectTreeItem()
        {
            if (SelectTreeItem != null)
                SelectTreeItem.Foreground = BlueSolid;
        }
        #endregion

        public void ClearData()
        {
            if (SelectTreeItem != null)
                SelectTreeItem.Foreground = RedSolid;
        }

        #region 数据管理        
        public void InitData(ChannelManagementOR obj)
        {
            if (obj == null)
                return;
            _Window._ManagementList.DataContext =new AlarmPolicyManagementEditViewModel(obj,this);
        }
        #endregion


        public override void Insert()
        {           
        }

        public override void Update(object par)
        {
        }

        public override void Delete(object parameter)
        {           
        }
    }
}

