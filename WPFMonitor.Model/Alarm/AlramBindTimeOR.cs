using System;
using System.Data;

namespace WPFMonitor.Model.Alarm
{
    /// <summary>
    /// 
    /// </summary>
    public class AlramBindTimeOR : ORBase
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

        private int _Channelno;
        /// <summary>
        /// 通道名
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

        private int _Intervaltime;
        /// <summary>
        /// 间隔时间
        /// </summary>
        public int Intervaltime
        {
            get { return _Intervaltime; }
            set
            {
                _Intervaltime = value;
                NotifyPropertyChanged("Intervaltime");
            }
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


        private string _ChannelName;
        /// <summary>
        /// 通道
        /// </summary>
        public string ChannelName
        {
            get { return _ChannelName; }
            set { _ChannelName = value; NotifyPropertyChanged("ChannelName"); }
        }

        /// <summary>
        /// AlramBindTime构造函数
        /// </summary>
        public AlramBindTimeOR()
        {

        }

        /// <summary>
        /// AlramBindTime构造函数
        /// </summary>
        public AlramBindTimeOR(DataRow row)
        {
            // 
            _Id = Convert.ToInt32(row["ID"]);
            // 站点
            _Stationid = Convert.ToInt32(row["StationID"]);
            // 设备
            _Deviceid = Convert.ToInt32(row["DeviceID"]);
            // 通道名
            _Channelno = Convert.ToInt32(row["ChannelNo"]);
            // 间隔时间
            _Intervaltime = Convert.ToInt32(row["IntervalTime"]);

            StationName = row["StationName"].ToString();
            DeviceName = row["DeviceName"].ToString();
            ChannelName = row["ChannelName"].ToString();

        }

        public void Clone(AlramBindTimeOR obj)
        {
            //
            Id = obj.Id;
            //站点
            Stationid = obj.Stationid;
            //设备
            Deviceid = obj.Deviceid;
            //通道名
            Channelno = obj.Channelno;
            //间隔时间
            Intervaltime = obj.Intervaltime;

            StationName = obj.StationName;
            DeviceName = obj.DeviceName;
            ChannelName = obj.ChannelName;

        }

    }
}

