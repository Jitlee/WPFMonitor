using System;
using System.Data;

namespace WPFMonitor.Model.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class t_Control: ORBase
    {
       
		private int _ControlID;
		/// <summary>
		/// 
		/// </summary>
		public int ControlID
		{
			get { return _ControlID; }
			set { _ControlID = value;
NotifyPropertyChanged("Controlid");}
		}

		private string _ControlName;
		/// <summary>
		/// 
		/// </summary>
		public string ControlName
		{
			get { return _ControlName; }
			set { _ControlName = value;
NotifyPropertyChanged("ControlName");}
		}

		private int _ControlType;
		/// <summary>
		/// 
		/// </summary>
		public int ControlType
		{
			get { return _ControlType; }
			set { _ControlType = value;
NotifyPropertyChanged("ControlType");}
		}

		private string _ImageURL;
		/// <summary>
		/// 
		/// </summary>
		public string ImageURL
		{
			get { return _ImageURL; }
			set { _ImageURL = value;
NotifyPropertyChanged("ImageURL");}
		}

		private string _ControlTypeName;
		/// <summary>
		/// 
		/// </summary>
		public string ControlTypeName
		{
			get { return _ControlTypeName; }
			set { _ControlTypeName = value;
NotifyPropertyChanged("ControlTypeName");}
		}

		private string _ControlCaption;
		/// <summary>
		/// 
		/// </summary>
		public string ControlCaption
		{
			get { return _ControlCaption; }
			set { _ControlCaption = value;
NotifyPropertyChanged("ControlCaption");}
		}

		/// <summary>
		/// Control构造函数
		/// </summary>
		public t_Control()
		{

		}

		/// <summary>
		/// Control构造函数
		/// </summary>
		public t_Control(DataRow row)
		{
			// 
			_ControlID = Convert.ToInt32(row["ControlID"]);
			// 
			_ControlName = row["ControlName"].ToString().Trim();
			// 
			_ControlType = Convert.ToInt32(row["ControlType"]);
			// 
			_ImageURL = row["ImageURL"].ToString().Trim();
			// 
			_ControlTypeName = row["ControlTypeName"].ToString().Trim();
			// 
			_ControlCaption = row["ControlCaption"].ToString().Trim();
		}

	public void Clone(t_Control obj){
//
ControlID = obj.ControlID;
//
ControlName = obj.ControlName;
//
ControlType = obj.ControlType;
//
ImageURL = obj.ImageURL;
//
ControlTypeName = obj.ControlTypeName;
//
ControlCaption = obj.ControlCaption;

}

    }
}

