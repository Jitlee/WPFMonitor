using System;
using System.Linq;
using WPFMonitor.Model.AlertAdmin;
using WPFMonitor.DAL.AlertAdmin;
using WPFMonitor.Core.ViewModel;
using WPFMonitor.View.AlertAdmin;
using WPFMonitor.DAL.Sys;
using System.Collections.ObjectModel;
using WPFMonitor.Model.Sys;
using System.Text;
using System.Windows.Controls;

namespace WPFMonitor.Core.ViewModel.AlertAdmin
{
    public class AlarmGroupMembersEditViewModel : EditBaseViewModel
    {
        AlarmGroupMembersListViewModel _AlarmGroupMembersListVM;
        AlarmGroupMembersOR _SourceObj;
        AlarmGroupMembersEditView _Window;
        public AlarmGroupMembersEditViewModel(AlarmGroupMembersListViewModel _vm, AlarmGroupMembersEditView _mw)
        {
            _AlarmGroupMembersListVM = _vm;
            _Window = _mw;
            OperationType = OpType.Add;
            AlarmGroupMembersObj = new AlarmGroupMembersOR();

            Init();
        }

        public AlarmGroupMembersEditViewModel(AlarmGroupMembersListViewModel _vm, AlarmGroupMembersEditView _mw, AlarmGroupMembersOR _AlarmGroupMembersObj)
        {
            _AlarmGroupMembersListVM = _vm;
            _Window = _mw;
            _SourceObj = _AlarmGroupMembersObj;

            OperationType = OpType.Alert;
            AlarmGroupMembersObj = new AlarmGroupMembersOR();
            AlarmGroupMembersObj.Clone(_AlarmGroupMembersObj);

            Init();
        }
        private void Init()
        {
            _Window.cbStation.SelectionChanged += new SelectionChangedEventHandler(cbStation_SelectionChanged);
                        
            StationORList = new StationDA().selectAllStation();
            SchedulingORList = new SchedulingDA().selectAllDate();
            AlarmLevelSetORList = new AlarmLevelSetDA().selectAllDate();

            if (OperationType == OpType.Alert && AlarmGroupMembersObj != null)
            {
                if (StationORList != null)
                {
                    var vS = from f in StationORList where f.Stationid == AlarmGroupMembersObj.Stationid select f;
                    if (vS.Count() != 0)
                        SelectStationOR = vS.First();
                }
                cbStation_SelectionChanged(null, null);

                if (AlarmGroupsORList != null)//报警组
                {
                    var vag = from f in AlarmGroupsORList where f.Alarmgroupsid == AlarmGroupMembersObj.Alarmgroupsid select f;
                    if (vag.Count() != 0)
                        SelectAlarmGroupsOR = vag.First();
                }

                if (SchedulingORList != null)//排班
                {
                    var vSchd = from f in SchedulingORList where f.Frequency == AlarmGroupMembersObj.Scheduling select f;
                    if (vSchd.Count() != 0)
                        SelectSchedulingOR = vSchd.First();
                }
                
                if (AlarmLevelSetORList != null)//处理等级
                {
                    var valarm = from f in AlarmLevelSetORList where f.Id == AlarmGroupMembersObj.Processlevel select f;
                    if (valarm.Count() != 0)
                        SelectAlarmLevelSetOR = valarm.First();
                }
            }

            //SchedulingORList
            //
        }

        #region 属性
        AlarmGroupsDA _DA = new AlarmGroupsDA();

        public AlarmGroupMembersOR AlarmGroupMembersObj { get; set; }
        
        //站点
        ObservableCollection<StationOR> _StationORList = new ObservableCollection<StationOR>();
        public ObservableCollection<StationOR> StationORList
        {
            set { _StationORList = value; NotifyPropertyChanged("StationORList"); }
            get { return _StationORList; }
        }
        public StationOR SelectStationOR { get; set; }
                
        //排班
        ObservableCollection<SchedulingOR> _SchedulingORList = new ObservableCollection<SchedulingOR>();
        public ObservableCollection<SchedulingOR> SchedulingORList
        {
            set { _SchedulingORList = value; }
            get { return _SchedulingORList; }
        }
        public SchedulingOR SelectSchedulingOR { get; set; }

        //报警等级        
        ObservableCollection<AlarmLevelSetOR> _AlarmLevelSetORList = new ObservableCollection<AlarmLevelSetOR>();
        public ObservableCollection<AlarmLevelSetOR> AlarmLevelSetORList
        {
            set { _AlarmLevelSetORList = value; NotifyPropertyChanged("AlarmLevelSetORList"); }
            get { return _AlarmLevelSetORList; }
        }
        public AlarmLevelSetOR SelectAlarmLevelSetOR { get; set; }

        public void cbStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectStationOR == null)
                return;

            SelectAlarmGroupsOR = null;

            if (AlarmGroupsORList != null)
                AlarmGroupsORList.Clear();

            var v = _DA.SelectAllDateByStationID(SelectStationOR.Stationid);

            foreach (AlarmGroupsOR obj in v)
            {
                AlarmGroupsORList.Add(obj);
                if (OperationType == OpType.Alert && AlarmGroupMembersObj != null
                    && obj.Alarmgroupsid == AlarmGroupMembersObj.Alarmgroupsid)
                {
                    SelectAlarmGroupsOR = obj;
                }
            }
        }
        //设备类型
        ObservableCollection<AlarmGroupsOR> _AlarmGroupsORList = new ObservableCollection<AlarmGroupsOR>();
        public ObservableCollection<AlarmGroupsOR> AlarmGroupsORList
        {
            set
            {
                _AlarmGroupsORList = value;
            }
            get { return _AlarmGroupsORList; }
        }
        public AlarmGroupsOR SelectAlarmGroupsOR { get; set; }

        #endregion


        public AlarmGroupMembersEditViewModel(AlarmGroupMembersOR _Sta)
        {
            OperationType = OpType.Alert;
            AlarmGroupMembersObj = _Sta;
        }
        public bool SetValue()
        {
            StringBuilder sbError = new StringBuilder();
           
           
            if (string.IsNullOrEmpty(AlarmGroupMembersObj.Name))
            {
                sbError.AppendLine("姓名不能为空！");
            }

            if (SelectAlarmGroupsOR == null)
            {
                sbError.AppendLine("报警组不能为空；");
            }
            else
            {
                AlarmGroupMembersObj.Stationid = SelectStationOR.Stationid;
                AlarmGroupMembersObj.StationName = SelectStationOR.Stationname;

                AlarmGroupMembersObj.Alarmgroupsid = SelectAlarmGroupsOR.Alarmgroupsid;
                AlarmGroupMembersObj.GroupName = SelectAlarmGroupsOR.Groupname;
            }

            if (SelectSchedulingOR == null)
            {
                sbError.AppendLine("没有选择 排班；");
            }
            else
            {
                AlarmGroupMembersObj.Scheduling = SelectSchedulingOR.Frequency;
                AlarmGroupMembersObj.FrequencyName = SelectSchedulingOR.Frequencyname;
            }

            if (SelectAlarmLevelSetOR == null)
            {
                sbError.AppendLine("没有选择 处理等级；");
            }
            else
            {
                AlarmGroupMembersObj.Processlevel = SelectAlarmLevelSetOR.Id;
                AlarmGroupMembersObj.LevelName = SelectAlarmLevelSetOR.Levelname;
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
                    new AlarmGroupMembersDA().Update(AlarmGroupMembersObj);
                    _SourceObj.Clone(AlarmGroupMembersObj);
                }
                else
                {
                    new AlarmGroupMembersDA().Insert(AlarmGroupMembersObj);
                    //_AlarmGroupMembersListVM.AlarmGroupMembersORList.Add(AlarmGroupMembersObj);
                    _AlarmGroupMembersListVM.Init();
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

