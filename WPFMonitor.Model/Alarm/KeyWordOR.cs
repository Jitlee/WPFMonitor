using System;
using System.Data;
using WPFMonitor.Model;

namespace WPFMonitor.Model.Alarm
{
    /// <summary>
    /// 
    /// </summary>
    public class KeyWordOR: ORBase
    {
       
		private int _Keywordid;
		/// <summary>
		/// 
		/// </summary>
		public int Keywordid
		{
			get { return _Keywordid; }
			set { _Keywordid = value;
NotifyPropertyChanged("Keywordid");}
		}

		private string _Keyword;
		/// <summary>
		/// 关键字
		/// </summary>
		public string Keyword
		{
			get { return _Keyword; }
			set { _Keyword = value;
NotifyPropertyChanged("Keyword");}
		}

		private string _Keywordname;
		/// <summary>
		/// 关键字名称
		/// </summary>
		public string Keywordname
		{
			get { return _Keywordname; }
			set { _Keywordname = value;
NotifyPropertyChanged("Keywordname");}
		}

		private string _Replace;
		/// <summary>
		/// 替换值
		/// </summary>
		public string Replace
		{
			get { return _Replace; }
			set { _Replace = value;
NotifyPropertyChanged("Replace");}
		}

		private int _Iscustomize;
		/// <summary>
		/// 是否自定义
		/// </summary>
		public int Iscustomize
		{
			get { return _Iscustomize; }
			set { _Iscustomize = value;
NotifyPropertyChanged("Iscustomize");}
		}

		/// <summary>
		/// KeyWord构造函数
		/// </summary>
		public KeyWordOR()
		{

		}

		/// <summary>
		/// KeyWord构造函数
		/// </summary>
		public KeyWordOR(DataRow row)
		{
			// 
			_Keywordid = Convert.ToInt32(row["KeyWordID"]);
			// 关键字
			_Keyword = row["KeyWord"].ToString().Trim();
			// 关键字名称
			_Keywordname = row["KeyWordName"].ToString().Trim();
			// 替换值
			_Replace = row["Replace"].ToString().Trim();
			// 是否自定义
			_Iscustomize = Convert.ToInt32(row["IsCustomize"]);
		}

	public void Clone(KeyWordOR obj){
//
Keywordid = obj.Keywordid;
//关键字
Keyword = obj.Keyword;
//关键字名称
Keywordname = obj.Keywordname;
//替换值
Replace = obj.Replace;
//是否自定义
Iscustomize = obj.Iscustomize;

}

    }
}

