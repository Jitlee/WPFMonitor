using System;
using System.Data;

namespace WPFMonitor.Model.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class t_ScreenLibrary : ORBase
    {
       
		private int _Screenid;
		/// <summary>
		/// 
		/// </summary>
		public int Screenid
		{
			get { return _Screenid; }
			set { _Screenid = value;
NotifyPropertyChanged("Screenid");}
		}

		private string _Screenname;
		/// <summary>
		/// 
		/// </summary>
		public string Screenname
		{
			get { return _Screenname; }
			set { _Screenname = value;
NotifyPropertyChanged("Screenname");}
		}

		private string _Imageurl;
		/// <summary>
		/// 
		/// </summary>
		public string Imageurl
		{
			get { return _Imageurl; }
			set { _Imageurl = value;
NotifyPropertyChanged("Imageurl");}
		}

		private int _Parentscreenid;
		/// <summary>
		/// 
		/// </summary>
		public int Parentscreenid
		{
			get { return _Parentscreenid; }
			set { _Parentscreenid = value;
NotifyPropertyChanged("Parentscreenid");}
		}

		private int _Stationid;
		/// <summary>
		/// 
		/// </summary>
		public int Stationid
		{
			get { return _Stationid; }
			set { _Stationid = value;
NotifyPropertyChanged("Stationid");}
		}

		/// <summary>
		/// ScreenLibrary构造函数
		/// </summary>
		public t_ScreenLibrary()
		{

		}

		/// <summary>
		/// ScreenLibrary构造函数
		/// </summary>
		public t_ScreenLibrary(DataRow row)
		{
			// 
			_Screenid = Convert.ToInt32(row["ScreenID"]);
			// 
			_Screenname = row["ScreenName"].ToString().Trim();
			// 
			_Imageurl = row["ImageURL"].ToString().Trim();
			// 
			_Parentscreenid = Convert.ToInt32(row["ParentScreenID"]);
			// 
			_Stationid = Convert.ToInt32(row["StationID"]);
		}

	public void Clone(t_ScreenLibrary obj){
//
Screenid = obj.Screenid;
//
Screenname = obj.Screenname;
//
Imageurl = obj.Imageurl;
//
Parentscreenid = obj.Parentscreenid;
//
Stationid = obj.Stationid;

}

    }
}

