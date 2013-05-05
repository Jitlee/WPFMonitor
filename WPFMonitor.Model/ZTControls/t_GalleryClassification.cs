using System;
using System.Data;

namespace WPFMonitor.Model.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class t_GalleryClassification: ORBase
    {
       
		private int _Id;
		/// <summary>
		/// 分类Id
		/// </summary>
		public int Id
		{
			get { return _Id; }
			set { _Id = value;
NotifyPropertyChanged("Id");}
		}

		private string _Name;
		/// <summary>
		/// 分类名称
		/// </summary>
		public string Name
		{
			get { return _Name; }
			set { _Name = value;
NotifyPropertyChanged("Name");}
		}

		private string _Description;
		/// <summary>
		/// 分类说明
		/// </summary>
		public string Description
		{
			get { return _Description; }
			set { _Description = value;
NotifyPropertyChanged("Description");}
		}

		private int _Sort;
		/// <summary>
		/// 排序字段
		/// </summary>
		public int Sort
		{
			get { return _Sort; }
			set { _Sort = value;
NotifyPropertyChanged("Sort");}
		}

		/// <summary>
		/// GalleryClassification构造函数
		/// </summary>
		public t_GalleryClassification()
		{

		}

		/// <summary>
		/// GalleryClassification构造函数
		/// </summary>
		public t_GalleryClassification(DataRow row)
		{
			// 分类Id
			_Id = Convert.ToInt32(row["Id"]);
			// 分类名称
			_Name = row["Name"].ToString().Trim();
			// 分类说明
			_Description = row["Description"].ToString().Trim();
			// 排序字段
			_Sort = Convert.ToInt32(row["Sort"]);
		}

	public void Clone(t_GalleryClassification obj){
//分类Id
Id = obj.Id;
//分类名称
Name = obj.Name;
//分类说明
Description = obj.Description;
//排序字段
Sort = obj.Sort;

}

    }
}

