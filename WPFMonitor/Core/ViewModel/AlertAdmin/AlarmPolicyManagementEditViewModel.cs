using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using WPFMonitor.Model.AlertAdmin;
using WPFMonitor.DAL.AlertAdmin;
using WPFMonitor.Core.ViewModel;
using WPFMonitor.View.AlertAdmin;
using WPFMonitor.Model.Sys;
using WPFMonitor.DAL.Sys;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;


namespace WPFMonitor.Core.ViewModel.AlertAdmin
{
    public class AlarmPolicyManagementEditViewModel : EditBaseViewModel
    {
        AlarmPolicyManagementListViewModel _AlarmPolicyManagementListVM;
        public AlarmPolicyManagementEditViewModel()
        {

            Init();
            _Sta = new ChannelManagementOR();
        }
        public AlarmPolicyManagementEditViewModel(ChannelManagementOR _mSta,AlarmPolicyManagementListViewModel _List)
        {
            _Sta = _mSta;
            _AlarmPolicyManagementListVM = _List;
            Init();            
        }

        #region 属性
        /// <summary>
        /// 当为开关量时的，可用状态
        /// </summary>
        public bool IsSwitchEnable { get; set; }
        /// <summary>
        /// 模拟量 ，可用状态
        /// </summary>
        public bool IsMoNiEnable { get; set; }

        private string _defaultSmsMsg;
        /// <summary>
        /// 默认- 短信邮件语音报警内容
        /// </summary>
        public string DefaultSmsMsg
        {
            get { return _defaultSmsMsg; }
            set { _defaultSmsMsg = value; NotifyPropertyChanged("DefaultSmsMsg"); }
        }

        /// <summary>
        /// 事件类型列表
        /// </summary>
        public ObservableCollection<EventTypeOR> EventTypeList { get; set; }
        /// <summary>
        /// 开关选择事件
        /// </summary>
        public EventTypeOR SelectSwitchEventTypeOR { get; set; }
        public EventTypeOR SelectHiEventTypeOR { get; set; }
        public EventTypeOR SelectLoEventTypeOR { get; set; }
        
        //报警
        public ObservableCollection<LightAlarmOR> LightAlarmList { get; set; }
        public LightAlarmOR SelectLightAlarmOR { get; set; }

        public LightAlarmOR SelectReleaseLightAlarmOR { get; set; }

        /// <summary>
        /// 开关量告警
        /// </summary>
        public ObservableCollection<string> SwitchvalueArr { get; set; }

        public string selectSwitchvalue { get; set; }

        //确定按钮、删除按钮 属性
        /// <summary>
        /// 保存按钮、是否可用
        /// </summary>
        public bool SaveEnable { get; set; }
        /// <summary>
        /// 删除按钮是否可用
        /// </summary>
        public bool DeleteEnable { get; set; }
        #endregion

        #region 修改默认内容,事件
        private ICommand _SmsMsgCommand;
        public ICommand SmsMsgCommand
        {
            get
            {
                if (null == this._SmsMsgCommand)
                {
                    this._SmsMsgCommand = new DelegateCommand(this.SmsMsgCommand_Click);
                }
                return this._SmsMsgCommand;
            }
        }
        protected  void SmsMsgCommand_Click()
        {
            SysParaOR obj = new SysParaOR();
            obj.Keystr = "defaultSmsMsg";
            obj.Valstr = DefaultSmsMsg;
            try
            {
                new SysParaDA().Update(obj);
                MessageBox.Show("保存成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Question);
            }
            catch (Exception ex)
            {
                ShowMsgError(ex.Message);
            }
        }

        private ICommand _DeleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (null == this._DeleteCommand)
                {
                    this._DeleteCommand = new DelegateCommand(this.DeleteCommand_Click);
                }
                return this._DeleteCommand;
            }
        }
        protected void DeleteCommand_Click()
        {
            try
            {
                if (MessageBox.Show("确定要删除报警信息吗?" , "询问", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    new AlarmPolicyManagementDA().Delete(AlarmPolicyManagementObj.Stationid, AlarmPolicyManagementObj.Devicetypeid
                        , AlarmPolicyManagementObj.Deviceid, AlarmPolicyManagementObj.Devicechannelid);
                    _AlarmPolicyManagementListVM.ClearData();
                }
            }
            catch (Exception ex)
            {
                ShowMsgError(ex.Message);
            }
        }
        #endregion
        
        #region 初使化
        private ChannelManagementOR _Sta;
        private void Init()
        {
            IsSwitchEnable = false;
            IsMoNiEnable = false;

            SaveEnable = false;
            DeleteEnable = false;

            if (_Sta == null)
                return;
            SaveEnable = true;
            BindEvent();

             AlarmPolicyManagementObj = new AlarmPolicyManagementDA().selectARowDate(_Sta.StationID, _Sta.DeviceTypeID
                   , _Sta.Deviceid,_Sta.Channelno);

             if (AlarmPolicyManagementObj == null)
             {
                 OperationType = OpType.Add;
                 AlarmPolicyManagementObj = new AlarmPolicyManagementOR();
             }
             else
             {
                 DeleteEnable = true;
                 OperationType = OpType.Alert;
                 showAlarmPolicyInfo(AlarmPolicyManagementObj);
             } 

            AlarmPolicyManagementObj.Stationid = _Sta.StationID;
            AlarmPolicyManagementObj.StationName = _Station.selectARowDate(_Sta.StationID).Stationname;

            AlarmPolicyManagementObj.Deviceid = _Sta.Deviceid;
            AlarmPolicyManagementObj.DeviceName = _Device.GetDeviceName(_Sta.Deviceid);

            AlarmPolicyManagementObj.Devicetypeid = _Sta.DeviceTypeID;
            AlarmPolicyManagementObj.DevicetypeName = _Device.GetDeviceTypeName(_Sta.DeviceTypeID);

            AlarmPolicyManagementObj.Devicechannelid = _Sta.Channelno;
            AlarmPolicyManagementObj.DeviceChannelName = _Device.GetChannelName(_Sta.Channelno);

            int ValueType = _Device.GetChannelValueType(_Sta.Deviceid, _Sta.Channelno);

            AlarmPolicyManagementObj.Valuetype = ValueType;
            if (ValueType == 1)
            {
                AlarmPolicyManagementObj.ValuetypeShow = "开关量";                
                IsMoNiEnable = true;
                IsSwitchEnable = false;
            }
            else
            {
                AlarmPolicyManagementObj.ValuetypeShow = "模拟量";
                IsMoNiEnable = false;
                IsSwitchEnable = true;
            }

            //加载短信默认值
           DefaultSmsMsg = new SysParaDA().selectARowDate("defaultSmsMsg").Valstr;
           AlarmPolicyManagementObj.Smsmsg = DefaultSmsMsg;
        }

        public void showAlarmPolicyInfo(AlarmPolicyManagementOR m_Alar)
        {
            string EventID = m_Alar.Eventid;
            if (m_Alar.Valuetype == 0)
            {
                string[] evds = EventID.Split('-');
                if (evds.Length > 1)
                {
                    foreach (EventTypeOR obj in EventTypeList)
                    {
                        if (evds[0] == obj.Eventid.ToString())
                        {
                            SelectHiEventTypeOR = obj;
                        }

                        if (evds[1] == obj.Eventid.ToString())
                        {
                            SelectLoEventTypeOR = obj;
                        }
                    }
                }
            }
            else
            {
                foreach (EventTypeOR obj in EventTypeList)
                {
                    if (EventID == obj.Eventid.ToString())
                    {
                        SelectSwitchEventTypeOR = obj;
                    }
                }
            }

            if (m_Alar.Switchvalue == 1)
                selectSwitchvalue = "高电平";
            else
                selectSwitchvalue = "低电平";
        }
        /// <summary>
        /// 绑定下拉列表
        /// </summary>
        private void BindEvent()
        {
            if (EventTypeList == null)
            {
                EventTypeList = new EventTypeDA().selectAllDate();
                if (EventTypeList == null)
                    EventTypeList = new ObservableCollection<EventTypeOR>();
                EventTypeOR tempOR = new EventTypeOR() { Eventid = -1, Eventname = "" };
                EventTypeList.Insert(0, tempOR);

                SelectSwitchEventTypeOR = tempOR;
                SelectHiEventTypeOR = tempOR;
                SelectLoEventTypeOR = tempOR;
            }

            if (LightAlarmList == null)
            {
                LightAlarmList = new LightAlarmDA().selectAllDate();
                LightAlarmOR tempOR = new LightAlarmOR() { Lightid = -1, Lightname = "未启用声光报警" };
                if (LightAlarmList == null)
                    LightAlarmList = new ObservableCollection<LightAlarmOR>();
                LightAlarmList.Insert(0, tempOR);
                SelectLightAlarmOR = tempOR;
                SelectReleaseLightAlarmOR = tempOR;
            }

            SwitchvalueArr = new ObservableCollection<string>() { "", "高电平", "低电平" };
            selectSwitchvalue = "";
        }
        #endregion

        DeviceDA _Device = new DeviceDA();
        StationDA _Station = new StationDA();
        public AlarmPolicyManagementOR AlarmPolicyManagementObj { get; set; }

        #region 保存数据
        private AlarmPolicyManagementSaveOR SetValue()
        {
            StringBuilder sbError = new StringBuilder();
            
            AlarmPolicyManagementSaveOR obj = new AlarmPolicyManagementSaveOR();
            obj.Stationid = AlarmPolicyManagementObj.Stationid;
            obj.Devicetypeid = AlarmPolicyManagementObj.Devicetypeid;
            obj.Deviceid = AlarmPolicyManagementObj.Deviceid;
            obj.Devicechannelid = AlarmPolicyManagementObj.Devicechannelid;

            obj.Isenable = AlarmPolicyManagementObj.Isenable;
            obj.Valuetype=AlarmPolicyManagementObj.Valuetype;
                       
            string EvHi, EvLo, EvSg;
            string EventID = "";
            if (AlarmPolicyManagementObj.Valuetype == 0)
            {

                if (SelectHiEventTypeOR.Eventid != -1)
                    EvHi = SelectHiEventTypeOR.Eventid.ToString();
                else
                    EvHi = "";

                if (SelectLoEventTypeOR.Eventid != -1)
                    EvLo = SelectLoEventTypeOR.Eventid.ToString();
                else
                    EvLo = "";

                EventID = EvHi + "-" + EvLo;

                obj.Maxvalue = AlarmPolicyManagementObj.Maxvalue.HasValue
                    ? AlarmPolicyManagementObj.Maxvalue.ToString() : "NULL";

                obj.Minvalue = AlarmPolicyManagementObj.Minvalue.HasValue
                    ? AlarmPolicyManagementObj.Minvalue.ToString() : "NULL";
               

                obj.Maxtiggertype = AlarmPolicyManagementObj.Maxtiggertype;
                obj.Mintiggertype = AlarmPolicyManagementObj.Mintiggertype;
            }
            else
            {
                if(SelectSwitchEventTypeOR.Eventid !=-1)
                    EvSg = SelectSwitchEventTypeOR.Eventid.ToString();
                else
                    EvSg = "";

                EventID = EvSg;


                obj.Maxvalue = "NULL";
                obj.Minvalue = "NULL";                
                if (selectSwitchvalue == "高电平")
                    obj.Switchvalue = 1;
                else if (selectSwitchvalue == "低电平")
                    obj.Switchvalue = 0;
            }
            obj.Eventid = EventID;
            if (AlarmPolicyManagementObj.Alarmfiltertimes.HasValue)
                obj.Alarmfiltertimes = AlarmPolicyManagementObj.Alarmfiltertimes.Value;
            if (AlarmPolicyManagementObj.Alarmtimes.HasValue)
                obj.Alarmtimes = AlarmPolicyManagementObj.Alarmtimes.Value;

            obj.Smsmsg = AlarmPolicyManagementObj.Smsmsg;
            obj.Lightid = SelectLightAlarmOR.Lightid;
            obj.Releaselightid = SelectReleaseLightAlarmOR.Lightid;
            obj.Disalarmaudiofile = AlarmPolicyManagementObj.Disalarmaudiofile;
            obj.Alarmaudiofile = AlarmPolicyManagementObj.Alarmaudiofile;

            string ErrorMsg = sbError.ToString();
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                ShowMsgError(ErrorMsg);
                return null;
            }

            return obj;
        }

        public override void OK()
        {
            //string errMsg = "";
            AlarmPolicyManagementSaveOR obj = SetValue();
            if (obj == null)
                return;

            try
            {
                if (OperationType == OpType.Alert)
                {
                    new AlarmPolicyManagementDA().Update(obj);
                }
                else
                {
                    new AlarmPolicyManagementDA().Insert(obj);

                    _AlarmPolicyManagementListVM.SetSelectTreeItem();

                    OperationType = OpType.Alert;
                }
                MessageBox.Show("保存成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Question);

            }
            catch (Exception ex)
            {
                ShowMsgError(ex.Message);
            }

        }
        #endregion
    }
}

