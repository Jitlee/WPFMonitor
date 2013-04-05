using System;
using System.Data;

namespace WPFMonitor.Model.Sys
{
    /// <summary>
    /// 
    /// </summary>
    public class DeviceTypeOR : ORBase
    {

        private int _Devicetypeid;
        /// <summary>
        /// 
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

        private string _Typename;
        /// <summary>
        /// 
        /// </summary>
        public string Typename
        {
            get { return _Typename; }
            set
            {
                _Typename = value;
                NotifyPropertyChanged("Typename");
            }
        }

        private string _Parsedll;
        /// <summary>
        /// 
        /// </summary>
        public string Parsedll
        {
            get { return _Parsedll; }
            set
            {
                _Parsedll = value;
                NotifyPropertyChanged("Parsedll");
            }
        }

        private int _Savetimeinteval;
        /// <summary>
        /// 
        /// </summary>
        public int Savetimeinteval
        {
            get { return _Savetimeinteval; }
            set
            {
                _Savetimeinteval = value;
                NotifyPropertyChanged("Savetimeinteval");
            }
        }

        private string _Param;
        /// <summary>
        /// 
        /// </summary>
        public string Param
        {
            get { return _Param; }
            set
            {
                _Param = value;
                NotifyPropertyChanged("Param");
            }
        }

        private string _Ip;
        /// <summary>
        /// 
        /// </summary>
        public string Ip
        {
            get { return _Ip; }
            set
            {
                _Ip = value;
                NotifyPropertyChanged("Ip");
            }
        }

        private int _Stationid;
        /// <summary>
        /// 
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

        /// <summary>
        /// DeviceType构造函数
        /// </summary>
        public DeviceTypeOR()
        {

        }

        /// <summary>
        /// DeviceType构造函数
        /// </summary>
        public DeviceTypeOR(DataRow row)
        {
            // 
            _Devicetypeid = Convert.ToInt32(row["DeviceTypeID"]);
            // 
            _Typename = row["TypeName"].ToString().Trim();
            // 
            _Parsedll = row["ParseDll"].ToString().Trim();
            // 
            _Savetimeinteval = Convert.ToInt32(row["SaveTimeInteval"]);
            // 
            _Param = row["Param"].ToString().Trim();
            // 
            _Ip = row["IP"].ToString().Trim();
            // 
            if (DBNull.Value != row["StationID"])
                _Stationid = Convert.ToInt32(row["StationID"]);
        }

        public void Clone(DeviceTypeOR obj)
        {
            //
            Devicetypeid = obj.Devicetypeid;
            //
            Typename = obj.Typename;
            //
            Parsedll = obj.Parsedll;
            //
            Savetimeinteval = obj.Savetimeinteval;
            //
            Param = obj.Param;
            //
            Ip = obj.Ip;
            //
            Stationid = obj.Stationid;

        }

    }
}

