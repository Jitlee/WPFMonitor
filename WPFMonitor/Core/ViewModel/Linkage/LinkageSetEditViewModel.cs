using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using WPFMonitor.Model.Linkage;
using WPFMonitor.DAL.Linkage;
using WPFMonitor.Core.ViewModel;
using WPFMonitor.View.Linkage;
using WPFMonitor.DAL.Sys;
using WPFMonitor.Model.Sys;
using System.Windows.Controls;
using WPFMonitor.Model;


namespace WPFMonitor.Core.ViewModel.Linkage
{
    public class LinkageSetEditViewModel : EditBaseViewModel
    {
        LinkageSetListViewModel _LinkageSetListVM;
        LinkageSetOR _SourceObj;
        LinkageSetEditView _Window;
        public LinkageSetEditViewModel(LinkageSetListViewModel _vm, LinkageSetEditView _mw)
        {
            _LinkageSetListVM = _vm;
            _Window = _mw;
            OperationType = OpType.Add;
            LinkageSetObj = new LinkageSetOR();
            Init();
        }

        public LinkageSetEditViewModel(LinkageSetListViewModel _vm, LinkageSetEditView _mw, LinkageSetOR _LinkageSetObj)
        {
            _LinkageSetListVM = _vm;
            _Window = _mw;
            _SourceObj = _LinkageSetObj;

            OperationType = OpType.Alert;
            LinkageSetObj = new LinkageSetOR();
            LinkageSetObj.Clone(_LinkageSetObj);
            Init();
        }

        private void Init()
        {
            _Window.cbStation.SelectionChanged += new SelectionChangedEventHandler(cbStation_SelectionChanged);
            _Window.cbDeviceID.SelectionChanged += new SelectionChangedEventHandler(cbDeviceID_SelectionChanged);

            _Window.cbLinkageStation.SelectionChanged += new SelectionChangedEventHandler(cbLinkageStation_SelectionChanged);
            _Window.cbLinkageDeviceID.SelectionChanged += new SelectionChangedEventHandler(cbLinkageDeviceID_SelectionChanged);

            StationDA _staDA = new StationDA();
            var v = _staDA.selectAllStation();
            StationLinkageORList = new ObservableCollection<StationOR>();
            StationORList = new ObservableCollection<StationOR>();

            foreach (StationOR obj in v)
            {
                StationLinkageORList.Add(obj);
                StationORList.Add(obj);
                if (OperationType == OpType.Alert)
                {
                    if (obj.Stationid == LinkageSetObj.Stationid)
                        SelectStationOR = obj;
                    if (obj.Stationid == LinkageSetObj.Linkagestationid)
                        SelectLinkageStationOR = obj;
                }
            }
            if (OperationType == OpType.Alert)
            {
                cbStation_SelectionChanged(null, null);
                cbDeviceID_SelectionChanged(null, null);

                cbLinkageStation_SelectionChanged(null, null);
                cbLinkageDeviceID_SelectionChanged(null, null);
            }

            //值类型
            VlueTypeORList = new ObservableCollection<StatusOR>() { 
            new StatusOR(){ ID=0, Name="模拟量"},
            new StatusOR(){ ID=1, Name="开关量"},
            new StatusOR(){ ID=2, Name="控制量"}
            };

            if (OperationType == OpType.Alert && LinkageSetObj != null)
            {
                var vValueT = from f in VlueTypeORList where f.ID == LinkageSetObj.Valuetype select f;
                if (vValueT.Count() > 0)
                    SelectVlueTypeOR = vValueT.First();
            }
        }

        public LinkageSetOR LinkageSetObj { get; set; }

        public ObservableCollection<StatusOR> VlueTypeORList { get; set; }
        public StatusOR SelectVlueTypeOR { get; set; }

        public LinkageSetEditViewModel(LinkageSetOR _Sta)
        {
            OperationType = OpType.Alert;
            LinkageSetObj = _Sta;
        }
        #region 站点、设备、通首管理
        DeviceDA _DeviceDA = new DeviceDA();
        #region 设备
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
                if (OperationType == OpType.Alert && LinkageSetObj != null
                    && obj.Deviceid == LinkageSetObj.Deviceid)
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

            var v = _DeviceDA.selectChannels(SelectDeviceOR.Deviceid);
            foreach (ChannelOR obj in v)
            {
                ChannelORList.Add(obj);
                if (OperationType == OpType.Alert && LinkageSetObj != null
                    && obj.Channelno == LinkageSetObj.Channelno)
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
        #region 联动设备
        //站点
        ObservableCollection<StationOR> _StationLinkageORList = new ObservableCollection<StationOR>();
        public ObservableCollection<StationOR> StationLinkageORList
        {
            set { _StationLinkageORList = value; NotifyPropertyChanged("StationLinkageORList"); }
            get { return _StationLinkageORList; }
        }
        public StationOR SelectLinkageStationOR { get; set; }
        public void cbLinkageStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectLinkageDeviceOR = null;
            SelectLinkageDeviceOR = null;

            if (DeviceLinkageORList != null)
                DeviceLinkageORList.Clear();
            var v = _DeviceDA.selectDevices(SelectLinkageStationOR.Stationid.ToString());

            foreach (DeviceOR obj in v)
            {
                DeviceLinkageORList.Add(obj);
                if (OperationType == OpType.Alert && LinkageSetObj != null
                    && obj.Deviceid == LinkageSetObj.Linkagedeviceid)
                {
                    SelectLinkageDeviceOR = obj;
                }
            }
        }
        //设备
        ObservableCollection<DeviceOR> _DeviceLinkageORList = new ObservableCollection<DeviceOR>();
        public ObservableCollection<DeviceOR> DeviceLinkageORList
        {
            set
            {
                _DeviceLinkageORList = value;
                NotifyPropertyChanged("DeviceLinkageORList");
            }
            get { return _DeviceLinkageORList; }
        }
        public DeviceOR SelectLinkageDeviceOR { get; set; }

        public void cbLinkageDeviceID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectLinkageChannelOR = null;
            if (ChannelLinkageORList != null)
                ChannelLinkageORList.Clear();
            if (SelectLinkageDeviceOR == null)
                return;
            
            var v = _DeviceDA.selectChannels(SelectLinkageDeviceOR.Deviceid);
            foreach (ChannelOR obj in v)
            {
                ChannelLinkageORList.Add(obj);
                if (OperationType == OpType.Alert && LinkageSetObj != null
                    && obj.Channelno == LinkageSetObj.Linkagechannelno)
                {
                    SelectLinkageChannelOR = obj;
                }
            }
        }
        //通首
        ObservableCollection<ChannelOR> _ChannelLinkageORList = new ObservableCollection<ChannelOR>();
        public ObservableCollection<ChannelOR> ChannelLinkageORList
        {
            set { _ChannelLinkageORList = value; }
            get { return _ChannelLinkageORList; }
        }
        public ChannelOR SelectLinkageChannelOR { get; set; }
        #endregion
        #endregion
        private bool SetValue()
        {
            StringBuilder sbError = new StringBuilder();
            if (SelectChannelOR == null)
            {
                sbError.Append("没有选择设备通道!");
            }
            else
            {
                LinkageSetObj.Stationid = SelectStationOR.Stationid;
                LinkageSetObj.StationName = SelectStationOR.Stationname;

                LinkageSetObj.Deviceid = SelectDeviceOR.Deviceid;
                LinkageSetObj.DeviceName = SelectDeviceOR.Devicename;

                LinkageSetObj.Channelno = SelectChannelOR.Channelno;
                LinkageSetObj.ChannelName = SelectChannelOR.Channelname;
            }

            //联动
            if (SelectLinkageChannelOR == null)
            {
                sbError.Append("没有选择机房联运设备通道；");
            }
            else
            {
                LinkageSetObj.Linkagestationid = SelectLinkageStationOR.Stationid;
                LinkageSetObj.LineStationName = SelectLinkageStationOR.Stationname;

                LinkageSetObj.Linkagedeviceid = SelectLinkageDeviceOR.Deviceid;
                LinkageSetObj.LineDeviceName = SelectLinkageDeviceOR.Devicename;

                LinkageSetObj.Linkagechannelno = SelectLinkageChannelOR.Channelno;
                LinkageSetObj.LineChannelName = SelectLinkageChannelOR.Channelname;
            }

            if (SelectVlueTypeOR == null)
            {
                sbError.Append("没有选择值类型；");
            }
            else
            {
                LinkageSetObj.Valuetype= SelectVlueTypeOR.ID;
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
                    new LinkageSetDA().Update(LinkageSetObj);
                    _SourceObj.Clone(LinkageSetObj);
                }
                else
                {
                    new LinkageSetDA().Insert(LinkageSetObj);
                    _LinkageSetListVM.Init();
                }
                _Window.Close();
            }
            catch (Exception ex)
            {
                ShowMsgError(ex.Message);
            }

        }
    }
}

