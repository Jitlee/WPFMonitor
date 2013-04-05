using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using WPFMonitor.Model.Linkage;
using WPFMonitor.DAL.Linkage;
using WPFMonitor.Core.ViewModel;
using WPFMonitor.View.Linkage;
using System.Windows.Controls;
using WPFMonitor.DAL.Sys;
using WPFMonitor.Model.Sys;


namespace WPFMonitor.Core.ViewModel.Linkage
{
    public class TimeLinkageEditViewModel : EditBaseViewModel
    {
        TimeLinkageListViewModel _TimeLinkageListVM;
        TimeLinkageOR _SourceObj;
        TimeLinkageEditView _Window;
        public TimeLinkageEditViewModel(TimeLinkageListViewModel _vm, TimeLinkageEditView _mw)
        {
            _TimeLinkageListVM = _vm;
            _Window = _mw;
            OperationType = OpType.Add;
            TimeLinkageObj = new TimeLinkageOR();
            Init();
        }

        public TimeLinkageEditViewModel(TimeLinkageListViewModel _vm, TimeLinkageEditView _mw, TimeLinkageOR _TimeLinkageObj)
        {
            _TimeLinkageListVM = _vm;
            _Window = _mw;
            _SourceObj = _TimeLinkageObj;

            OperationType = OpType.Alert;
            TimeLinkageObj = new TimeLinkageOR();
            TimeLinkageObj.Clone(_TimeLinkageObj);
            Init();
        }

        private void Init()
        {

            _Window.cbStation.SelectionChanged += new SelectionChangedEventHandler(cbStation_SelectionChanged);
            _Window.cbDeviceID.SelectionChanged += new SelectionChangedEventHandler(cbDeviceID_SelectionChanged);

            StationDA _staDA = new StationDA();
            StationORList = _staDA.selectAllStation();

            if (OperationType == OpType.Alert)
            {
                if (StationORList != null && TimeLinkageObj != null)
                {
                    var vS = from f in StationORList where f.Stationid == TimeLinkageObj.Linkagestationid select f;
                    if (vS.Count() != 0)
                        SelectStationOR = vS.First();
                }
            }
        }

        public TimeLinkageOR TimeLinkageObj { get; set; }


        public TimeLinkageEditViewModel(TimeLinkageOR _Sta)
        {
            OperationType = OpType.Alert;
            TimeLinkageObj = _Sta;
        }
        private bool SetValue()
        {
            StringBuilder sbError = new StringBuilder();
            if (SelectChannelOR == null)
            {
                sbError.Append("没有选择设备!");
            }
            else
            {
                TimeLinkageObj.Linkagestationid = SelectStationOR.Stationid;
                TimeLinkageObj.StationName = SelectStationOR.Stationname;

                TimeLinkageObj.Linkagedeviceid = SelectDeviceOR.Deviceid;
                TimeLinkageObj.DeviceName = SelectDeviceOR.Devicename;

                TimeLinkageObj.Linkagechannelno = SelectChannelOR.Channelno;
                TimeLinkageObj.ChannelName = SelectChannelOR.Channelname;
            }
            

            string ErrorMsg = sbError.ToString();
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                ShowMsgError(ErrorMsg);
                return false;
            }

            return true;
        }
        public override void OK()
        {
            string errMsg = "";
            if (GetErrors(_Window.gridContent, out errMsg))
            {
                ShowMsgError(errMsg);
                return;
            }
            if (!SetValue())
                return;
            try
            {
                if (OperationType == OpType.Alert)
                {
                    new TimeLinkageDA().Update(TimeLinkageObj);
                    _SourceObj.Clone(TimeLinkageObj);
                }
                else
                {
                    new TimeLinkageDA().Insert(TimeLinkageObj);
		    
                    _TimeLinkageListVM.Init();
                }
                _Window.Close();
            }
            catch (Exception ex)
            {
                ShowMsgError(ex.Message);
            }

        }

        #region 站点、设备、通首管理
        DeviceDA _DeviceDA = new DeviceDA();

        //站点
        ObservableCollection<StationOR> _StationORList = new ObservableCollection<StationOR>();
        public ObservableCollection<StationOR> StationORList
        {
            set { _StationORList = value; NotifyPropertyChanged("StationORList"); }
            get { return _StationORList; }
        }
        public StationOR SelectStationOR { get; set; }
        public void cbStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectDeviceOR = null;
            SelectDeviceOR = null;

            if (DeviceORList != null)
                DeviceORList.Clear();
            var v = _DeviceDA.selectDevices(SelectStationOR.Stationid.ToString());

            foreach (DeviceOR obj in v)
            {
                DeviceORList.Add(obj);
                if (OperationType == OpType.Alert && TimeLinkageObj != null
                    && obj.Deviceid == TimeLinkageObj.Linkagedeviceid)
                {
                    SelectDeviceOR = obj;
                }
            }
        }
        //设备
        ObservableCollection<DeviceOR> _DeviceORList = new ObservableCollection<DeviceOR>();
        public ObservableCollection<DeviceOR> DeviceORList
        {
            set
            {
                _DeviceORList = value;
                NotifyPropertyChanged("DeviceORList");
            }
            get { return _DeviceORList; }
        }
        public DeviceOR SelectDeviceOR { get; set; }

        public void cbDeviceID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectChannelOR = null;
            if (ChannelORList != null)
                ChannelORList.Clear();
            if (SelectDeviceOR == null)
                return;

            var v = _DeviceDA.selectChannels(SelectDeviceOR.Deviceid.ToString());
            foreach (ChannelOR obj in v)
            {
                ChannelORList.Add(obj);
                if (OperationType == OpType.Alert && TimeLinkageObj != null
                    && obj.Channelno == TimeLinkageObj.Linkagechannelno)
                {
                    SelectChannelOR = obj;
                }
            }
        }
        //通首
        ObservableCollection<ChannelOR> _ChannelORList = new ObservableCollection<ChannelOR>();
        public ObservableCollection<ChannelOR> ChannelORList
        {
            set { _ChannelORList = value; }
            get { return _ChannelORList; }
        }
        public ChannelOR SelectChannelOR { get; set; }


        #endregion
    }
}

