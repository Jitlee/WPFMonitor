using System;
using System.Data;
using WPFMonitor.Model;

namespace WPFMonitor.Model.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class t_MonitorSystemParam: ORBase
    {
       
		private int _MonitorRefreshTime;
		/// <summary>
		/// 
		/// </summary>
		public int MonitorRefreshTime
		{
			get { return _MonitorRefreshTime; }
			set { _MonitorRefreshTime = value;
            NotifyPropertyChanged("MonitorRefreshTime");
            }
		}

		private int _StartScreenID;
		/// <summary>
		/// 
		/// </summary>
        public int StartScreenID
		{
			get { return _StartScreenID; }
			set { _StartScreenID = value;
            NotifyPropertyChanged("StartScreenID");
            }
		}

		private int _AlarmLogTime;
		/// <summary>
		/// 报警框刷新时间
		/// </summary>
        public int AlarmLogTime
		{
			get { return _AlarmLogTime; }
			set { _AlarmLogTime = value;
            NotifyPropertyChanged("AlarmLogTime");
            }
		}

		private string _ServerIP;
		/// <summary>
		/// 
		/// </summary>
        public string ServerIP
		{
			get { return _ServerIP; }
			set { _ServerIP = value;
            NotifyPropertyChanged("ServerIP");
            }
		}

		private int _ServerPort;
		/// <summary>
		/// 
		/// </summary>
        public int ServerPort
		{
			get { return _ServerPort; }
			set { _ServerPort = value;
            NotifyPropertyChanged("ServerPort");
            }
		}

		private string _Door_Sysid;
		/// <summary>
		/// 
		/// </summary>
        public string Door_Sysid
		{
			get { return _Door_Sysid; }
			set { _Door_Sysid = value;
            NotifyPropertyChanged("Door_Sysid");
            }
		}

		private int _Door_Com;
		/// <summary>
		/// 
		/// </summary>
		public int Door_Com
		{
			get { return _Door_Com; }
			set { _Door_Com = value;
            NotifyPropertyChanged("Door_Com");
            }
		}

		private int _HaveDoor;
		/// <summary>
		/// 
		/// </summary>
        public int HaveDoor
		{
			get { return _HaveDoor; }
			set { _HaveDoor = value;
            NotifyPropertyChanged("HaveDoor");
            }
		}

		/// <summary>
		/// MonitorSystemParam构造函数
		/// </summary>
		public t_MonitorSystemParam()
		{

		}

		/// <summary>
		/// MonitorSystemParam构造函数
		/// </summary>
		public t_MonitorSystemParam(DataRow row)
		{
			// 
			_MonitorRefreshTime = Convert.ToInt32(row["MonitorRefreshTime"]);
			// 
			_StartScreenID = Convert.ToInt32(row["StartScreenID"]);
			// 报警框刷新时间
			_AlarmLogTime = Convert.ToInt32(row["AlarmLogTime"]);
			// 
			_ServerIP = row["ServerIP"].ToString().Trim();
			// 
			_ServerPort = Convert.ToInt32(row["ServerPort"]);
			// 
			_Door_Sysid = row["Door_Sysid"].ToString().Trim();
			// 
			_Door_Com = Convert.ToInt32(row["Door_Com"]);
			// 
			_HaveDoor = Convert.ToInt32(row["HaveDoor"]);
		}

        public void Clone(t_MonitorSystemParam obj)
        {
            //
            MonitorRefreshTime = obj.MonitorRefreshTime;
            //
            StartScreenID = obj.StartScreenID;
            //报警框刷新时间
            AlarmLogTime = obj.AlarmLogTime;
            //
            ServerIP = obj.ServerIP;
            //
            ServerPort = obj.ServerPort;
            //
            Door_Sysid = obj.Door_Sysid;
            //
            Door_Com = obj.Door_Com;
            //
            HaveDoor = obj.HaveDoor;

        }

    }
}

