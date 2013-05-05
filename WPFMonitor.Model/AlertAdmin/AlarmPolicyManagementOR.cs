using System;
using System.Data;
using WPFMonitor.Model;

namespace WPFMonitor.Model.AlertAdmin
{
    /// <summary>
    /// 
    /// </summary>
    public class AlarmPolicyManagementOR : ORBase
    {

        private int _Alarmpolicymanagementid;
        /// <summary>
        /// 
        /// </summary>
        public int Alarmpolicymanagementid
        {
            get { return _Alarmpolicymanagementid; }
            set
            {
                _Alarmpolicymanagementid = value;
                NotifyPropertyChanged("Alarmpolicymanagementid");
            }
        }

        private int _Stationid;
        /// <summary>
        /// 机房名称
        /// </summary>
        public int Stationid
        {
            get { return _Stationid; }
            set
            {
                _Stationid = value;
                NotifyPropertyChanged("Stationid");
            }
        }

        private string _StationName;
        /// <summary>
        /// 站点名称
        /// </summary>
        public string StationName
        {
            get { return _StationName; }
            set { _StationName = value; }
        }

        private int _Devicetypeid;
        /// <summary>
        /// 设备类型
        /// </summary>
        public int Devicetypeid
        {
            get { return _Devicetypeid; }
            set
            {
                _Devicetypeid = value;
                NotifyPropertyChanged("Devicetypeid");
            }
        }

        private string _DevicetypeName;
        /// <summary>
        /// 设备类型名称
        /// </summary>
        public string DevicetypeName
        {
            get { return _DevicetypeName; }
            set { _DevicetypeName = value; }
        }

        private int _Deviceid;
        /// <summary>
        /// 设备名称
        /// </summary>
        public int Deviceid
        {
            get { return _Deviceid; }
            set
            {
                _Deviceid = value;
                NotifyPropertyChanged("Deviceid");
            }
        }

        private string _DeviceName;
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName
        {
            get { return _DeviceName; }
            set { _DeviceName = value; }
        }

        private int _Devicechannelid;
        /// <summary>
        /// 通道
        /// </summary>
        public int Devicechannelid
        {
            get { return _Devicechannelid; }
            set
            {
                _Devicechannelid = value;
                NotifyPropertyChanged("Devicechannelid");
            }
        }

        private string _DeviceChannelName;
        /// <summary>
        /// 设备类型名称
        /// </summary>
        public string DeviceChannelName
        {
            get { return _DeviceChannelName; }
            set { _DeviceChannelName = value; }
        }

        private int _Valuetype;
        /// <summary>
        /// 测点类型
        /// </summary>
        public int Valuetype
        {
            get { return _Valuetype; }
            set
            {
                _Valuetype = value;
                NotifyPropertyChanged("Valuetype");
            }
        }

        public string ValuetypeShow { get; set; }

        private int _Maxtiggertype;
        /// <summary>
        /// 高于高限触发
        /// </summary>
        public int Maxtiggertype
        {
            get { return _Maxtiggertype; }
            set
            {
                _Maxtiggertype = value;
                NotifyPropertyChanged("Maxtiggertype");
            }
        }

        private float? _Maxvalue;
        /// <summary>
        /// 高限
        /// </summary>
        public float? Maxvalue
        {
            get { return _Maxvalue; }
            set
            {
                _Maxvalue = value;
                NotifyPropertyChanged("Maxvalue");
            }
        }

        private int _Mintiggertype;
        /// <summary>
        /// 低于触发
        /// </summary>
        public int Mintiggertype
        {
            get { return _Mintiggertype; }
            set
            {
                _Mintiggertype = value;
                NotifyPropertyChanged("Mintiggertype");
            }
        }

        private float? _Minvalue;
        /// <summary>
        /// 低限
        /// </summary>
        public float? Minvalue
        {
            get { return _Minvalue; }
            set
            {
                _Minvalue = value;
                NotifyPropertyChanged("Minvalue");
            }
        }

        private string _Eventid;
        /// <summary>
        /// 事件
        /// </summary>
        public string Eventid
        {
            get { return _Eventid; }
            set
            {
                _Eventid = value;
                NotifyPropertyChanged("Eventid");
            }
        }

        private int _Switchvalue;
        /// <summary>
        /// 开关量告警值
        /// </summary>
        public int Switchvalue
        {
            get { return _Switchvalue; }
            set
            {
                _Switchvalue = value;
                NotifyPropertyChanged("Switchvalue");
            }
        }

        private int _Alarmlevel;
        /// <summary>
        /// 
        /// </summary>
        public int Alarmlevel
        {
            get { return _Alarmlevel; }
            set
            {
                _Alarmlevel = value;
                NotifyPropertyChanged("Alarmlevel");
            }
        }

        private string _Alarmtarget;
        /// <summary>
        /// 
        /// </summary>
        public string Alarmtarget
        {
            get { return _Alarmtarget; }
            set
            {
                _Alarmtarget = value;
                NotifyPropertyChanged("Alarmtarget");
            }
        }

        private string _Alarmway;
        /// <summary>
        /// 告警方式
        /// </summary>
        public string Alarmway
        {
            get { return _Alarmway; }
            set
            {
                _Alarmway = value;
                NotifyPropertyChanged("Alarmway");
            }
        }

        private int _Isenablefrequency;
        /// <summary>
        /// 
        /// </summary>
        public int Isenablefrequency
        {
            get { return _Isenablefrequency; }
            set
            {
                _Isenablefrequency = value;
                NotifyPropertyChanged("Isenablefrequency");
            }
        }

        private string _Alarmaudiofile;
        /// <summary>
        /// 
        /// </summary>
        public string Alarmaudiofile
        {
            get { return _Alarmaudiofile; }
            set
            {
                _Alarmaudiofile = value;
                NotifyPropertyChanged("Alarmaudiofile");
            }
        }

        private string _Disalarmaudiofile;
        /// <summary>
        /// 
        /// </summary>
        public string Disalarmaudiofile
        {
            get { return _Disalarmaudiofile; }
            set
            {
                _Disalarmaudiofile = value;
                NotifyPropertyChanged("Disalarmaudiofile");
            }
        }

        private int? _Alarmtimes;
        /// <summary>
        /// 告警次数
        /// </summary>
        public int? Alarmtimes
        {
            get { return _Alarmtimes; }
            set
            {
                _Alarmtimes = value;
                NotifyPropertyChanged("Alarmtimes");
            }
        }

        private int? _Alarmfiltertimes;
        /// <summary>
        /// 报警过滤次数
        /// </summary>
        public int? Alarmfiltertimes
        {
            get { return _Alarmfiltertimes; }
            set
            {
                _Alarmfiltertimes = value;
                NotifyPropertyChanged("Alarmfiltertimes");
            }
        }

        private string _Smsmsg;
        /// <summary>
        /// 短信邮件语音报警内容
        /// </summary>
        public string Smsmsg
        {
            get { return _Smsmsg; }
            set
            {
                _Smsmsg = value;
                NotifyPropertyChanged("Smsmsg");
            }
        }

        private int _Alarmverify;
        /// <summary>
        /// 
        /// </summary>
        public int Alarmverify
        {
            get { return _Alarmverify; }
            set
            {
                _Alarmverify = value;
                NotifyPropertyChanged("Alarmverify");
            }
        }

        private int _Isenable;
        /// <summary>
        /// 启用本策略
        /// </summary>
        public int Isenable
        {
            get { return _Isenable; }
            set
            {
                _Isenable = value;
                NotifyPropertyChanged("Isenable");
                NotifyPropertyChanged("IsenableBool");
            }
        }

        
        /// <summary>
        /// 启用本策略
        /// </summary>
        public Boolean IsenableBool
        {
            get { return _Isenable == 1 ? true : false; }
            set
            {
                if (value)
                    _Isenable = 1;
                else
                    _Isenable = 0;
            }
        }


        private int _Lightid;
        /// <summary>
        /// 报警时声光告警器
        /// </summary>
        public int Lightid
        {
            get { return _Lightid; }
            set
            {
                _Lightid = value;
                NotifyPropertyChanged("Lightid");
            }
        }

        private int _Releaselightid;
        /// <summary>
        /// 解除时声光告警器
        /// </summary>
        public int Releaselightid
        {
            get { return _Releaselightid; }
            set
            {
                _Releaselightid = value;
                NotifyPropertyChanged("Releaselightid");
            }
        }

        /// <summary>
        /// AlarmPolicyManagement构造函数
        /// </summary>
        public AlarmPolicyManagementOR()
        {

        }

        /// <summary>
        /// AlarmPolicyManagement构造函数
        /// </summary>
        public AlarmPolicyManagementOR(DataRow row)
        {
            // 
            _Alarmpolicymanagementid = Convert.ToInt32(row["AlarmPolicyManagementID"]);
            // 机房名称
            _Stationid = Convert.ToInt32(row["StationID"]);
            // 设备类型
            _Devicetypeid = Convert.ToInt32(row["DeviceTypeID"]);
            // 设备名称
            _Deviceid = Convert.ToInt32(row["DeviceID"]);
            // 通道
            _Devicechannelid = Convert.ToInt32(row["DeviceChannelID"]);
            // 测点类型
            if (DBNull.Value != row["ValueType"])
                _Valuetype = Convert.ToInt32(row["ValueType"]);
            // 高于高限触发
            if (DBNull.Value != row["MaxTiggerType"])
                _Maxtiggertype = Convert.ToInt32(row["MaxTiggerType"]);
            // 高限
            if (DBNull.Value != row["MaxValue"])
                _Maxvalue = float.Parse(row["MaxValue"].ToString());
            // 低于触发
            if (DBNull.Value != row["MinTiggerType"])
                _Mintiggertype = Convert.ToInt32(row["MinTiggerType"]);
            // 低限
            if (DBNull.Value != row["MinValue"])
                _Minvalue = float.Parse(row["MinValue"].ToString());
            // 事件
            _Eventid = row["EventID"].ToString().Trim();
            // 开关量告警值
            if (DBNull.Value != row["SwitchValue"])
                _Switchvalue = Convert.ToInt32(row["SwitchValue"]);
            // 
            if (DBNull.Value != row["AlarmLevel"])
                _Alarmlevel = Convert.ToInt32(row["AlarmLevel"]);
            // 
            _Alarmtarget = row["AlarmTarget"].ToString().Trim();
            // 告警方式
            _Alarmway = row["AlarmWay"].ToString().Trim();
            // 
            if (DBNull.Value != row["IsEnableFrequency"])
                _Isenablefrequency = Convert.ToInt32(row["IsEnableFrequency"]);
            // 
            _Alarmaudiofile = row["AlarmAudioFile"].ToString().Trim();
            // 
            _Disalarmaudiofile = row["DisAlarmAudioFile"].ToString().Trim();
            // 告警次数
            if (DBNull.Value != row["AlarmTimes"])
                _Alarmtimes = Convert.ToInt32(row["AlarmTimes"]);
            // 报警过滤次数
            if (DBNull.Value != row["AlarmfilterTimes"])
                _Alarmfiltertimes = Convert.ToInt32(row["AlarmfilterTimes"]);
            // 短信邮件语音报警内容
            _Smsmsg = row["SmsMsg"].ToString().Trim();
            //
            if (DBNull.Value != row["AlarmVerify"])
                _Alarmverify = Convert.ToInt32(row["AlarmVerify"]);
            // 启用本策略
            if (DBNull.Value != row["IsEnable"])
                _Isenable = Convert.ToInt32(row["IsEnable"]);
            // 报警时声光告警器
            if (DBNull.Value != row["LightID"])
                _Lightid = Convert.ToInt32(row["LightID"]);
            // 解除时声光告警器
            if (DBNull.Value != row["ReleaseLightID"])
                _Releaselightid = Convert.ToInt32(row["ReleaseLightID"]);
        }

        public void Clone(AlarmPolicyManagementOR obj)
        {
            //
            Alarmpolicymanagementid = obj.Alarmpolicymanagementid;
            //机房名称
            Stationid = obj.Stationid;
            //设备类型
            Devicetypeid = obj.Devicetypeid;
            //设备名称
            Deviceid = obj.Deviceid;
            //通道
            Devicechannelid = obj.Devicechannelid;
            //测点类型
            Valuetype = obj.Valuetype;
            //高于高限触发
            Maxtiggertype = obj.Maxtiggertype;
            //高限
            Maxvalue = obj.Maxvalue;
            //低于触发
            Mintiggertype = obj.Mintiggertype;
            //低限
            Minvalue = obj.Minvalue;
            //事件
            Eventid = obj.Eventid;
            //开关量告警值
            Switchvalue = obj.Switchvalue;
            //
            Alarmlevel = obj.Alarmlevel;
            //
            Alarmtarget = obj.Alarmtarget;
            //告警方式
            Alarmway = obj.Alarmway;
            //
            Isenablefrequency = obj.Isenablefrequency;
            //
            Alarmaudiofile = obj.Alarmaudiofile;
            //
            Disalarmaudiofile = obj.Disalarmaudiofile;
            //告警次数
            Alarmtimes = obj.Alarmtimes;
            //报警过滤次数
            Alarmfiltertimes = obj.Alarmfiltertimes;
            //短信邮件语音报警内容
            Smsmsg = obj.Smsmsg;
            //
            Alarmverify = obj.Alarmverify;
            //启用本策略
            Isenable = obj.Isenable;
            //报警时声光告警器
            Lightid = obj.Lightid;
            //解除时声光告警器
            Releaselightid = obj.Releaselightid;

        }

    }

    public class AlarmPolicyManagementSaveOR
    {

        private int _Alarmpolicymanagementid;
        /// <summary>
        /// 
        /// </summary>
        public int Alarmpolicymanagementid
        {
            get { return _Alarmpolicymanagementid; }
            set { _Alarmpolicymanagementid = value; }
        }

        private int _Stationid;
        /// <summary>
        /// 机房名称
        /// </summary>
        public int Stationid
        {
            get { return _Stationid; }
            set { _Stationid = value; }
        }

        private int _Devicetypeid;
        /// <summary>
        /// 设备类型
        /// </summary>
        public int Devicetypeid
        {
            get { return _Devicetypeid; }
            set { _Devicetypeid = value; }
        }

        private int _Deviceid;
        /// <summary>
        /// 设备名称
        /// </summary>
        public int Deviceid
        {
            get { return _Deviceid; }
            set { _Deviceid = value; }
        }

        private int _Devicechannelid;
        /// <summary>
        /// 通道
        /// </summary>
        public int Devicechannelid
        {
            get { return _Devicechannelid; }
            set { _Devicechannelid = value; }
        }

        private int _Valuetype;
        /// <summary>
        /// 
        /// </summary>
        public int Valuetype
        {
            get { return _Valuetype; }
            set { _Valuetype = value; }
        }

        private int _Maxtiggertype;
        /// <summary>
        /// 高于高限触发
        /// </summary>
        public int Maxtiggertype
        {
            get { return _Maxtiggertype; }
            set { _Maxtiggertype = value; }
        }

        private string _Maxvalue;
        /// <summary>
        /// 
        /// </summary>
        public string Maxvalue
        {
            get { return _Maxvalue; }
            set { _Maxvalue = value; }
        }

        private int _Mintiggertype;
        /// <summary>
        /// 低于触发
        /// </summary>
        public int Mintiggertype
        {
            get { return _Mintiggertype; }
            set { _Mintiggertype = value; }
        }

        private string _Minvalue;
        /// <summary>
        /// 
        /// </summary>
        public string Minvalue
        {
            get { return _Minvalue; }
            set { _Minvalue = value; }
        }

        

        private int _Switchvalue;
        /// <summary>
        /// 
        /// </summary>
        public int Switchvalue
        {
            get { return _Switchvalue; }
            set { _Switchvalue = value; }
        }

        private int _Alarmlevel;
        /// <summary>
        /// 
        /// </summary>
        public int Alarmlevel
        {
            get { return _Alarmlevel; }
            set { _Alarmlevel = value; }
        }

        private string _Alarmtarget;
        /// <summary>
        /// 
        /// </summary>
        public string Alarmtarget
        {
            get { return _Alarmtarget; }
            set { _Alarmtarget = value; }
        }

        private string _Alarmway;
        /// <summary>
        /// 
        /// </summary>
        public string Alarmway
        {
            get { return _Alarmway; }
            set { _Alarmway = value; }
        }

        private int _Isenablefrequency;
        /// <summary>
        /// 
        /// </summary>
        public int Isenablefrequency
        {
            get { return _Isenablefrequency; }
            set { _Isenablefrequency = value; }
        }

        private string _Alarmaudiofile;
        /// <summary>
        /// 电话报警语音文件
        /// </summary>
        public string Alarmaudiofile
        {
            get { return _Alarmaudiofile; }
            set { _Alarmaudiofile = value; }
        }

        private string _Disalarmaudiofile;
        /// <summary>
        /// 电话解除语音文件
        /// </summary>
        public string Disalarmaudiofile
        {
            get { return _Disalarmaudiofile; }
            set { _Disalarmaudiofile = value; }
        }

        private int _Alarmtimes;
        /// <summary>
        /// 告警次数
        /// </summary>
        public int Alarmtimes
        {
            get { return _Alarmtimes; }
            set { _Alarmtimes = value; }
        }

        private int _Alarmfiltertimes;
        /// <summary>
        /// 报警过滤次数
        /// </summary>
        public int Alarmfiltertimes
        {
            get { return _Alarmfiltertimes; }
            set { _Alarmfiltertimes = value; }
        }

        private string _Smsmsg;
        /// <summary>
        /// 筹集语间报警内容
        /// </summary>
        public string Smsmsg
        {
            get { return _Smsmsg; }
            set { _Smsmsg = value; }
        }

        private int _Alarmverify;
        /// <summary>
        /// 
        /// </summary>
        public int Alarmverify
        {
            get { return _Alarmverify; }
            set { _Alarmverify = value; }
        }

        private int _Isenable;
        /// <summary>
        /// 启用本策略
        /// </summary>
        public int Isenable
        {
            get { return _Isenable; }
            set { _Isenable = value; }
        }

        private string _Eventid;
        /// <summary>
        /// 事件编号
        /// </summary>
        public string Eventid
        {
            get { return _Eventid; }
            set { _Eventid = value; }
        }

        private int _Lightid;
        /// <summary>
        /// 
        /// </summary>
        public int Lightid
        {
            get { return _Lightid; }
            set { _Lightid = value; }
        }

        private int _Releaselightid;
        /// <summary>
        /// 
        /// </summary>
        public int Releaselightid
        {
            get { return _Releaselightid; }
            set { _Releaselightid = value; }
        }
    }
}

