using System;
using System.Data;

namespace WPFMonitor.Model.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class t_ElementProperty: ORBase
    {
       
		private int _ElementID;
		/// <summary>
		/// 
		/// </summary>
		public int ElementID
		{
			get { return _ElementID; }
			set { _ElementID = value;
NotifyPropertyChanged("ElementID");}
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

		private string _PropertyValue;
		/// <summary>
		/// 
		/// </summary>
		public string PropertyValue
		{
			get { return _PropertyValue; }
			set { _PropertyValue = value;
NotifyPropertyChanged("PropertyValue");}
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

		/// <summary>
		/// ElementProperty构造函数
		/// </summary>
		public t_ElementProperty()
		{

		}

		/// <summary>
		/// ElementProperty构造函数
		/// </summary>
		public t_ElementProperty(DataRow row)
		{
			// 
			_ElementID = Convert.ToInt32(row["ElementID"]);
			// 
			_PropertyNo = Convert.ToInt32(row["PropertyNo"]);
			// 
			_PropertyValue = row["PropertyValue"].ToString().Trim();
			// 
			_Caption = row["Caption"].ToString().Trim();
			// 
			_PropertyName = row["PropertyName"].ToString().Trim();
		}

	public void Clone(t_ElementProperty obj){
//
ElementID = obj.ElementID;
//
PropertyNo = obj.PropertyNo;
//
PropertyValue = obj.PropertyValue;
//
Caption = obj.Caption;
//
PropertyName = obj.PropertyName;

}

    }
}

