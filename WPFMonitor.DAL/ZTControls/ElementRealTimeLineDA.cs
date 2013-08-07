using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using DBUtility;
using WPFMonitor.Model.ZTControls;
using System.Linq;

namespace WPFMonitor.DAL.ZTControls
{
	/// <summary>
	/// 
	/// </summary>
	public class ElementRealTimeLineDA : DALBase
	{

		#region 查询

		public List<t_Element_RealTimeLine> SelectBy(int elementId)
		{
			return db.ExecuteQuery("select * from t_Element_RealTimeLine where ElementID = @ElementID",
				new[]
                {
                    new SqlParameter("@ElementID", elementId),
                })
				.Rows
				.OfType<DataRow>()
				.Select(r => new t_Element_RealTimeLine(r))
				.ToList();
		}

		//public List<t_Element_RealTimeLine> selectAllDate(int ElementID)
		//{
		//    string sql = string.Format("select * from t_Element_RealTimeLine  where ElementID={0}", ElementID);

		//    DataTable dt = null;
		//    try
		//    {
		//        dt = db.ExecuteQuery(sql);
		//    }
		//    catch (Exception ex)
		//    {
		//        throw ex;
		//    }
		//    List<t_Element_RealTimeLine> _List = new List<t_Element_RealTimeLine>();
		//    foreach (DataRow dr in dt.Rows)
		//    {
		//        t_Element_RealTimeLine obj = new t_Element_RealTimeLine(dr);
		//        _List.Add(obj);
		//    }
		//    return _List;
		//}


		#endregion

		 

		 
	}
}

