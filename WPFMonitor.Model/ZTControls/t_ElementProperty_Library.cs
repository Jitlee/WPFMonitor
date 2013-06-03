using System;
using System.Data;

namespace WPFMonitor.Model.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class t_ElementProperty_Library: ORBase
    {
       
		private int _Elementid;
		/// <summary>
		/// 
		/// </summary>
		public int Elementid
		{
			get { return _Elementid; }
			set { _Elementid = value;
NotifyPropertyChanged("Elementid");}
		}

		private int _Propertyno;
		/// <summary>
		/// 
		/// </summary>
		public int Propertyno
		{
			get { return _Propertyno; }
			set { _Propertyno = value;
NotifyPropertyChanged("Propertyno");}
		}

		private string _Propertyvalue;
		/// <summary>
		/// 
		/// </summary>
		public string Propertyvalue
		{
			get { return _Propertyvalue; }
			set { _Propertyvalue = value;
NotifyPropertyChanged("Propertyvalue");}
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

		private string _Propertyname;
		/// <summary>
		/// 
		/// </summary>
		public string Propertyname
		{
			get { return _Propertyname; }
			set { _Propertyname = value;
NotifyPropertyChanged("Propertyname");}
		}

		/// <summary>
		/// ElementPropertyLibrary构造函数
		/// </summary>
		public t_ElementProperty_Library()
		{

		}

		/// <summary>
		/// ElementPropertyLibrary构造函数
		/// </summary>
		public t_ElementProperty_Library(DataRow row)
		{
			// 
			_Elementid = Convert.ToInt32(row["ElementID"]);
			// 
			_Propertyno = Convert.ToInt32(row["PropertyNo"]);
			// 
			_Propertyvalue = row["PropertyValue"].ToString().Trim();
			// 
			_Caption = row["Caption"].ToString().Trim();
			// 
			_Propertyname = row["PropertyName"].ToString().Trim();
		}

	public void Clone(t_ElementProperty_Library obj){
//
Elementid = obj.Elementid;
//
Propertyno = obj.Propertyno;
//
Propertyvalue = obj.Propertyvalue;
//
Caption = obj.Caption;
//
Propertyname = obj.Propertyname;

}

    }
}

