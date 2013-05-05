using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using DBUtility;
using WPFMonitor.Model.ZTControls;

namespace WPFMonitor.DAL.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class ScreenLibraryDA : DALBase
    {
        
		#region 查询
public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
{
string sql = "select * from t_Screen_Library";
if (!string.IsNullOrEmpty(where)){
sql = string.Format(" {0} where {1}", sql, where);
}
 DataTable dt = null;
int returnC = 0;try
 {
     dt = db.ExecuteQuery(sql,pageCrrent, pageSize,  out returnC);
  }
  catch (Exception ex)
  {
    throw ex;
  }
 pageCount = returnC;
  return dt;
}

public t_ScreenLibrary selectARowDate(string m_id)
{string sql = string.Format("select * from t_Screen_Library where  Screenid='{0}'",m_id);
  DataTable dt = null;
try
 {
 dt = db.ExecuteQueryDataSet(sql).Tables[0];
}
  catch (Exception ex)
{
 throw ex;
  }
if (dt == null)
 return null;
if (dt.Rows.Count == 0)
return null;
DataRow dr = dt.Rows[0];
t_ScreenLibrary m_Scre=new t_ScreenLibrary(dr); 
 return m_Scre;

}


        public ObservableCollection<t_ScreenLibrary> selectAllDate()
        {
            string sql = "select * from t_Screen_Library";
           
            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<t_ScreenLibrary> _List = new ObservableCollection<t_ScreenLibrary>();
            foreach (DataRow dr in dt.Rows)
            {
                t_ScreenLibrary obj = new t_ScreenLibrary(dr);
                _List.Add(obj);
            }
            return _List;
        }


		#endregion

		#region 插入
		/// <summary>
		/// 插入t_Screen_Library
		/// </summary>
		public virtual bool Insert(t_ScreenLibrary screenLibrary)
		{
			string sql = "insert into t_Screen_Library (ScreenID, ScreenName, ImageURL, ParentScreenID, StationID) values (@ScreenID, @ScreenName, @ImageURL, @ParentScreenID, @StationID)";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@ScreenID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ScreenID", DataRowVersion.Default, screenLibrary.Screenid),
				new SqlParameter("@ScreenName", SqlDbType.VarChar, 64, ParameterDirection.Input, false, 0, 0, "ScreenName", DataRowVersion.Default, screenLibrary.Screenname),
				new SqlParameter("@ImageURL", SqlDbType.VarChar, 256, ParameterDirection.Input, false, 0, 0, "ImageURL", DataRowVersion.Default, screenLibrary.Imageurl),
				new SqlParameter("@ParentScreenID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ParentScreenID", DataRowVersion.Default, screenLibrary.Parentscreenid),
				new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, screenLibrary.Stationid)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region 修改
		/// <summary>
		/// 更新t_Screen_Library
		/// </summary>
		public virtual bool Update(t_ScreenLibrary screenLibrary)
		{
			string sql = "update t_Screen_Library set  ScreenName = @ScreenName,  ImageURL = @ImageURL,  ParentScreenID = @ParentScreenID,  StationID = @StationID where  ScreenID = @ScreenID";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@ScreenID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ScreenID", DataRowVersion.Default, screenLibrary.Screenid),
				new SqlParameter("@ScreenName", SqlDbType.VarChar, 64, ParameterDirection.Input, false, 0, 0, "ScreenName", DataRowVersion.Default, screenLibrary.Screenname),
				new SqlParameter("@ImageURL", SqlDbType.VarChar, 256, ParameterDirection.Input, false, 0, 0, "ImageURL", DataRowVersion.Default, screenLibrary.Imageurl),
				new SqlParameter("@ParentScreenID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ParentScreenID", DataRowVersion.Default, screenLibrary.Parentscreenid),
				new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, screenLibrary.Stationid)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region DELETE
		/// <summary>
		/// 删除t_Screen_Library
		/// </summary>
		public virtual bool Delete(IList mlist){
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (t_ScreenLibrary obj in mlist)
            {
                string sql = string.Format(" delete from t_Screen_Library where  Screenid = '{0}'", obj.Screenid);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
}
		#endregion
    }
}

