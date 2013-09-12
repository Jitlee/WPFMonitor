using System;
using System.Linq;
using WPFMonitor.Model.AlertAdmin;
using WPFMonitor.DAL.AlertAdmin;
using WPFMonitor.Core.ViewModel;
using WPFMonitor.View.AlertAdmin;
using System.Collections.ObjectModel;
using WPFMonitor.Model.Sys;
using System.Windows.Controls;
using WPFMonitor.DAL.Sys;
using System.Text;
using WPFMonitor.Model;


namespace WPFMonitor.Core.ViewModel.AlertAdmin
{
    public class MainteEditViewModel : EditBaseViewModel
    {
        MainteListViewModel _MainteListVM;
        MainteOR _SourceObj;
        MainteEditView _Window;
        public MainteEditViewModel(MainteListViewModel _vm, MainteEditView _mw)
        {
            _MainteListVM = _vm;
            _Window = _mw;
            OperationType = OpType.Add;
            MainteObj = new MainteOR();

            Init();
        }

        public MainteEditViewModel(MainteListViewModel _vm, MainteEditView _mw, MainteOR _MainteObj)
        {
            _MainteListVM = _vm;
            _Window = _mw;
            _SourceObj = _MainteObj;

            OperationType = OpType.Alert;
            MainteObj = new MainteOR();
            MainteObj.Clone(_MainteObj);

            Init();
        }

        private void Init()
        {
            MainteStatusORList = new ObservableCollection<StatusOR>() { 
            new StatusOR(){ ID=1, Name="开始维护"},
            new StatusOR(){ ID=2, Name="维护中断"},
            new StatusOR(){ ID=3, Name="完成维护"}
            };

            if (OperationType == OpType.Alert && MainteObj != null)
            {
                var v = from f in MainteStatusORList where f.ID == MainteObj.Valuetype select f;
                if (v.Count() > 0)
                    SelectMainteStatus = v.First();
            }


            _Window.cbStation.SelectionChanged += new SelectionChangedEventHandler(cbStation_SelectionChanged);
            

            StationDA _staDA = new StationDA();
            StationORList = _staDA.selectAllStation();

            if (OperationType == OpType.Alert)
            {
                if (StationORList != null && MainteObj != null)
                {
                    var vS = from f in StationORList where f.Stationid == MainteObj.Stationid select f;
                    if (vS.Count() != 0)
                        SelectStationOR = vS.First();
                }
            }
        }
        public MainteOR MainteObj { get; set; }

        public ObservableCollection<StatusOR> MainteStatusORList { get; set; }
        public StatusOR SelectMainteStatus { get; set; }
          
        public MainteEditViewModel(MainteOR _Sta)
        {
            
            OperationType = OpType.Alert;
            MainteObj = _Sta;
        }

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
                if (OperationType == OpType.Alert && MainteObj != null
                    && obj.Deviceid == MainteObj.Deviceid)
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


        private bool SetValue()
        {
            StringBuilder sbError = new StringBuilder();
            if (SelectDeviceOR == null)
            {
                sbError.AppendLine("没有选择设备;");
            }
            else
            {
                MainteObj.Stationid = SelectStationOR.Stationid;
                MainteObj.StationName = SelectStationOR.Stationname;

                MainteObj.Deviceid = SelectDeviceOR.Deviceid;
                MainteObj.DeviceName = SelectDeviceOR.Devicename;
            }

            if (SelectMainteStatus == null)
            {
                sbError.AppendLine("没有选择维修状态;");
            }
            else
            {
                MainteObj.Valuetype = SelectMainteStatus.ID;
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
            //
            if (!SetValue())
                return;
            try
            {
                if (OperationType == OpType.Alert)
                {
                    new MainteDA().Update(MainteObj);
                    _SourceObj.Clone(MainteObj);
                }
                else
                {
                    new MainteDA().Insert(MainteObj);
                    _MainteListVM.Init();
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

