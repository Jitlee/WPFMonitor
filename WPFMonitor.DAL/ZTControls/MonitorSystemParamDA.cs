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
    public class MonitorSystemParamDA : DALBase
    {
        
		#region 查询
public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
{
string sql = "select * from t_MonitorSystemParam";
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

public t_MonitorSystemParam selectARowDate(string m_id)
{string sql = string.Format("select * from t_MonitorSystemParam where  Monitorrefreshtime='{0}'",m_id);
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
t_MonitorSystemParam m_Moni=new t_MonitorSystemParam(dr); 
 return m_Moni;

}


        public List<t_MonitorSystemParam> selectAllDate()
        {
            string sql = "select * from t_MonitorSystemParam";
           
            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            List<t_MonitorSystemParam> _List = new List<t_MonitorSystemParam>();
            foreach (DataRow dr in dt.Rows)
            {
                t_MonitorSystemParam obj = new t_MonitorSystemParam(dr);
                _List.Add(obj);
            }
            return _List;
        }


		#endregion

		#region 插入
		/// <summary>
		/// 插入t_MonitorSystemParam
		/// </summary>
		public virtual bool Insert(t_MonitorSystemParam monitorSystemParam)
		{
			string sql = "insert into t_MonitorSystemParam (MonitorRefreshTime, StartScreenID, AlarmLogTime, ServerIP, ServerPort, Door_Sysid, Door_Com, HaveDoor) values (@MonitorRefreshTime, @StartScreenID, @AlarmLogTime, @ServerIP, @ServerPort, @Door_Sysid, @Door_Com, @HaveDoor)";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@MonitorRefreshTime", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "MonitorRefreshTime", DataRowVersion.Default, monitorSystemParam.MonitorRefreshTime),
				new SqlParameter("@StartScreenID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StartScreenID", DataRowVersion.Default, monitorSystemParam.StartScreenID),
				new SqlParameter("@AlarmLogTime", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "AlarmLogTime", DataRowVersion.Default, monitorSystemParam.AlarmLogTime),
				new SqlParameter("@ServerIP", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "ServerIP", DataRowVersion.Default, monitorSystemParam.ServerIP),
				new SqlParameter("@ServerPort", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ServerPort", DataRowVersion.Default, monitorSystemParam.ServerPort),
				new SqlParameter("@Door_Sysid", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Door_Sysid", DataRowVersion.Default, monitorSystemParam.Door_Sysid),
				new SqlParameter("@Door_Com", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Door_Com", DataRowVersion.Default, monitorSystemParam.Door_Com),
				new SqlParameter("@HaveDoor", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "HaveDoor", DataRowVersion.Default, monitorSystemParam.HaveDoor)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region 修改
		/// <summary>
		/// 更新t_MonitorSystemParam
		/// </summary>
		public virtual bool Update(t_MonitorSystemParam monitorSystemParam)
		{
			string sql = "update t_MonitorSystemParam set  StartScreenID = @StartScreenID,  AlarmLogTime = @AlarmLogTime,  ServerIP = @ServerIP,  ServerPort = @ServerPort,  Door_Sysid = @Door_Sysid,  Door_Com = @Door_Com,  HaveDoor = @HaveDoor where  MonitorRefreshTime = @MonitorRefreshTime";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@MonitorRefreshTime", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "MonitorRefreshTime", DataRowVersion.Default, monitorSystemParam.MonitorRefreshTime),
				new SqlParameter("@StartScreenID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StartScreenID", DataRowVersion.Default, monitorSystemParam.StartScreenID),
				new SqlParameter("@AlarmLogTime", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "AlarmLogTime", DataRowVersion.Default, monitorSystemParam.AlarmLogTime),
				new SqlParameter("@ServerIP", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "ServerIP", DataRowVersion.Default, monitorSystemParam.ServerIP),
				new SqlParameter("@ServerPort", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ServerPort", DataRowVersion.Default, monitorSystemParam.ServerPort),
				new SqlParameter("@Door_Sysid", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Door_Sysid", DataRowVersion.Default, monitorSystemParam.Door_Sysid),
				new SqlParameter("@Door_Com", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Door_Com", DataRowVersion.Default, monitorSystemParam.Door_Com),
				new SqlParameter("@HaveDoor", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "HaveDoor", DataRowVersion.Default, monitorSystemParam.HaveDoor)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region DELETE
		/// <summary>
		/// 删除t_MonitorSystemParam
		/// </summary>
		public virtual bool Delete(IList mlist){
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (t_MonitorSystemParam obj in mlist)
            {
                string sql = string.Format(" delete from t_MonitorSystemParam where  Monitorrefreshtime = '{0}'", obj.MonitorRefreshTime);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
}
		#endregion
    }
}

