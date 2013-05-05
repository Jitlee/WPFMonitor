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
    public class ControlPropertyDA : DALBase
    {
        
		#region 查询

        public List<t_ControlProperty> SelectByControlId(int controlID)
        {
            return db.ExecuteQuery("SELECT * FROM T_CONTROLPROPERTY WHERE ControlID = @ControlID",
                new SqlParameter("@ControlID", controlID))
                .Rows
                .OfType<DataRow>()
                .Select(r => new t_ControlProperty(r)).ToList();
        }

public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
{
string sql = "select * from t_ControlProperty";
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

public t_ControlProperty selectARowDate(string m_id)
{string sql = string.Format("select * from t_ControlProperty where  Controlid='{0}'",m_id);
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
t_ControlProperty m_Cont=new t_ControlProperty(dr); 
 return m_Cont;

}


        public ObservableCollection<t_ControlProperty> selectAllDate()
        {
            string sql = "select * from t_ControlProperty";
           
            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<t_ControlProperty> _List = new ObservableCollection<t_ControlProperty>();
            foreach (DataRow dr in dt.Rows)
            {
                t_ControlProperty obj = new t_ControlProperty(dr);
                _List.Add(obj);
            }
            return _List;
        }


		#endregion

		#region 插入
		/// <summary>
		/// 插入t_ControlProperty
		/// </summary>
		public virtual bool Insert(t_ControlProperty controlProperty)
		{
			string sql = "insert into t_ControlProperty (ControlID, PropertyNo, PropertyName, DefaultValue, Caption) values (@ControlID, @PropertyNo, @PropertyName, @DefaultValue, @Caption)";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@ControlID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ControlID", DataRowVersion.Default, controlProperty.ControlID),
				new SqlParameter("@PropertyNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "PropertyNo", DataRowVersion.Default, controlProperty.PropertyNo),
				new SqlParameter("@PropertyName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "PropertyName", DataRowVersion.Default, controlProperty.PropertyName),
				new SqlParameter("@DefaultValue", SqlDbType.VarChar, 128, ParameterDirection.Input, false, 0, 0, "DefaultValue", DataRowVersion.Default, controlProperty.DefaultValue),
				new SqlParameter("@Caption", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Caption", DataRowVersion.Default, controlProperty.Caption)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region 修改
		/// <summary>
		/// 更新t_ControlProperty
		/// </summary>
		public virtual bool Update(t_ControlProperty controlProperty)
		{
			string sql = "update t_ControlProperty set  PropertyNo = @PropertyNo,  PropertyName = @PropertyName,  DefaultValue = @DefaultValue,  Caption = @Caption where  ControlID = @ControlID";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@ControlID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ControlID", DataRowVersion.Default, controlProperty.ControlID),
				new SqlParameter("@PropertyNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "PropertyNo", DataRowVersion.Default, controlProperty.PropertyNo),
				new SqlParameter("@PropertyName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "PropertyName", DataRowVersion.Default, controlProperty.PropertyName),
				new SqlParameter("@DefaultValue", SqlDbType.VarChar, 128, ParameterDirection.Input, false, 0, 0, "DefaultValue", DataRowVersion.Default, controlProperty.DefaultValue),
				new SqlParameter("@Caption", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Caption", DataRowVersion.Default, controlProperty.Caption)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region DELETE
		/// <summary>
		/// 删除t_ControlProperty
		/// </summary>
		public virtual bool Delete(IList mlist){
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (t_ControlProperty obj in mlist)
            {
                string sql = string.Format(" delete from t_ControlProperty where  Controlid = '{0}'", obj.ControlID);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
}
		#endregion
    }
}

