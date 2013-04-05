using System;
using System.Data;

namespace WPFMonitor.Model.Alarm
{
    /// <summary>
    /// 
    /// </summary>
    public class InspectionOR : ORBase
    {

        private int _Inspectionid;
        /// <summary>
        /// 
        /// </summary>
        public int Inspectionid
        {
            get { return _Inspectionid; }
            set
            {
                _Inspectionid = value;
                NotifyPropertyChanged("Inspectionid");
            }
        }

        private int _Stationid;
        /// <summary>
        /// 站点名称
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

        private int _Channelno;
        /// <summary>
        /// 通道号
        /// </summary>
        public int Channelno
        {
            get { return _Channelno; }
            set
            {
                _Channelno = value;
                NotifyPropertyChanged("Channelno");
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

        private string _Smsemail;
        /// <summary>
        /// 短信及邮件内容
        /// </summary>
        public string Smsemail
        {
            get { return _Smsemail; }
            set
            {
                _Smsemail = value;
                NotifyPropertyChanged("Smsemail");
            }
        }

        private string _Phonemedia;
        /// <summary>
        /// 电话语音文件
        /// </summary>
        public string Phonemedia
        {
            get { return _Phonemedia; }
            set
            {
                _Phonemedia = value;
                NotifyPropertyChanged("Phonemedia");
            }
        }

        private string _Inspectiontime;
        /// <summary>
        /// 巡检时间
        /// </summary>
        public string Inspectiontime
        {
            get { return _Inspectiontime; }
            set
            {
                _Inspectiontime = value;
                NotifyPropertyChanged("Inspectiontime");
            }
        }

        private string _Inspectiontype;
        /// <summary>
        /// 巡检类型
        /// </summary>
        public string Inspectiontype
        {
            get { return _Inspectiontype; }
            set
            {
                _Inspectiontype = value;
                NotifyPropertyChanged("Inspectiontype");
            }
        }

        private int _Valuetype;
        /// <summary>
        /// 值类型
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



        /// <summary>
        /// 设备类型
        /// </summary>
        public string TypeName { get; set; }

        private string _StationName;
        /// <summary>
        /// 站点名称
        /// </summary>
        public string StationName
        {
            get { return _StationName; }
            set { _StationName = value; }
        }

        private string _DeviceName;
        /// <summary>
        /// 
        /// </summary>
        public string DeviceName
        {
            get { return _DeviceName; }
            set { _DeviceName = value; }
        }


        private string _ChannelName;
        /// <summary>
        /// 通道
        /// </summary>
        public string ChannelName
        {
            get { return _ChannelName; }
            set { _ChannelName = value; }
        }

        /// <summary>
        /// Inspection构造函数
        /// </summary>
        public InspectionOR()
        {
            Phonemedia = "";
            Inspectiontype = "";
            Smsemail = "";
        }

        /// <summary>
        /// Inspection构造函数
        /// </summary>
        public InspectionOR(DataRow row)
        {
            // 
            _Inspectionid = Convert.ToInt32(row["InspectionID"]);
            // 站点名称
            _Stationid = Convert.ToInt32(row["StationID"]);
            // 设备类型
            _Devicetypeid = Convert.ToInt32(row["DeviceTypeID"]);
            // 设备名称
            _Deviceid = Convert.ToInt32(row["DeviceID"]);
            // 通道号
            _Channelno = Convert.ToInt32(row["ChannelNO"]);
            // 告警方式
            _Alarmway = row["AlarmWay"].ToString().Trim();
            // 短信及邮件内容
            _Smsemail = row["SmsEmail"].ToString().Trim();
            // 电话语音文件
            _Phonemedia = row["PhoneMedia"].ToString().Trim();
            // 巡检时间
            _Inspectiontime = row["InspectionTime"].ToString().Trim();
            // 巡检类型
            _Inspectiontype = row["InspectionType"].ToString().Trim();
            // 值类型
            _Valuetype = Convert.ToInt32(row["ValueType"]);

            StationName = row["StationName"].ToString();
            DeviceName = row["DeviceName"].ToString();
            ChannelName = row["ChannelName"].ToString();
            TypeName = row["TypeName"].ToString();
        }

        public void Clone(InspectionOR obj)
        {
            //
            Inspectionid = obj.Inspectionid;
            //站点名称
            Stationid = obj.Stationid;
            //设备类型
            Devicetypeid = obj.Devicetypeid;
            //设备名称
            Deviceid = obj.Deviceid;
            //通道号
            Channelno = obj.Channelno;
            //告警方式
            Alarmway = obj.Alarmway;
            //短信及邮件内容
            Smsemail = obj.Smsemail;
            //电话语音文件
            Phonemedia = obj.Phonemedia;
            //巡检时间
            Inspectiontime = obj.Inspectiontime;
            //巡检类型
            Inspectiontype = obj.Inspectiontype;
            //值类型
            Valuetype = obj.Valuetype;

            StationName = obj.StationName;
            DeviceName = obj.DeviceName;
            ChannelName = obj.ChannelName;
            TypeName = obj.TypeName;
        }

    }
}

