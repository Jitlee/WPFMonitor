using System;
using System.Data;
using WPFMonitor.Model;

namespace WPFMonitor.Model.AlertAdmin
{
    /// <summary>
    /// 
    /// </summary>
    public class LightAlarmOR: ORBase
    {
       
		private int _Lightid;
		/// <summary>
		/// 
		/// </summary>
		public int Lightid
		{
			get { return _Lightid; }
			set { _Lightid = value;
NotifyPropertyChanged("Lightid");}
		}

		private string _Lightname;
		/// <summary>
		/// 
		/// </summary>
		public string Lightname
		{
			get { return _Lightname; }
			set { _Lightname = value;
NotifyPropertyChanged("Lightname");}
		}

		private int _Deviceid;
		/// <summary>
		/// 
		/// </summary>
		public int Deviceid
		{
			get { return _Deviceid; }
			set { _Deviceid = value;
NotifyPropertyChanged("Deviceid");}
		}

		private int _Channelno;
		/// <summary>
		/// 
		/// </summary>
		public int Channelno
		{
			get { return _Channelno; }
			set { _Channelno = value;
NotifyPropertyChanged("Channelno");}
		}

		/// <summary>
		/// LightAlarm构造函数
		/// </summary>
		public LightAlarmOR()
		{

		}

		/// <summary>
		/// LightAlarm构造函数
		/// </summary>
		public LightAlarmOR(DataRow row)
		{
			// 
			_Lightid = Convert.ToInt32(row["LightID"]);
			// 
			_Lightname = row["LightName"].ToString().Trim();
			// 
			_Deviceid = Convert.ToInt32(row["DeviceID"]);
			// 
			_Channelno = Convert.ToInt32(row["ChannelNO"]);
		}

	public void Clone(LightAlarmOR obj){
//
Lightid = obj.Lightid;
//
Lightname = obj.Lightname;
//
Deviceid = obj.Deviceid;
//
Channelno = obj.Channelno;

}

    }
}

