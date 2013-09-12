using System;
using System.Data;

namespace WPFMonitor.Model.AlertAdmin
{
    /// <summary>
    /// 
    /// </summary>
    public class AlarmGroupMembersOR : ORBase
    {

        private int _Id;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                NotifyPropertyChanged("Id");
            }
        }

        private int _Stationid;
        /// <summary>
        /// 站点
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

        private int _Alarmgroupsid;
        /// <summary>
        /// 告警组
        /// </summary>
        public int Alarmgroupsid
        {
            get { return _Alarmgroupsid; }
            set
            {
                _Alarmgroupsid = value;
                NotifyPropertyChanged("Alarmgroupsid");
            }
        }

        private string _Name;
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private string _Mobileno;
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobileno
        {
            get { return _Mobileno; }
            set
            {
                _Mobileno = value;
                NotifyPropertyChanged("Mobileno");
            }
        }

        private string _Telno;
        /// <summary>
        /// 固定号码
        /// </summary>
        public string Telno
        {
            get { return _Telno; }
            set
            {
                _Telno = value;
                NotifyPropertyChanged("Telno");
            }
        }

        private string _Email;
        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                NotifyPropertyChanged("Email");
            }
        }

        
        /// <summary>
        /// 排次
        /// </summary>
        public int Scheduling{get;set;}


        private int _Processlevel;
        /// <summary>
        /// 处理等级
        /// </summary>
        public int Processlevel
        {
            get { return _Processlevel; }
            set
            {
                _Processlevel = value;
                NotifyPropertyChanged("Processlevel");
            }
        }
        private string _StationName;
        /// <summary>
        /// 
        /// </summary>
        public string StationName
        {
            get { return _StationName; }
            set { _StationName = value; NotifyPropertyChanged("StationName"); }
        }
        
        private string _GroupName;
        public string GroupName { get { return _GroupName; }
            set { _GroupName = value; NotifyPropertyChanged("GroupName"); } 
        }

        private string _LevelName;

        public string LevelName
        {
            get { return _LevelName; }
            set { _LevelName = value; NotifyPropertyChanged("LevelName"); }
        }

        private string _FrequencyName;

        public string FrequencyName
        {
            get { return _FrequencyName; }
            set { _FrequencyName = value; NotifyPropertyChanged("FrequencyName"); }
        }

        /// <summary>
        /// AlarmGroupMembers构造函数
        /// </summary>
        public AlarmGroupMembersOR()
        {
            _Name = "";
            _Mobileno = "";
            _Telno = "";
            _Email = "";
        }

        /// <summary>
        /// AlarmGroupMembers构造函数
        /// </summary>f
        public AlarmGroupMembersOR(DataRow row)
        {
            // 
            _Id = Convert.ToInt32(row["ID"]);
            // 站点
            _Stationid = Convert.ToInt32(row["StationID"]);
            // 告警组
            _Alarmgroupsid = Convert.ToInt32(row["AlarmGroupsID"]);
            // 姓名
            _Name = row["Name"].ToString().Trim();
            // 手机
            _Mobileno = row["MobileNo"].ToString().Trim();
            // 固定号码
            _Telno = row["TelNo"].ToString().Trim();
            // Email
            _Email = row["Email"].ToString().Trim();
            // 排次
            Scheduling = Convert.ToInt32(row["Scheduling"]);
            // 处理等级
            _Processlevel = Convert.ToInt32(row["ProcessLevel"]);

            StationName = row["StationName"].ToString();
            GroupName = row["GroupName"].ToString();
            LevelName = row["LevelName"].ToString();
            FrequencyName = row["FrequencyName"].ToString();
        }

        public void Clone(AlarmGroupMembersOR obj)
        {
            //
            Id = obj.Id;
            //站点
            Stationid = obj.Stationid;
            //告警组
            Alarmgroupsid = obj.Alarmgroupsid;
            //姓名
            Name = obj.Name;
            //手机
            Mobileno = obj.Mobileno;
            //固定号码
            Telno = obj.Telno;
            //Email
            Email = obj.Email;
            //排次
            Scheduling = obj.Scheduling;
            //处理等级
            Processlevel = obj.Processlevel;

            StationName = obj.StationName;
            GroupName = obj.GroupName;
            LevelName = obj.LevelName;
            FrequencyName = obj.FrequencyName;

        }

    }
}

