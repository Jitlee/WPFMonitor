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
    public class DeviceDA : DALBase
    {
        
		#region 查询
public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
{
string sql = "select * from t_Device";
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

public t_Device selectARowDate(string m_id)
{string sql = string.Format("select * from t_Device where  Deviceid='{0}'",m_id);
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
t_Device m_Devi=new t_Device(dr); 
 return m_Devi;

}


        public ObservableCollection<t_Device> selectAllDate()
        {
            string sql = "select * from t_Device";
           
            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<t_Device> _List = new ObservableCollection<t_Device>();
            foreach (DataRow dr in dt.Rows)
            {
                t_Device obj = new t_Device(dr);
                _List.Add(obj);
            }
            return _List;
        }


		#endregion

		#region 插入
		/// <summary>
		/// 插入t_Device
		/// </summary>
		public virtual bool Insert(t_Device device)
		{
			string sql = "insert into t_Device (DeviceID, DeviceName, CommunicateType, CommunicateID, SubAddr, DeviceTypeID, StationID, StationName, IP, UserId, Password, Enable, Port) values (@DeviceID, @DeviceName, @CommunicateType, @CommunicateID, @SubAddr, @DeviceTypeID, @StationID, @StationName, @IP, @UserId, @Password, @Enable, @Port)";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, device.DeviceID),
				new SqlParameter("@DeviceName", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "DeviceName", DataRowVersion.Default, device.DeviceName),
				new SqlParameter("@CommunicateType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "CommunicateType", DataRowVersion.Default, device.Communicatetype),
				new SqlParameter("@CommunicateID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "CommunicateID", DataRowVersion.Default, device.CommunicateID),
				new SqlParameter("@SubAddr", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "SubAddr", DataRowVersion.Default, device.SubAddr),
				new SqlParameter("@DeviceTypeID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceTypeID", DataRowVersion.Default, device.DeviceTypeID),
				new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, device.StationID),
				new SqlParameter("@StationName", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "StationName", DataRowVersion.Default, device.StationName),
				new SqlParameter("@IP", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "IP", DataRowVersion.Default, device.IP),
				new SqlParameter("@UserId", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "UserId", DataRowVersion.Default, device.UserID),
				new SqlParameter("@Password", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Password", DataRowVersion.Default, device.Password),
				new SqlParameter("@Enable", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Enable", DataRowVersion.Default, device.Enable),
				new SqlParameter("@Port", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Port", DataRowVersion.Default, device.Port)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region 修改
		/// <summary>
		/// 更新t_Device
		/// </summary>
		public virtual bool Update(t_Device device)
		{
			string sql = "update t_Device set  DeviceName = @DeviceName,  CommunicateType = @CommunicateType,  CommunicateID = @CommunicateID,  SubAddr = @SubAddr,  DeviceTypeID = @DeviceTypeID,  StationID = @StationID,  StationName = @StationName,  IP = @IP,  UserId = @UserId,  Password = @Password,  Enable = @Enable,  Port = @Port where  DeviceID = @DeviceID";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, device.DeviceID),
				new SqlParameter("@DeviceName", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "DeviceName", DataRowVersion.Default, device.DeviceName),
				new SqlParameter("@CommunicateType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "CommunicateType", DataRowVersion.Default, device.Communicatetype),
				new SqlParameter("@CommunicateID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "CommunicateID", DataRowVersion.Default, device.CommunicateID),
				new SqlParameter("@SubAddr", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "SubAddr", DataRowVersion.Default, device.SubAddr),
				new SqlParameter("@DeviceTypeID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceTypeID", DataRowVersion.Default, device.DeviceTypeID),
				new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, device.StationID),
				new SqlParameter("@StationName", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "StationName", DataRowVersion.Default, device.StationName),
				new SqlParameter("@IP", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "IP", DataRowVersion.Default, device.IP),
				new SqlParameter("@UserId", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "UserId", DataRowVersion.Default, device.UserID),
				new SqlParameter("@Password", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Password", DataRowVersion.Default, device.Password),
				new SqlParameter("@Enable", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Enable", DataRowVersion.Default, device.Enable),
				new SqlParameter("@Port", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Port", DataRowVersion.Default, device.Port)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region DELETE
		/// <summary>
		/// 删除t_Device
		/// </summary>
		public virtual bool Delete(IList mlist){
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (t_Device obj in mlist)
            {
                string sql = string.Format(" delete from t_Device where  Deviceid = '{0}'", obj.DeviceID);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
}
		#endregion
    }
}

