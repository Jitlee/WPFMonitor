using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DBUtility;
using WPFMonitor.Model.ZTControls;

namespace WPFMonitor.DAL.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class ControlDA : DALBase
    {
        
		#region 查询

        public List<t_Control> SelectBy(int controlType)
        {
            return db.ExecuteQuery("SELECT * FROM t_Control WHERE ControlType = @ControlType",
                new SqlParameter("@ControlType", controlType))
                .Rows
                .OfType<DataRow>()
                .Select(r => new t_Control(r))
                .ToList();
        }

public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
{
string sql = "select * from t_Control";
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

public t_Control selectARowDate(string m_id)
{string sql = string.Format("select * from t_Control where  Controlid='{0}'",m_id);
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
t_Control m_Cont=new t_Control(dr); 
 return m_Cont;

}


public List<t_Control> selectAllDate()
{
    string sql = "select * from t_Control";

    DataTable dt = null;
    dt = db.ExecuteQuery(sql);
    List<t_Control> _List = new List<t_Control>();
    foreach (DataRow dr in dt.Rows)
    {
        t_Control obj = new t_Control(dr);
        _List.Add(obj);
    }
    return _List;
}


		#endregion

		#region 插入
		/// <summary>
		/// 插入t_Control
		/// </summary>
		public virtual bool Insert(t_Control control)
		{
			string sql = "insert into t_Control (ControlID, ControlName, ControlType, ImageURL, ControlTypeName, ControlCaption) values (@ControlID, @ControlName, @ControlType, @ImageURL, @ControlTypeName, @ControlCaption)";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@ControlID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ControlID", DataRowVersion.Default, control.ControlID),
				new SqlParameter("@ControlName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ControlName", DataRowVersion.Default, control.ControlName),
				new SqlParameter("@ControlType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ControlType", DataRowVersion.Default, control.ControlType),
				new SqlParameter("@ImageURL", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ImageURL", DataRowVersion.Default, control.ImageURL),
				new SqlParameter("@ControlTypeName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ControlTypeName", DataRowVersion.Default, control.ControlTypeName),
				new SqlParameter("@ControlCaption", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ControlCaption", DataRowVersion.Default, control.ControlCaption)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region 修改
		/// <summary>
		/// 更新t_Control
		/// </summary>
		public virtual bool Update(t_Control control)
		{
			string sql = "update t_Control set  ControlName = @ControlName,  ControlType = @ControlType,  ImageURL = @ImageURL,  ControlTypeName = @ControlTypeName,  ControlCaption = @ControlCaption where  ControlID = @ControlID";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@ControlID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ControlID", DataRowVersion.Default, control.ControlID),
				new SqlParameter("@ControlName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ControlName", DataRowVersion.Default, control.ControlName),
				new SqlParameter("@ControlType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ControlType", DataRowVersion.Default, control.ControlType),
				new SqlParameter("@ImageURL", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ImageURL", DataRowVersion.Default, control.ImageURL),
				new SqlParameter("@ControlTypeName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ControlTypeName", DataRowVersion.Default, control.ControlTypeName),
				new SqlParameter("@ControlCaption", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ControlCaption", DataRowVersion.Default, control.ControlCaption)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region DELETE
		/// <summary>
		/// 删除t_Control
		/// </summary>
		public virtual bool Delete(IList mlist){
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (t_Control obj in mlist)
            {
                string sql = string.Format(" delete from t_Control where  Controlid = '{0}'", obj.ControlID);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
}
		#endregion
    }
}

