using System;
using System.Data;
using WPFMonitor.Model;

namespace WPFMonitor.Model.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class t_Channel: ORBase
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

		private int _ChannelNo;
		/// <summary>
		/// 
		/// </summary>
		public int ChannelNo
		{
			get { return _ChannelNo; }
			set { _ChannelNo = value;
NotifyPropertyChanged("ChannelNo");}
		}

		private string _ChannelName;
		/// <summary>
		/// 
		/// </summary>
		public string ChannelName
		{
			get { return _ChannelName; }
			set { _ChannelName = value;
NotifyPropertyChanged("ChannelName");}
		}

		private string _Value0Name;
		/// <summary>
		/// 
		/// </summary>
		public string Value0Name
		{
			get { return _Value0Name; }
			set { _Value0Name = value;
NotifyPropertyChanged("Value0Name");}
		}

		private string _Value1Name;
		/// <summary>
		/// 
		/// </summary>
		public string Value1Name
		{
			get { return _Value1Name; }
			set { _Value1Name = value;
NotifyPropertyChanged("Value1Name");}
		}

		private float _CurrentValue;
		/// <summary>
		/// 
		/// </summary>
		public float CurrentValue
		{
			get { return _CurrentValue; }
			set { _CurrentValue = value;
NotifyPropertyChanged("CurrentValue");}
		}

		private string _ChannelParam;
		/// <summary>
		/// 
		/// </summary>
		public string ChannelParam
		{
			get { return _ChannelParam; }
			set { _ChannelParam = value;
NotifyPropertyChanged("ChannelParam");}
		}

		/// <summary>
		/// Channel构造函数
		/// </summary>
		public t_Channel()
		{

		}

		/// <summary>
		/// Channel构造函数
		/// </summary>
		public t_Channel(DataRow row)
		{
			// 
			_DeviceID = Convert.ToInt32(row["DeviceID"]);
			// 
			_ChannelNo = Convert.ToInt32(row["ChannelNo"]);
			// 
			_ChannelName = row["ChannelName"].ToString().Trim();
			// 
			_Value0Name = row["Value0_Name"].ToString().Trim();
			// 
			_Value1Name = row["Value1_Name"].ToString().Trim();
			// 
			_CurrentValue = float.Parse(row["CurrentValue"].ToString());
			// 
			_ChannelParam = row["ChannelParam"].ToString().Trim();
		}

	public void Clone(t_Channel obj){
//
DeviceID = obj.DeviceID;
//
ChannelNo = obj.ChannelNo;
//
ChannelName = obj.ChannelName;
//
Value0Name = obj.Value0Name;
//
Value1Name = obj.Value1Name;
//
CurrentValue = obj.CurrentValue;
//
ChannelParam = obj.ChannelParam;

}

    }
}

