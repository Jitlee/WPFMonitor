using System;
using System.Data;

namespace WPFMonitor.Model.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class t_Device: ORBase
    {
       
		private int _DeviceID;
		/// <summary>
		/// 
		/// </summary>
		public int DeviceID
		{
			get { return _DeviceID; }
			set { _DeviceID = value;
NotifyPropertyChanged("DeviceID");}
		}

		private string _DeviceName;
		/// <summary>
		/// 
		/// </summary>
		public string DeviceName
		{
			get { return _DeviceName; }
			set { _DeviceName = value;
NotifyPropertyChanged("DeviceName");}
		}

		private int _CommunicateType;
		/// <summary>
		/// 
		/// </summary>
		public int Communicatetype
		{
			get { return _CommunicateType; }
			set { _CommunicateType = value;
NotifyPropertyChanged("CommunicateType");}
		}

		private int _CommunicateID;
		/// <summary>
		/// 
		/// </summary>
		public int CommunicateID
		{
			get { return _CommunicateID; }
			set { _CommunicateID = value;
NotifyPropertyChanged("CommunicateID");}
		}

		private string _SubAddr;
		/// <summary>
		/// 
		/// </summary>
		public string SubAddr
		{
			get { return _SubAddr; }
			set { _SubAddr = value;
NotifyPropertyChanged("SubAddr");}
		}

		private int _DeviceTypeID;
		/// <summary>
		/// 
		/// </summary>
		public int DeviceTypeID
		{
			get { return _DeviceTypeID; }
			set { _DeviceTypeID = value;
NotifyPropertyChanged("DevicetypeID");}
		}

		private int _StationID;
		/// <summary>
		/// 
		/// </summary>
		public int StationID
		{
			get { return _StationID; }
			set { _StationID = value;
NotifyPropertyChanged("StationID");}
		}

		private string _StationName;
		/// <summary>
		/// 
		/// </summary>
		public string StationName
		{
			get { return _StationName; }
			set { _StationName = value;
NotifyPropertyChanged("StationName");}
		}

		private string _IP;
		/// <summary>
		/// 
		/// </summary>
		public string IP
		{
			get { return _IP; }
			set { _IP = value;
NotifyPropertyChanged("IP");}
		}

		private string _UserID;
		/// <summary>
		/// 
		/// </summary>
		public string UserID
		{
			get { return _UserID; }
			set { _UserID = value;
NotifyPropertyChanged("UserID");}
		}

		private string _Password;
		/// <summary>
		/// 
		/// </summary>
		public string Password
		{
			get { return _Password; }
			set { _Password = value;
NotifyPropertyChanged("Password");}
		}

		private int _Enable;
		/// <summary>
		/// 
		/// </summary>
		public int Enable
		{
			get { return _Enable; }
			set { _Enable = value;
NotifyPropertyChanged("Enable");}
		}

		private int _Port;
		/// <summary>
		/// 
		/// </summary>
		public int Port
		{
			get { return _Port; }
			set { _Port = value;
NotifyPropertyChanged("Port");}
		}

		/// <summary>
		/// Device构造函数
		/// </summary>
		public t_Device()
		{

		}

		/// <summary>
		/// Device构造函数
		/// </summary>
		public t_Device(DataRow row)
		{
			// 
			_DeviceID = Converter.ToInt32(row["DeviceID"]);
			// 
			_DeviceName = row["DeviceName"].ToString().Trim();
			// 
			_CommunicateType = Converter.ToInt32(row["CommunicateType"]);
			// 
			_CommunicateID = Converter.ToInt32(row["CommunicateID"]);
			// 
			_SubAddr = row["SubAddr"].ToString().Trim();
			// 
			_DeviceTypeID = Converter.ToInt32(row["DeviceTypeID"]);
			// 
			_StationID = Converter.ToInt32(row["StationID"]);
			// 
			_StationName = row["StationName"].ToString().Trim();
			// 
			_IP = row["IP"].ToString().Trim();
			// 
			_UserID = row["UserId"].ToString().Trim();
			// 
			_Password = row["Password"].ToString().Trim();
			// 
			_Enable = Converter.ToInt32(row["Enable"]);
			// 
			_Port = Converter.ToInt32(row["Port"]);
		}

	public void Clone(t_Device obj){
//
DeviceID = obj.DeviceID;
//
DeviceName = obj.DeviceName;
//
Communicatetype = obj.Communicatetype;
//
CommunicateID = obj.CommunicateID;
//
SubAddr = obj.SubAddr;
//
DeviceTypeID = obj.DeviceTypeID;
//
StationID = obj.StationID;
//
StationName = obj.StationName;
//
IP = obj.IP;
//
UserID = obj.UserID;
//
Password = obj.Password;
//
Enable = obj.Enable;
//
Port = obj.Port;

}

    }
}

