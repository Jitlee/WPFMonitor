using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using WPFMonitor.DAL.Alarm;
using WPFMonitor.DAL.Sys;
using WPFMonitor.Model.Alarm;
using WPFMonitor.Model.Sys;
using WPFMonitor.View.Alarm;


namespace WPFMonitor.Core.ViewModel.Alarm
{
    public class AlramBindTimeEditViewModel : EditBaseViewModel
    {
        AlramBindTimeListViewModel _AlramBindTimeListVM;
        AlramBindTimeOR _SourceObj;
        AlramBindTimeEditView _Window;
        public AlramBindTimeEditViewModel(AlramBindTimeListViewModel _vm, AlramBindTimeEditView _mw)
        {
            _AlramBindTimeListVM = _vm;
            _Window = _mw;
            
            OperationType = OpType.Add;            
            AlramBindTimeObj = new AlramBindTimeOR();
            Init();
        }

        public AlramBindTimeEditViewModel(AlramBindTimeListViewModel _vm, AlramBindTimeEditView _mw, AlramBindTimeOR _AlramBindTimeObj)
        {
            _AlramBindTimeListVM = _vm;
            _Window = _mw;
           

            _SourceObj = _AlramBindTimeObj;
            OperationType = OpType.Alert;
            AlramBindTimeObj = new AlramBindTimeOR();
            AlramBindTimeObj.Clone(_AlramBindTimeObj);
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
                if (StationORList != null && AlramBindTimeObj != null)
                {
                    var vS = from f in StationORList where f.Stationid == AlramBindTimeObj.Stationid select f;
                    if (vS.Count() != 0)
                        SelectStationOR = vS.First();
                }
            }
        }



        /// <summary>
        /// 当前站点
        /// </summary>
        public AlramBindTimeOR AlramBindTimeObj { get; set; }


        public AlramBindTimeEditViewModel(AlramBindTimeOR _Sta)
        {
            OperationType = OpType.Alert;
            AlramBindTimeObj = _Sta;
        }

        

        public override void OK()
        {
            if (SelectDeviceOR == null || SelectDeviceOR == null || SelectChannelOR == null)
            {
                ShowMsgError("请选择通道");
                return;
            }

            string errMsg = "";
            if (GetErrors(_Window.gridContent, out errMsg))
            {
                ShowMsgError(errMsg);
                return;
            }
            AlramBindTimeObj.Stationid = SelectStationOR.Stationid;
            AlramBindTimeObj.StationName = SelectStationOR.Stationname;

            AlramBindTimeObj.Deviceid = SelectDeviceOR.Deviceid;
            AlramBindTimeObj.DeviceName = SelectDeviceOR.Devicename;

            AlramBindTimeObj.Channelno = SelectChannelOR.Channelno;
            AlramBindTimeObj.ChannelName = SelectChannelOR.Channelname;
            //
            try
            {
                if (OperationType == OpType.Alert)
                {
                    new AlramBindTimeDA().Update(AlramBindTimeObj);
                    _SourceObj.Clone(AlramBindTimeObj);
                }
                else
                {
                    new AlramBindTimeDA().Insert(AlramBindTimeObj);
                    //_AlramBindTimeListVM.AlramBindTimeORList.Add(AlramBindTimeObj);
                    _AlramBindTimeListVM.Init();
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
                if (OperationType == OpType.Alert && AlramBindTimeObj != null
                    && obj.Deviceid == AlramBindTimeObj.Deviceid)
                {
                    SelectDeviceOR = obj;
                }
            }
        }
        //设备
        ObservableCollection<DeviceOR> _DeviceORList = new ObservableCollection<DeviceOR>();
        public ObservableCollection<DeviceOR> DeviceORList
        {
            set {
                _DeviceORList = value;
                NotifyPropertyChanged("DeviceORList");
            }
            get{ return _DeviceORList; }
        }
        public DeviceOR SelectDeviceOR { get; set; }

        public void cbDeviceID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectChannelOR = null;
            if (ChannelORList != null)
                ChannelORList.Clear();
            if (SelectDeviceOR == null)
                return;

            var v = _DeviceDA.selectChannels(SelectDeviceOR.Deviceid);
            foreach (ChannelOR obj in v)
            {
                ChannelORList.Add(obj);
                if (OperationType == OpType.Alert && AlramBindTimeObj != null
                    && obj.Channelno == AlramBindTimeObj.Channelno)
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

