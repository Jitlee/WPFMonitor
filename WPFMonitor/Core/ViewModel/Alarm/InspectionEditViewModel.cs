using System;
using System.Linq;
using WPFMonitor.Model.Alarm;
using WPFMonitor.DAL.Alarm;
using WPFMonitor.Core.ViewModel;
using WPFMonitor.View.Alarm;
using System.Collections.ObjectModel;
using WPFMonitor.Model.Sys;
using System.Windows.Controls;
using WPFMonitor.DAL.Sys;
using System.Text;


namespace WPFMonitor.Core.ViewModel.Alarm
{
    public class InspectionEditViewModel : EditBaseViewModel
    {
        InspectionListViewModel _InspectionListVM;
        InspectionOR _SourceObj;
        InspectionEditView _Window;
        public InspectionEditViewModel(InspectionListViewModel _vm, InspectionEditView _mw)
        {
            _InspectionListVM = _vm;
            _Window = _mw;
            OperationType = OpType.Add;
            InspectionObj = new InspectionOR();
            //UpdatetxtSource(_Window.gridContent);
            Init();
        }

        public InspectionEditViewModel(InspectionListViewModel _vm, InspectionEditView _mw, InspectionOR _InspectionObj)
        {
            _InspectionListVM = _vm;
            _Window = _mw;
            _SourceObj = _InspectionObj;

            OperationType = OpType.Alert;
            InspectionObj = new InspectionOR();
            InspectionObj.Clone(_InspectionObj);

            Init();
        }

        private void Init()
        {

            _Window.cbStation.SelectionChanged += new SelectionChangedEventHandler(cbStation_SelectionChanged);
            _Window.cbDeviceTypeID.SelectionChanged += new SelectionChangedEventHandler(cbStation_SelectionChanged);

            _Window.cbDeviceID.SelectionChanged += new SelectionChangedEventHandler(cbDeviceID_SelectionChanged);

            //初使化值
            DateTimeHH = GlobalData.DateHH;
            DateTimeMi = GlobalData.DateMi;
            SelectDateTimeHH = "00";
            SelectDateTimeMi = "00";

            StationDA _staDA = new StationDA();
            StationORList = _staDA.selectAllStation();

            DeviceAndTypeDA _DTypeDA = new DeviceAndTypeDA();
            this.DeviceTypeORList = _DTypeDA.GetAllDeviceType();

            if (OperationType == OpType.Alert)
            {
                LoadVlaue();

                if (StationORList != null && InspectionObj != null)
                {
                    var vS = from f in StationORList where f.Stationid == InspectionObj.Stationid select f;
                    if (vS.Count() != 0)
                        SelectStationOR = vS.First();
                }

                if (DeviceTypeORList != null && InspectionObj != null)
                {
                    var vDty = from f in DeviceTypeORList where f.Devicetypeid == InspectionObj.Devicetypeid select f;
                    if (vDty.Count() != 0)
                        SelectDeviceTypeOR = vDty.First();
                }
            }
        }

        private void LoadVlaue()
        {
            //时间设置
            if (!string.IsNullOrEmpty(InspectionObj.Inspectiontime))
            {
                string[] arr = InspectionObj.Inspectiontime.Split(':');
                if (arr.Length == 2)
                {
                    SelectDateTimeHH = arr[0];
                    SelectDateTimeMi = arr[1];
                }
            }

            if (!string.IsNullOrEmpty(InspectionObj.Alarmway))
            {
                string[] arr = InspectionObj.Alarmway.Split('-');
                if (arr.Length == 4)
                {
                    if (arr[0] == "1")
                        _Window.check_Sms.IsChecked = true;
                    if (arr[1] == "1")
                        _Window.check_Phone.IsChecked = true;
                    if (arr[2] == "1")
                        _Window.check_Media.IsChecked = true;
                    if (arr[3] == "1")
                        _Window.check_Email.IsChecked = true;
                }
            }
        }

        public  ObservableCollection<string> DateTimeHH { get; set; }
        public ObservableCollection<string> DateTimeMi { get; set; }
        public string SelectDateTimeHH { get; set; }
        public string SelectDateTimeMi { get; set; }

        /// <summary>
        /// 当前站点
        /// </summary>
        public InspectionOR InspectionObj { get; set; }


        public InspectionEditViewModel(InspectionOR _Sta)
        {
            OperationType = OpType.Alert;
            InspectionObj = _Sta;
        }

        private bool SetValue()
        {
            StringBuilder sbError = new StringBuilder();

            if (SelectChannelOR == null)
            {
                sbError.AppendLine("没有选择正确的通道值；");
            }
            InspectionObj.Stationid = SelectStationOR.Stationid;
            InspectionObj.StationName = SelectStationOR.Stationname;

            InspectionObj.Devicetypeid = SelectDeviceTypeOR.Devicetypeid;
            InspectionObj.TypeName = SelectDeviceTypeOR.Typename;

            InspectionObj.Deviceid = SelectDeviceOR.Deviceid;
            InspectionObj.DeviceName = SelectDeviceOR.Devicename;

            InspectionObj.Channelno = SelectChannelOR.Channelno;
            InspectionObj.ChannelName = SelectChannelOR.Channelname;
            

            //时间
            InspectionObj.Inspectiontime = string.Format("{0}:{1}", SelectDateTimeHH, SelectDateTimeMi);//巡检时间

            string[] myAlarmWay = new string[4];
            if (_Window.check_Sms.IsChecked.Value)
            {
                if (string.IsNullOrEmpty(InspectionObj.Smsemail))
                {
                    sbError.AppendLine("报警内容不能为空；");                    
                }
            }
            if (_Window.check_Sms.IsChecked.Value)
                myAlarmWay[0] = "1";
            else
                myAlarmWay[0] = "0";

            if (_Window.check_Email.IsChecked.Value)
                myAlarmWay[3] = "1";
            else
                myAlarmWay[3] = "0";


            if (_Window.check_Phone.IsChecked.Value)
            {
                if (string.IsNullOrEmpty(InspectionObj.Phonemedia))
                {
                    sbError.AppendLine("报警语音文件名不能为空；");
                }
            }
            if (_Window.check_Phone.IsChecked.Value)
                myAlarmWay[1] = "1";
            else
                myAlarmWay[1] = "0";
            if (_Window.check_Media.IsChecked.Value)
                myAlarmWay[2] = "1";
            else
                myAlarmWay[2] = "0";

            string way = "";
            for (int i = 0; i < myAlarmWay.Length; i++)
            {
                if (i != myAlarmWay.Length - 1)
                    way += myAlarmWay[i] + "-";
                else
                    way += myAlarmWay[i];

            }
            InspectionObj.Alarmway = way;

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
                    new InspectionDA().Update(InspectionObj);
                    _SourceObj.Clone(InspectionObj);
                }
                else
                {
                    new InspectionDA().Insert(InspectionObj);
                    _InspectionListVM.Init();
                }
                _Window.Close();
            }
            catch (Exception ex)
            {
                ShowMsgError(ex.Message);
            }

        }

        #region 站点、设备类型、 设备、通首管理
        DeviceAndTypeDA _DeviceDA = new DeviceAndTypeDA();

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
            if (SelectDeviceTypeOR == null || SelectStationOR == null)
                return;

            SelectDeviceOR = null;

            if (DeviceORList != null)
                DeviceORList.Clear();

            var v = _DeviceDA.GetAllGenerdDevice(SelectStationOR.Stationid.ToString(),
                SelectDeviceTypeOR.Devicetypeid.ToString());

            foreach (DeviceOR obj in v)
            {
                DeviceORList.Add(obj);
                if (OperationType == OpType.Alert && InspectionObj != null
                    && obj.Deviceid == InspectionObj.Deviceid)
                {
                    SelectDeviceOR = obj;
                }
            }
        }
        //设备类型
        ObservableCollection<DeviceTypeOR> _DeviceTypeORList = new ObservableCollection<DeviceTypeOR>();
        public ObservableCollection<DeviceTypeOR> DeviceTypeORList
        {
            set
            {
                _DeviceTypeORList = value;
                NotifyPropertyChanged("DeviceTypeORList");
            }
            get { return _DeviceTypeORList; }
        }
        public DeviceTypeOR SelectDeviceTypeOR { get; set; }

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

            var v = _DeviceDA.SelectChannels(SelectDeviceOR.Deviceid.ToString());
            foreach (ChannelOR obj in v)
            {
                ChannelORList.Add(obj);

                if (OperationType == OpType.Alert && InspectionObj != null
                    && obj.Channelno == InspectionObj.Channelno)
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

