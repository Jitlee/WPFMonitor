using System;
using System.Data;

namespace WPFMonitor.Model.AlertAdmin
{
    /// <summary>
    /// 
    /// </summary>
    public class AlarmGroupsOR : ORBase
    {

        private int _Alarmgroupsid;
        /// <summary>
        /// 
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

        private string _Groupname;
        /// <summary>
        /// 名称
        /// </summary>
        public string Groupname
        {
            get { return _Groupname; }
            set
            {
                _Groupname = value;
                NotifyPropertyChanged("Groupname");
            }
        }

        public string StationName { get; set; }

        /// <summary>
        /// AlarmGroups构造函数
        /// </summary>
        public AlarmGroupsOR()
        {

        }

        /// <summary>
        /// AlarmGroups构造函数
        /// </summary>
        public AlarmGroupsOR(DataRow row)
        {
            // 
            _Alarmgroupsid = Convert.ToInt32(row["AlarmGroupsID"]);
            // 站点
            _Stationid = Convert.ToInt32(row["StationID"]);
            // 名称
            _Groupname = row["GroupName"].ToString().Trim();

            StationName = row["StationName"].ToString().Trim();
        }

        public void Clone(AlarmGroupsOR obj)
        {
            //
            Alarmgroupsid = obj.Alarmgroupsid;
            //站点
            Stationid = obj.Stationid;
            //名称
            Groupname = obj.Groupname;

            StationName = obj.StationName;

        }

    }
}

