using System;
using System.Data;

namespace WPFMonitor.Model.AlertAdmin
{
    /// <summary>
    /// 
    /// </summary>
    public class MainteOR : ORBase
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

        private int _Deviceid;
        /// <summary>
        /// 设备
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

        private int _Valuetype;
        /// <summary>
        /// 维修状态
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

        private DateTime _Maintetime;
        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime Maintetime
        {
            get { return _Maintetime; }
            set
            {
                _Maintetime = value;
                NotifyPropertyChanged("Maintetime");
            }
        }

        private string _Maintename;
        /// <summary>
        /// 维修人员
        /// </summary>
        public string Maintename
        {
            get { return _Maintename; }
            set
            {
                _Maintename = value;
                NotifyPropertyChanged("Maintename");
            }
        }

        private string _Duty;
        /// <summary>
        /// 值班人员
        /// </summary>
        public string Duty
        {
            get { return _Duty; }
            set
            {
                _Duty = value;
                NotifyPropertyChanged("Duty");
            }
        }

        private string _Contentmainte;
        /// <summary>
        /// 检修内容
        /// </summary>
        public string Contentmainte
        {
            get { return _Contentmainte; }
            set
            {
                _Contentmainte = value;
                NotifyPropertyChanged("Contentmainte");
            }
        }

        /// <summary>
        /// Mainte构造函数
        /// </summary>
        public MainteOR()
        {
            Maintetime = DateTime.Now;
            _Maintename ="";
            _Duty = "";
            _Contentmainte ="";
        }


        private string _StationName;
        /// <summary>
        /// 站点名称
        /// </summary>
        public string StationName
        {
            get { return _StationName; }
            set { _StationName = value; NotifyPropertyChanged("StationName"); }
        }

        private string _DeviceName;
        /// <summary>
        /// 
        /// </summary>
        public string DeviceName
        {
            get { return _DeviceName; }
            set { _DeviceName = value; NotifyPropertyChanged("DeviceName"); }
        }

        /// <summary>
        /// Mainte构造函数
        /// </summary>
        public MainteOR(DataRow row)
        {
            StationName = row["StationName"].ToString();
            DeviceName = row["DeviceName"].ToString();

            // 
            _Id = Convert.ToInt32(row["ID"]);
            // 设备
            _Deviceid = Convert.ToInt32(row["DeviceID"]);
            // 站点
            _Stationid = Convert.ToInt32(row["StationID"]);
            // 维修状态
            _Valuetype = Convert.ToInt32(row["ValueType"]);
            // 维修时间
            _Maintetime = Convert.ToDateTime(row["MainteTime"]);
            // 维修人员
            _Maintename = row["MainteName"].ToString().Trim();
            // 值班人员
            _Duty = row["Duty"].ToString().Trim();
            // 检修内容
            _Contentmainte = row["ContentMainte"].ToString().Trim();
        }

        public void Clone(MainteOR obj)
        {
            //
            Id = obj.Id;
            //设备
            Deviceid = obj.Deviceid;
            //站点
            Stationid = obj.Stationid;
            //维修状态
            Valuetype = obj.Valuetype;
            //维修时间
            Maintetime = obj.Maintetime;
            //维修人员
            Maintename = obj.Maintename;
            //值班人员
            Duty = obj.Duty;
            //检修内容
            Contentmainte = obj.Contentmainte;

            StationName = obj.StationName;
            DeviceName = obj.DeviceName;

        }

    }
}

