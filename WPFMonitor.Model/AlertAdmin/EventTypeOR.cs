using System;
using System.Data;
using WPFMonitor.Model;

namespace WPFMonitor.Model.AlertAdmin
{
    /// <summary>
    /// 
    /// </summary>
    public class EventTypeOR : ORBase
    {

        private int _Eventid;
        /// <summary>
        /// 
        /// </summary>
        public int Eventid
        {
            get { return _Eventid; }
            set
            {
                _Eventid = value;
                NotifyPropertyChanged("Eventid");
            }
        }

        private string _Eventname;
        /// <summary>
        /// 事件名称
        /// </summary>
        public string Eventname
        {
            get { return _Eventname; }
            set
            {
                _Eventname = value;
                NotifyPropertyChanged("Eventname");
            }
        }

        private int _Alarmlevel;
        /// <summary>
        /// 事件级别
        /// </summary>
        public int Alarmlevel
        {
            get { return _Alarmlevel; }
            set
            {
                _Alarmlevel = value;
                NotifyPropertyChanged("Alarmlevel");
                NotifyPropertyChanged("AlarmlevelShow");
            }
        }

        public string AlarmlevelShow { get { return string.Format("{0}级", Alarmlevel); } }

        private string _Alarmtarget;
        /// <summary>
        /// 报警组
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
        /// 报警方式
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
        /// 是否班次报警
        /// </summary>
        public int Isenablefrequency
        {
            get { return _Isenablefrequency; }
            set
            {
                _Isenablefrequency = value;
                NotifyPropertyChanged("Isenablefrequency");
                NotifyPropertyChanged("IsenablefrequencyBool");
            }
        }

        public bool IsenablefrequencyBool
        {
            set {
                if (value == true)
                    Isenablefrequency = 1;
                else
                    Isenablefrequency = 0;
            }
            get
            {
                if (_Isenablefrequency == 1)
                    return true;
                else
                    return false;
            }
        }
        public string IsenablefrequencyShow
        {
            get
            {
                if (Isenablefrequency == 1) return "是"; else return "否";
            }
        }

        private string _Alarmaudiofile;
        /// <summary>
        /// 电话语音文件
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
        /// 电话解出语音文件
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

        private string _Smsmsg;
        /// <summary>
        /// 
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

        private string _Disarmid;
        /// <summary>
        /// 
        /// </summary>
        public string Disarmid
        {
            get { return _Disarmid; }
            set
            {
                _Disarmid = value;
                NotifyPropertyChanged("Disarmid");
            }
        }

        /// <summary>
        /// EventType构造函数
        /// </summary>
        public EventTypeOR()
        {

        }

        /// <summary>
        /// EventType构造函数
        /// </summary>
        public EventTypeOR(DataRow row)
        {
            // 
            _Eventid = Convert.ToInt32(row["EventID"]);
            // 事件名称
            _Eventname = row["EventName"].ToString().Trim();
            // 事件级别
            _Alarmlevel = Convert.ToInt32(row["AlarmLevel"]);
            // 报警组
            _Alarmtarget = row["AlarmTarget"].ToString().Trim();
            // 报警方式
            _Alarmway = row["AlarmWay"].ToString().Trim();
            // 是否班次报警
            _Isenablefrequency = Convert.ToInt32(row["IsEnableFrequency"]);
            // 电话语音文件
            _Alarmaudiofile = row["AlarmAudioFile"].ToString().Trim();
            // 电话解出语音文件
            _Disalarmaudiofile = row["DisAlarmAudioFile"].ToString().Trim();
            // 
            _Smsmsg = row["SmsMsg"].ToString().Trim();
            // 
            _Disarmid = row["DisarmID"].ToString().Trim();
        }

        public void Clone(EventTypeOR obj)
        {
            //
            Eventid = obj.Eventid;
            //事件名称
            Eventname = obj.Eventname;
            //事件级别
            Alarmlevel = obj.Alarmlevel;
            //报警组
            Alarmtarget = obj.Alarmtarget;
            //报警方式
            Alarmway = obj.Alarmway;
            //是否班次报警
            Isenablefrequency = obj.Isenablefrequency;
            //电话语音文件
            Alarmaudiofile = obj.Alarmaudiofile;
            //电话解出语音文件
            Disalarmaudiofile = obj.Disalarmaudiofile;
            //
            Smsmsg = obj.Smsmsg;
            //
            Disarmid = obj.Disarmid;

        }

    }
}

