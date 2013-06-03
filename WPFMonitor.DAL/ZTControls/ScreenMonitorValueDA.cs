using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DBUtility;
using System.Collections;
using System.Collections.ObjectModel;
using WPFMonitor.Model.ZTControls;


namespace WPFMonitor.DAL.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class ScreenMonitorValueDA : DALBase
    {
        
		#region 查询
public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
{
string sql = "select * from V_ScreenMonitorValue";
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

public V_ScreenMonitorValue selectARowDate(string m_id)
{string sql = string.Format("select * from V_ScreenMonitorValue where  Id='{0}'",m_id);
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
V_ScreenMonitorValue m_Scre=new V_ScreenMonitorValue(dr); 
 return m_Scre;

}


        public List<V_ScreenMonitorValue> selectAllDate()
        {
            string sql = "select * from V_ScreenMonitorValue";
           
            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            List<V_ScreenMonitorValue> _List = new List<V_ScreenMonitorValue>();
            foreach (DataRow dr in dt.Rows)
            {
                V_ScreenMonitorValue obj = new V_ScreenMonitorValue(dr);
                _List.Add(obj);
            }
            return _List;
        }


		#endregion

		#region 插入
		/// <summary>
		/// 插入V_ScreenMonitorValue
		/// </summary>
		public virtual bool Insert(V_ScreenMonitorValue screenMonitorValue)
		{
			string sql = "insert into V_ScreenMonitorValue (id, ElementID, ScreenID, DeviceID, ChannelNo, ComputeStr, StationID, ChanenlSubNo, MonitorValue, flag) values (@id, @ElementID, @ScreenID, @DeviceID, @ChannelNo, @ComputeStr, @StationID, @ChanenlSubNo, @MonitorValue, @flag)";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@id", SqlDbType.VarChar, 16, ParameterDirection.Input, false, 0, 0, "id", DataRowVersion.Default, screenMonitorValue.ID),
				new SqlParameter("@ElementID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ElementID", DataRowVersion.Default, screenMonitorValue.ElementID),
				new SqlParameter("@ScreenID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ScreenID", DataRowVersion.Default, screenMonitorValue.ScreenID),
				new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, screenMonitorValue.DeviceID),
				new SqlParameter("@ChannelNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChannelNo", DataRowVersion.Default, screenMonitorValue.ChannelNo),
				new SqlParameter("@ComputeStr", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "ComputeStr", DataRowVersion.Default, screenMonitorValue.ComputeStr),
				new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, screenMonitorValue.StationID),
				new SqlParameter("@ChanenlSubNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChanenlSubNo", DataRowVersion.Default, screenMonitorValue.ChanenlSubNo),
				new SqlParameter("@MonitorValue", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "MonitorValue", DataRowVersion.Default, screenMonitorValue.MonitorValue),
				new SqlParameter("@flag", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "flag", DataRowVersion.Default, screenMonitorValue.Flag)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region 修改
		/// <summary>
		/// 更新V_ScreenMonitorValue
		/// </summary>
		public virtual bool Update(V_ScreenMonitorValue screenMonitorValue)
		{
			string sql = "update V_ScreenMonitorValue set  id = @id,  ElementID = @ElementID,  ScreenID = @ScreenID,  DeviceID = @DeviceID,  ChannelNo = @ChannelNo,  ComputeStr = @ComputeStr,  StationID = @StationID,  ChanenlSubNo = @ChanenlSubNo,  MonitorValue = @MonitorValue,  flag = @flag where  ID = @ID";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@id", SqlDbType.VarChar, 16, ParameterDirection.Input, false, 0, 0, "id", DataRowVersion.Default, screenMonitorValue.ID),
				new SqlParameter("@ElementID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ElementID", DataRowVersion.Default, screenMonitorValue.ElementID),
				new SqlParameter("@ScreenID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ScreenID", DataRowVersion.Default, screenMonitorValue.ScreenID),
				new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, screenMonitorValue.DeviceID),
				new SqlParameter("@ChannelNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChannelNo", DataRowVersion.Default, screenMonitorValue.ChannelNo),
				new SqlParameter("@ComputeStr", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "ComputeStr", DataRowVersion.Default, screenMonitorValue.ComputeStr),
				new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, screenMonitorValue.StationID),
				new SqlParameter("@ChanenlSubNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChanenlSubNo", DataRowVersion.Default, screenMonitorValue.ChanenlSubNo),
				new SqlParameter("@MonitorValue", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "MonitorValue", DataRowVersion.Default, screenMonitorValue.MonitorValue),
				new SqlParameter("@flag", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "flag", DataRowVersion.Default, screenMonitorValue.Flag)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region DELETE
		/// <summary>
		/// 删除V_ScreenMonitorValue
		/// </summary>
		public virtual bool Delete(IList mlist){
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (V_ScreenMonitorValue obj in mlist)
            {
                string sql = string.Format(" delete from V_ScreenMonitorValue where  Id = '{0}'", obj.ID);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
}
		#endregion
    }
}

