using System;
using System.Data;

namespace WPFMonitor.Model.Linkage
{
    /// <summary>
    /// 
    /// </summary>
    public class LinkageSetOR : ORBase
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
                NotifyPropertyChanged("ValueTypeShow");
            }
        }

        private float _Triggervalue;
        /// <summary>
        /// 触动值
        /// </summary>
        public float Triggervalue
        {
            get { return _Triggervalue; }
            set
            {
                _Triggervalue = value;
                NotifyPropertyChanged("Triggervalue");
            }
        }

        private int _Linkagestationid;
        /// <summary>
        /// 联动机房
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
        /// 联动通道
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

        public string ValueTypeShow
        {
            get { 
                string tempValue="控制量";
                if (_Valuetype == 0)
                    tempValue = "模拟量";
                else if (_Valuetype == 1)
                    tempValue = "开关量";
                
                return tempValue;
            }
        }

        #region 联动
        private string _LineStationName;
        /// <summary>
        /// 站点名称
        /// </summary>
        public string LineStationName
        {
            get { return _LineStationName; }
            set { _LineStationName = value; NotifyPropertyChanged("LineStationName"); }
        }

        private string _LineDeviceName;
        /// <summary>
        /// 
        /// </summary>
        public string LineDeviceName
        {
            get { return _LineDeviceName; }
            set { _LineDeviceName = value; NotifyPropertyChanged("LineDeviceName"); }
        }


        private string _LineChannelName;
        /// <summary>
        /// 通道
        /// </summary>
        public string LineChannelName
        {
            get { return _LineChannelName; }
            set { _LineChannelName = value; NotifyPropertyChanged("LineChannelName"); }
        }
        #endregion

        /// <summary>
        /// LinkageSet构造函数
        /// </summary>
        public LinkageSetOR()
        {

        }

        /// <summary>
        /// LinkageSet构造函数
        /// </summary>
        public LinkageSetOR(DataRow row)
        {
            // 
            _Id = Convert.ToInt32(row["ID"]);
            // 站点名称
            _Stationid = Convert.ToInt32(row["StationID"]);
            // 设备名称
            _Deviceid = Convert.ToInt32(row["DeviceID"]);
            // 通道名
            _Channelno = Convert.ToInt32(row["ChannelNo"]);
            // 值类型
            _Valuetype = Convert.ToInt32(row["ValueType"]);
            // 触动值
            _Triggervalue = float.Parse(row["TriggerValue"].ToString());
            // 联动机房
            _Linkagestationid = Convert.ToInt32(row["LinkageStationID"]);
            // 联动设备名称
            _Linkagedeviceid = Convert.ToInt32(row["LinkageDeviceID"]);
            // 联动通道
            _Linkagechannelno = Convert.ToInt32(row["LinkageChannelNo"]);

            StationName = row["StationName"].ToString();
            DeviceName = row["DeviceName"].ToString();
            ChannelName = row["ChannelName"].ToString();

            LineStationName = row["LineStationName"].ToString();
            LineDeviceName = row["LineDeviceName"].ToString();
            LineChannelName = row["LineChannelName"].ToString();
        }

        public void Clone(LinkageSetOR obj)
        {
            //
            Id = obj.Id;
            //站点名称
            Stationid = obj.Stationid;
            //设备名称
            Deviceid = obj.Deviceid;
            //通道名
            Channelno = obj.Channelno;
            //值类型
            Valuetype = obj.Valuetype;
            //触动值
            Triggervalue = obj.Triggervalue;
            //联动机房
            Linkagestationid = obj.Linkagestationid;
            //联动设备名称
            Linkagedeviceid = obj.Linkagedeviceid;
            //联动通道
            Linkagechannelno = obj.Linkagechannelno;

            StationName = obj.StationName;
            DeviceName = obj.DeviceName;
            ChannelName = obj.ChannelName;

            LineStationName = obj.LineStationName;
            LineDeviceName = obj.LineDeviceName;
            LineChannelName = obj.LineChannelName;
        }

    }
}

