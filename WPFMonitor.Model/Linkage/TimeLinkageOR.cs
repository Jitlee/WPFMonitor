using System;
using System.Data;

namespace WPFMonitor.Model.Linkage
{
    /// <summary>
    /// 
    /// </summary>
    public class TimeLinkageOR : ORBase
    {

        private int _Linkageid;
        /// <summary>
        /// 
        /// </summary>
        public int Linkageid
        {
            get { return _Linkageid; }
            set
            {
                _Linkageid = value;
                NotifyPropertyChanged("Linkageid");
            }
        }

        private int _Linkagestationid;
        /// <summary>
        /// 联动机房名称
        /// </summary>
        public int Linkagestationid
        {
            get { return _Linkagestationid; }
            set
            {
                _Linkagestationid = value;
                NotifyPropertyChanged("Linkagestationid");
            }
        }

        private int _Linkagedeviceid;
        /// <summary>
        /// 联动设备名称
        /// </summary>
        public int Linkagedeviceid
        {
            get { return _Linkagedeviceid; }
            set
            {
                _Linkagedeviceid = value;
                NotifyPropertyChanged("Linkagedeviceid");
            }
        }

        private int _Linkagechannelno;
        /// <summary>
        /// 联动通道名称
        /// </summary>
        public int Linkagechannelno
        {
            get { return _Linkagechannelno; }
            set
            {
                _Linkagechannelno = value;
                NotifyPropertyChanged("Linkagechannelno");
            }
        }

        private string _Triggertime;
        /// <summary>
        /// 联动时间
        /// </summary>
        public string Triggertime
        {
            get { return _Triggertime; }
            set
            {
                _Triggertime = value;
                NotifyPropertyChanged("Triggertime");
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
        /// TimeLinkage构造函数
        /// </summary>
        public TimeLinkageOR()
        {
            _Triggertime = "";
        }

        /// <summary>
        /// TimeLinkage构造函数
        /// </summary>
        public TimeLinkageOR(DataRow row)
        {
            // 
            _Linkageid = Convert.ToInt32(row["LinkageID"]);
            // 联动机房名称
            _Linkagestationid = Convert.ToInt32(row["LinkageStationID"]);
            // 联动设备名称
            _Linkagedeviceid = Convert.ToInt32(row["LinkageDeviceID"]);
            // 联动通道名称
            _Linkagechannelno = Convert.ToInt32(row["LinkageChannelNo"]);
            // 联动时间
            _Triggertime = row["TriggerTime"].ToString().Trim();

            StationName = row["StationName"].ToString();
            DeviceName = row["DeviceName"].ToString();
            ChannelName = row["ChannelName"].ToString();
        }

        public void Clone(TimeLinkageOR obj)
        {
            //
            Linkageid = obj.Linkageid;
            //联动机房名称
            Linkagestationid = obj.Linkagestationid;
            //联动设备名称
            Linkagedeviceid = obj.Linkagedeviceid;
            //联动通道名称
            Linkagechannelno = obj.Linkagechannelno;
            //联动时间
            Triggertime = obj.Triggertime;

            StationName = obj.StationName;
            DeviceName = obj.DeviceName;
            ChannelName = obj.ChannelName;
        }

    }
}

