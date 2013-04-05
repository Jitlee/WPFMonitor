using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WPFMonitor.Model.AlertAdmin
{
    public class AlarmPolicyManagementOR
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
            // 
            _Valuetype = Convert.ToInt32(row["ValueType"]);
            // 高于高限触发
            _Maxtiggertype = Convert.ToInt32(row["MaxTiggerType"]);
            // 
            _Maxvalue = row["MaxValue"].ToString();
            // 低于触发
            if (row["MinTiggerType"] != DBNull.Value)
                _Mintiggertype = Convert.ToInt32(row["MinTiggerType"]);
            // 
            _Minvalue = row["MinValue"].ToString();
            // 
            if (row["SwitchValue"] != DBNull.Value)
                _Switchvalue = Convert.ToInt32(row["SwitchValue"]);
            // 
            if (row["AlarmLevel"] != DBNull.Value)
                _Alarmlevel = Convert.ToInt32(row["AlarmLevel"]);
            // 
            _Alarmtarget = row["AlarmTarget"].ToString().Trim();
            // 
            _Alarmway = row["AlarmWay"].ToString().Trim();
            // 
            if (row["IsEnableFrequency"] != DBNull.Value)
                _Isenablefrequency = Convert.ToInt32(row["IsEnableFrequency"]);
            // 电话报警语音文件
            _Alarmaudiofile = row["AlarmAudioFile"].ToString().Trim();
            // 电话解除语音文件
            _Disalarmaudiofile = row["DisAlarmAudioFile"].ToString().Trim();
            // 告警次数
            if (row["AlarmTimes"] != DBNull.Value)
                _Alarmtimes = Convert.ToInt32(row["AlarmTimes"]);
            // 报警过滤次数
            if (row["AlarmfilterTimes"] != DBNull.Value)
                _Alarmfiltertimes = Convert.ToInt32(row["AlarmfilterTimes"]);
            // 筹集语间报警内容
            _Smsmsg = row["SmsMsg"].ToString().Trim();
            // 
            if (row["AlarmVerify"] != DBNull.Value)
                _Alarmverify = Convert.ToInt32(row["AlarmVerify"]);
            // 启用本策略
            _Isenable = Convert.ToInt32(row["IsEnable"]);
            // 事件编号
            _Eventid = row["EventID"].ToString().Trim();
            // 
            _Lightid = Convert.ToInt32(row["LightID"]);
            // 
            _Releaselightid = Convert.ToInt32(row["ReleaseLightID"]);
        }
    }
}
