using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using WPFMonitor.Model.AlertAdmin;
using WPFMonitor.DAL.AlertAdmin;
using WPFMonitor.Core.ViewModel;
using WPFMonitor.View.AlertAdmin;
using WPFMonitor.Model;


namespace WPFMonitor.Core.ViewModel.AlertAdmin
{
    public class EventTypeEditViewModel : EditBaseViewModel
    {
        EventTypeListViewModel _EventTypeListVM;
        EventTypeOR _SourceObj;
        EventTypeEditView _Window;
        public EventTypeEditViewModel(EventTypeListViewModel _vm, EventTypeEditView _mw)
        {
            _EventTypeListVM = _vm;
            _Window = _mw;
            OperationType = OpType.Add;
            EventTypeObj = new EventTypeOR();
            Init();
        }

        public EventTypeEditViewModel(EventTypeListViewModel _vm, EventTypeEditView _mw, EventTypeOR _EventTypeObj)
        {
            _EventTypeListVM = _vm;
            _Window = _mw;
            _SourceObj = _EventTypeObj;

            OperationType = OpType.Alert;
            EventTypeObj = new EventTypeOR();
            EventTypeObj.Clone(_EventTypeObj);

            Init();
        }

        private void Init()
        {
            //AlarmWayOR = new AlarmWayInfo();

            AlarmGroupsList = new AlarmGroupsDA().selectAllDate();
            DisarmTimeList = new DisarmTimeDA().selectAllDate();

              StatusLeve = new ObservableCollection<StatusOR>() { 
            new StatusOR(){ ID=1,Name="1级"},
            new StatusOR(){ ID=2,Name="2级"},
            new StatusOR(){ ID=3,Name="3级"},
            new StatusOR(){ ID=4,Name="4级"}
            };
            if (OperationType == OpType.Alert)
            {
                //AlarmWayOR.
                    InitAlarm(EventTypeObj.Alarmway);
                //告警组
                CheckAlarm(EventTypeObj.Alarmtarget);

                //撤防
                foreach (DisarmTimeOR li in DisarmTimeList)
                {
                    if (li.Disarmid.ToString()== EventTypeObj.Disarmid)
                    {
                        li.IsSelected = true;
                    }
                }

                //状态
                foreach (StatusOR obj in StatusLeve)
                {
                    if (obj.ID == EventTypeObj.Alarmlevel)
                    {
                        SelectStatusOR = obj;
                        break;
                    }
                }
            }
        }

        public EventTypeOR EventTypeObj { get; set; }

        //等级
        public ObservableCollection<StatusOR> StatusLeve { get; set; }
        public StatusOR SelectStatusOR { get; set; }

        public ObservableCollection<AlarmGroupsOR> AlarmGroupsList { get; set; }
        public ObservableCollection<DisarmTimeOR> DisarmTimeList { get; set; }
        //protected AlarmWayInfo AlarmWayOR { get; set; }
        public EventTypeEditViewModel(EventTypeOR _Sta)
        {
            OperationType = OpType.Alert;
            EventTypeObj = _Sta;
        }
        private void CheckAlarm(string Alarms)
        {
            if (string.IsNullOrEmpty(Alarms))
                return;
            string[] strArr = Alarms.Split('-');
            foreach (string str in strArr)
            {
                foreach (AlarmGroupsOR li in AlarmGroupsList)
                {
                    if (li.Alarmgroupsid.ToString() == str)
                    {
                        li.IsSelected = true;
                        break;
                    }

                }
            }
        }
        private bool SetValue()
        {
            StringBuilder sbError = new StringBuilder();

            if (string.IsNullOrEmpty(EventTypeObj.Eventname))
            {
                sbError.AppendLine("名称不能为空！");
            }

            this.EventTypeObj.Alarmway =SetAlarm();
            //报警组
            string AlarmTarget = "";
            foreach (AlarmGroupsOR li in AlarmGroupsList)
            {
                if (!li.IsSelected)
                    continue;
                AlarmTarget = AlarmTarget + li.Alarmgroupsid + "-";
            } 
            EventTypeObj.Alarmtarget = AlarmTarget;
                        
            //撤防时间
            EventTypeObj.Disarmid = "";
            foreach (DisarmTimeOR li in DisarmTimeList)
            {
                if (li.IsSelected)
                {
                    if (!string.IsNullOrEmpty(EventTypeObj.Disarmid))
                    {
                        sbError.AppendLine("你不能选择连2个或2个以上撤防时间段");                        
                    }
                    EventTypeObj.Disarmid = li.Disarmid.ToString();
                }
            }

            if (SelectStatusOR == null)
            {
                sbError.AppendLine("没有选择级别；");
            }
            else
            {
                EventTypeObj.Alarmlevel = SelectStatusOR.ID;
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
                    new EventTypeDA().Update(EventTypeObj);
                    _SourceObj.Clone(EventTypeObj);
                }
                else
                {
                    new EventTypeDA().Insert(EventTypeObj);
		    
                    _EventTypeListVM.Init();
                }
                _Window.Close();
            }
            catch (Exception ex)
            {
                ShowMsgError(ex.Message);
            }

        }

        #region 告警方式
        //public AlarmWayInfo()
            //{
            //    check_Sms = true;
            //    check_Phone = true;
            //    check_Media = true;
            //    check_Emali = true;
            //}
            public bool check_Sms { get; set; }
            public bool check_Phone { get; set; }
            public bool check_Media { get; set; }
            public bool check_Emali { get; set; }

            public string SetAlarm()
            {
                string[] ArryAlarmWay = { "0", "0", "0", "0" };
                if (check_Sms)
                    ArryAlarmWay[0] = "1";
                if (check_Phone)
                    ArryAlarmWay[1] = "1";
                if (check_Media)
                    ArryAlarmWay[2] = "1";
                if (check_Emali)
                    ArryAlarmWay[3] = "1";

                string AlarmWay = "";
                for (int i = 0; i < ArryAlarmWay.Length; i++)
                {
                    if (i != 3)
                        AlarmWay += ArryAlarmWay[i] + "-";
                    else
                        AlarmWay += ArryAlarmWay[i];

                }
                return AlarmWay;
            }

            public void InitAlarm(string mAlarmway)
            {
                string[] AlarmWay = mAlarmway.Split('-');//报警方式
                if (AlarmWay[0] == "1")
                    check_Sms = true;
                else
                    check_Sms = false;

                if (AlarmWay[1] == "1")
                    check_Phone = true;
                else
                    check_Phone = false;

                if (AlarmWay[2] == "1")
                    check_Media = true;
                else
                    check_Media = false;

                if (AlarmWay[3] == "1")
                    check_Emali = true;
                else
                    check_Emali = false;
            }
        #endregion
    }
}

