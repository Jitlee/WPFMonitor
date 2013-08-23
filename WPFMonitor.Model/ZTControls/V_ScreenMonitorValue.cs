using System;
using System.Data;
using WPFMonitor.Model;

namespace WPFMonitor.Model.ZTControls
{
	/// <summary>
	/// 
	/// </summary>
	public class V_ScreenMonitorValue : ORBase
	{

		//private string _ID;
		///// <summary>
		///// 
		///// </summary>
		//public string ID
		//{
		//    get { return _ID; }
		//    set { _ID = value;
		//    NotifyPropertyChanged("ID");
		//    }
		//}

		private int _ElementID;
		/// <summary>
		/// 
		/// </summary>
		public int ElementID
		{
			get { return _ElementID; }
			set
			{
				_ElementID = value;
				NotifyPropertyChanged("ElementID");
			}
		}

		private int _Screenid;
		/// <summary>
		/// 
		/// </summary>
		public int ScreenID
		{
			get { return _Screenid; }
			set
			{
				_Screenid = value;
				NotifyPropertyChanged("ScreenID");
			}
		}

		private int _DeviceID;
		/// <summary>
		/// 
		/// </summary>
		public int DeviceID
		{
			get { return _DeviceID; }
			set
			{
				_DeviceID = value;
				NotifyPropertyChanged("DeviceID");
			}
		}

		private int _ChannelNo;
		/// <summary>
		/// 
		/// </summary>
		public int ChannelNo
		{
			get { return _ChannelNo; }
			set
			{
				_ChannelNo = value;
				NotifyPropertyChanged("ChannelNo");
			}
		}

		private string _ComputeStr;
		/// <summary>
		/// 
		/// </summary>
		public string ComputeStr
		{
			get { return _ComputeStr; }
			set
			{
				_ComputeStr = value;
				NotifyPropertyChanged("ComputeStr");
			}
		}

		private int _StationID;
		/// <summary>
		/// 
		/// </summary>
		public int StationID
		{
			get { return _StationID; }
			set
			{
				_StationID = value;
				NotifyPropertyChanged("StationID");
			}
		}

		private int _ChanenlSubNo;
		/// <summary>
		/// 
		/// </summary>
		public int ChanenlSubNo
		{
			get { return _ChanenlSubNo; }
			set
			{
				_ChanenlSubNo = value;
				NotifyPropertyChanged("ChanenlSubNo");
			}
		}

		private float _MonitorValue;
		/// <summary>
		/// 
		/// </summary>
		public float MonitorValue
		{
			get { return _MonitorValue; }
			set
			{
				_MonitorValue = value;
				NotifyPropertyChanged("MonitorValue");
			}
		}

		private int _Flag;
		/// <summary>
		/// 
		/// </summary>
		public int Flag
		{
			get { return _Flag; }
			set
			{
				_Flag = value;
				NotifyPropertyChanged("Flag");
			}
		}

		/// <summary>
		/// ScreenMonitorValue构造函数
		/// </summary>
		public V_ScreenMonitorValue()
		{

		}

		/// <summary>
		/// ScreenMonitorValue构造函数
		/// </summary>
		public V_ScreenMonitorValue(DataRow row)
		{
			// 
			//_ID = row["id"].ToString().Trim();
			// 
			_ElementID = Convert.ToInt32(row["ElementID"]);
			// 
			_Screenid = Convert.ToInt32(row["ScreenID"]);
			// 
			_DeviceID = Convert.ToInt32(row["DeviceID"]);
			// 
			_ChannelNo = Convert.ToInt32(row["ChannelNo"]);
			// 
			_ComputeStr = row["ComputeStr"].ToString().Trim();
			// 
			_StationID = Convert.ToInt32(row["StationID"]);
			// 
			_ChanenlSubNo = Convert.ToInt32(row["ChanenlSubNo"]);
			// 
			_MonitorValue = float.Parse(row["MonitorValue"].ToString());
			// 
			_Flag = Convert.ToInt32(row["flag"]);
		}

		public void Clone(V_ScreenMonitorValue obj)
		{
			//
			//ID = obj.ID;
			//
			ElementID = obj.ElementID;
			//
			ScreenID = obj.ScreenID;
			//
			DeviceID = obj.DeviceID;
			//
			ChannelNo = obj.ChannelNo;
			//
			ComputeStr = obj.ComputeStr;
			//
			StationID = obj.StationID;
			//
			ChanenlSubNo = obj.ChanenlSubNo;
			//
			MonitorValue = obj.MonitorValue;
			//
			Flag = obj.Flag;

		}

	}
}

