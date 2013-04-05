using System;
using System.Data;

namespace WPFMonitor.Model.Sys
{
    /// <summary>
    /// 
    /// </summary>
    public class DeviceOR : ORBase
    {

        private int _Deviceid;
        /// <summary>
        /// 
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

        private string _Devicename;
        /// <summary>
        /// 
        /// </summary>
        public string Devicename
        {
            get { return _Devicename; }
            set
            {
                _Devicename = value;
                NotifyPropertyChanged("Devicename");
            }
        }

        private int _Communicatetype;
        /// <summary>
        /// 
        /// </summary>
        public int Communicatetype
        {
            get { return _Communicatetype; }
            set
            {
                _Communicatetype = value;
                NotifyPropertyChanged("Communicatetype");
            }
        }

        private int _Communicateid;
        /// <summary>
        /// 
        /// </summary>
        public int Communicateid
        {
            get { return _Communicateid; }
            set
            {
                _Communicateid = value;
                NotifyPropertyChanged("Communicateid");
            }
        }

        private string _Subaddr;
        /// <summary>
        /// 
        /// </summary>
        public string Subaddr
        {
            get { return _Subaddr; }
            set
            {
                _Subaddr = value;
                NotifyPropertyChanged("Subaddr");
            }
        }

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

        private string _Stationname;
        /// <summary>
        /// 
        /// </summary>
        public string Stationname
        {
            get { return _Stationname; }
            set
            {
                _Stationname = value;
                NotifyPropertyChanged("Stationname");
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

        private string _Userid;
        /// <summary>
        /// 
        /// </summary>
        public string Userid
        {
            get { return _Userid; }
            set
            {
                _Userid = value;
                NotifyPropertyChanged("Userid");
            }
        }

        private string _Password;
        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                NotifyPropertyChanged("Password");
            }
        }

        private int _Enable;
        /// <summary>
        /// 
        /// </summary>
        public int Enable
        {
            get { return _Enable; }
            set
            {
                _Enable = value;
                NotifyPropertyChanged("Enable");
            }
        }

        private int _Port;
        /// <summary>
        /// 
        /// </summary>
        public int Port
        {
            get { return _Port; }
            set
            {
                _Port = value;
                NotifyPropertyChanged("Port");
            }
        }

        /// <summary>
        /// Device构造函数
        /// </summary>
        public DeviceOR()
        {

        }

        /// <summary>
        /// Device构造函数
        /// </summary>
        public DeviceOR(DataRow row)
        {
            // 
            _Deviceid = Convert.ToInt32(row["DeviceID"]);
            // 
            _Devicename = row["DeviceName"].ToString().Trim();
            // 
            _Communicatetype = Convert.ToInt32(row["CommunicateType"]);
            // 
            _Communicateid = Convert.ToInt32(row["CommunicateID"]);
            // 
            _Subaddr = row["SubAddr"].ToString().Trim();
            // 
            _Devicetypeid = Convert.ToInt32(row["DeviceTypeID"]);
            // 
            _Stationid = Convert.ToInt32(row["StationID"]);
            // 
            _Stationname = row["StationName"].ToString().Trim();
            // 
            _Ip = row["IP"].ToString().Trim();
            // 
            _Userid = row["UserId"].ToString().Trim();
            // 
            _Password = row["Password"].ToString().Trim();
            // 
            _Enable = Convert.ToInt32(row["Enable"]);
            //
            if (row["Port"] != DBNull.Value)
                _Port = Convert.ToInt32(row["Port"]);
        }

        public void Clone(DeviceOR obj)
        {
            //
            Deviceid = obj.Deviceid;
            //
            Devicename = obj.Devicename;
            //
            Communicatetype = obj.Communicatetype;
            //
            Communicateid = obj.Communicateid;
            //
            Subaddr = obj.Subaddr;
            //
            Devicetypeid = obj.Devicetypeid;
            //
            Stationid = obj.Stationid;
            //
            Stationname = obj.Stationname;
            //
            Ip = obj.Ip;
            //
            Userid = obj.Userid;
            //
            Password = obj.Password;
            //
            Enable = obj.Enable;
            //
            Port = obj.Port;

        }

    }
}

