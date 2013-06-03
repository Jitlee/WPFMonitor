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
    public class ElementPropertyLibraryDA : DALBase
    {
        
		#region 查询
public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
{
string sql = "select * from t_ElementProperty_Library";
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

public t_ElementProperty_Library selectARowDate(string m_id)
{string sql = string.Format("select * from t_ElementProperty_Library where  Elementid='{0}'",m_id);
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
t_ElementProperty_Library m_Elem=new t_ElementProperty_Library(dr); 
 return m_Elem;

}


        public List<t_ElementProperty_Library> selectAllDate()
        {
            string sql = "select * from t_ElementProperty_Library";
           
            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            List<t_ElementProperty_Library> _List = new List<t_ElementProperty_Library>();
            foreach (DataRow dr in dt.Rows)
            {
                t_ElementProperty_Library obj = new t_ElementProperty_Library(dr);
                _List.Add(obj);
            }
            return _List;
        }


		#endregion

		#region 插入
		/// <summary>
		/// 插入t_ElementProperty_Library
		/// </summary>
		public virtual bool Insert(t_ElementProperty_Library elementPropertyLibrary)
		{
			string sql = "insert into t_ElementProperty_Library (ElementID, PropertyNo, PropertyValue, Caption, PropertyName) values (@ElementID, @PropertyNo, @PropertyValue, @Caption, @PropertyName)";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@ElementID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ElementID", DataRowVersion.Default, elementPropertyLibrary.Elementid),
				new SqlParameter("@PropertyNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "PropertyNo", DataRowVersion.Default, elementPropertyLibrary.Propertyno),
				new SqlParameter("@PropertyValue", SqlDbType.VarChar, 4096, ParameterDirection.Input, false, 0, 0, "PropertyValue", DataRowVersion.Default, elementPropertyLibrary.Propertyvalue),
				new SqlParameter("@Caption", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Caption", DataRowVersion.Default, elementPropertyLibrary.Caption),
				new SqlParameter("@PropertyName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "PropertyName", DataRowVersion.Default, elementPropertyLibrary.Propertyname)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region 修改
		/// <summary>
		/// 更新t_ElementProperty_Library
		/// </summary>
		public virtual bool Update(t_ElementProperty_Library elementPropertyLibrary)
		{
			string sql = "update t_ElementProperty_Library set  PropertyNo = @PropertyNo,  PropertyValue = @PropertyValue,  Caption = @Caption,  PropertyName = @PropertyName where  ElementID = @ElementID";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@ElementID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ElementID", DataRowVersion.Default, elementPropertyLibrary.Elementid),
				new SqlParameter("@PropertyNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "PropertyNo", DataRowVersion.Default, elementPropertyLibrary.Propertyno),
				new SqlParameter("@PropertyValue", SqlDbType.VarChar, 4096, ParameterDirection.Input, false, 0, 0, "PropertyValue", DataRowVersion.Default, elementPropertyLibrary.Propertyvalue),
				new SqlParameter("@Caption", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Caption", DataRowVersion.Default, elementPropertyLibrary.Caption),
				new SqlParameter("@PropertyName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "PropertyName", DataRowVersion.Default, elementPropertyLibrary.Propertyname)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region DELETE
		/// <summary>
		/// 删除t_ElementProperty_Library
		/// </summary>
		public virtual bool Delete(IList mlist){
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (t_ElementProperty_Library obj in mlist)
            {
                string sql = string.Format(" delete from t_ElementProperty_Library where  Elementid = '{0}'", obj.Elementid);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
}
		#endregion
    }
}

