using System;
using System.Data;

namespace WPFMonitor.Model.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class t_ControlProperty: ORBase
    {
       
		private int _ControlID;
		/// <summary>
		/// 
		/// </summary>
		public int ControlID
		{
			get { return _ControlID; }
			set { _ControlID = value;
NotifyPropertyChanged("ControlID");}
		}

		private int _PropertyNo;
		/// <summary>
		/// 
		/// </summary>
		public int PropertyNo
		{
			get { return _PropertyNo; }
			set { _PropertyNo = value;
NotifyPropertyChanged("PropertyNo");}
		}

		private string _PropertyName;
		/// <summary>
		/// 
		/// </summary>
		public string PropertyName
		{
			get { return _PropertyName; }
			set { _PropertyName = value;
NotifyPropertyChanged("PropertyName");}
		}

		private string _DefaultValue;
		/// <summary>
		/// 
		/// </summary>
		public string DefaultValue
		{
			get { return _DefaultValue; }
			set { _DefaultValue = value;
NotifyPropertyChanged("DefaultValue");}
		}

		private string _Caption;
		/// <summary>
		/// 
		/// </summary>
		public string Caption
		{
			get { return _Caption; }
			set { _Caption = value;
NotifyPropertyChanged("Caption");}
		}

		/// <summary>
		/// ControlProperty构造函数
		/// </summary>
		public t_ControlProperty()
		{

		}

		/// <summary>
		/// ControlProperty构造函数
		/// </summary>
		public t_ControlProperty(DataRow row)
		{
			// 
			_ControlID = Convert.ToInt32(row["ControlID"]);
			// 
			_PropertyNo = Convert.ToInt32(row["PropertyNo"]);
			// 
			_PropertyName = row["PropertyName"].ToString().Trim();
			// 
			_DefaultValue = row["DefaultValue"].ToString().Trim();
			// 
			_Caption = row["Caption"].ToString().Trim();
		}

	public void Clone(t_ControlProperty obj){
//
ControlID = obj.ControlID;
//
PropertyNo = obj.PropertyNo;
//
PropertyName = obj.PropertyName;
//
DefaultValue = obj.DefaultValue;
//
Caption = obj.Caption;

}

    }
}

