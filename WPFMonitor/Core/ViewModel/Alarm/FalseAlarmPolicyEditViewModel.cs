using System;
using System.Linq;
using WPFMonitor.Model.Alarm;
using WPFMonitor.DAL.Alarm;
using WPFMonitor.Core.ViewModel;
using WPFMonitor.View.Alarm;
using System.Windows.Controls;
using WPFMonitor.DAL.Sys;
using System.Collections.ObjectModel;
using WPFMonitor.Model.Sys;
using WPFMonitor.Model.AlertAdmin;
using WPFMonitor.DAL.AlertAdmin;
using System.Text;


namespace WPFMonitor.Core.ViewModel.Alarm
{
    public class FalseAlarmPolicyEditViewModel : EditBaseViewModel
    {
        FalseAlarmPolicyListViewModel _FalseAlarmPolicyListVM;
        FalseAlarmPolicyOR _SourceObj;
        FalseAlarmPolicyEditView _Window;
        public FalseAlarmPolicyEditViewModel(FalseAlarmPolicyListViewModel _vm, FalseAlarmPolicyEditView _mw)
        {
            _FalseAlarmPolicyListVM = _vm;
            _Window = _mw;
            OperationType = OpType.Add;
            FalseAlarmPolicyObj = new FalseAlarmPolicyOR();
            //UpdatetxtSource(_Window.gridContent);
            Init();
        }

        public FalseAlarmPolicyEditViewModel(FalseAlarmPolicyListViewModel _vm, FalseAlarmPolicyEditView _mw, FalseAlarmPolicyOR _FalseAlarmPolicyObj)
        {
            _FalseAlarmPolicyListVM = _vm;
            _Window = _mw;
            _SourceObj = _FalseAlarmPolicyObj;

            OperationType = OpType.Alert;
            FalseAlarmPolicyObj = new FalseAlarmPolicyOR();
            FalseAlarmPolicyObj.Clone(_FalseAlarmPolicyObj);

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
                SetFalseType(FalseAlarmPolicyObj.Falsetype);
                if (StationORList != null && FalseAlarmPolicyObj != null)
                {
                    AlarmPolicyObj = new AlarmPolicyManagementDA().selectARowDate(FalseAlarmPolicyObj.Policyid.ToString());

                    var vS = from f in StationORList where f.Stationid == AlarmPolicyObj.Stationid select f;
                    if (vS.Count() != 0)
                        SelectStationOR = vS.First();
                }
            }
        }

        #region 限制
        private string GetFalseType()
        {
            string Falsetype = "";
            if (_Window.cbFalseAlarmLow.IsChecked == true && _Window.cbFalseAlarmHeight.IsChecked == true)
            {
                Falsetype = "全限制";
            }
            else if (_Window.cbFalseAlarmLow.IsChecked == true)
            {
                Falsetype = "低限";
            }
            else
            {
                Falsetype = "高限";
            }
            return Falsetype;
        }
        private void SetFalseType(string strType)
        {
            if (strType == "全限制")
            {

                _Window.cbFalseAlarmLow.IsChecked = true;
                _Window.cbFalseAlarmHeight.IsChecked = true;
            }
            else if (strType == "低限")
            {
                _Window.cbFalseAlarmLow.IsChecked = true;
            }
            else if (strType == "高限")
            {
                _Window.cbFalseAlarmHeight.IsChecked = true;
            }
        }
        #endregion

        /// <summary>
        /// 当前站点
        /// </summary>
        public FalseAlarmPolicyOR FalseAlarmPolicyObj { get; set; }

        public AlarmPolicyManagementOR AlarmPolicyObj { get; set; }

        public FalseAlarmPolicyEditViewModel(FalseAlarmPolicyOR _Sta)
        {
            OperationType = OpType.Alert;
            FalseAlarmPolicyObj = _Sta;
        }

        public override void OK()
        {
            string errMsg = "";
            if (GetErrors(_Window.gridContent, out errMsg))
            {
                ShowMsgError(errMsg);
                return;
            }
            if (SelectChannelOR == null)
            {
                ShowMsgError("请选择通道！");
                return;
            }
            //
            FalseAlarmPolicyObj.Policyid = FalseAlarmPolicyObj.Falsepolicyid = SelectChannelOR.AlarmPolicyManagementID;
            FalseAlarmPolicyObj.Falsetype = GetFalseType();

            FalseAlarmPolicyObj.StationName = SelectStationOR.Stationname;
            FalseAlarmPolicyObj.DeviceName = SelectDeviceOR.Devicename;
            FalseAlarmPolicyObj.ChannelName = SelectChannelOR.ChannelName;

            try
            {
                if (OperationType == OpType.Alert)
                {
                    new FalseAlarmPolicyDA().Update(FalseAlarmPolicyObj);
                    _SourceObj.Clone(FalseAlarmPolicyObj);
                }
                else
                {
                    new FalseAlarmPolicyDA().Insert(FalseAlarmPolicyObj);
                    _FalseAlarmPolicyListVM.Init();
                    //_FalseAlarmPolicyListVM.FalseAlarmPolicyORList.Add(FalseAlarmPolicyObj);
                }
                _Window.Close();
            }
            catch (Exception ex)
            {
                ShowMsgError(ex.Message);
            }

        }


        #region 站点、设备、通首管理
        PoliceDeviceDA _DeviceDA = new PoliceDeviceDA();

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
                if (OperationType == OpType.Alert && AlarmPolicyObj != null
                    && obj.Deviceid == AlarmPolicyObj.Deviceid)
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

            var v = _DeviceDA.selectChannels(SelectStationOR.Stationid.ToString(),
                SelectDeviceOR.Deviceid.ToString());
            foreach (PolicyChannelOR obj in v)
            {
                ChannelORList.Add(obj);
                if (OperationType == OpType.Alert && FalseAlarmPolicyObj != null
                    && obj.AlarmPolicyManagementID == FalseAlarmPolicyObj.Policyid)
                {
                    SelectChannelOR = obj;
                }
            }
        }
        //通首
        ObservableCollection<PolicyChannelOR> _ChannelORList = new ObservableCollection<PolicyChannelOR>();
        public ObservableCollection<PolicyChannelOR> ChannelORList
        {
            set { _ChannelORList = value; }
            get { return _ChannelORList; }
        }
        public PolicyChannelOR SelectChannelOR { get; set; }

        #endregion
    }
}

