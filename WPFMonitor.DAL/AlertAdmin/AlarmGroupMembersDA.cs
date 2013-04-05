using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBUtility;
using WPFMonitor.Model.AlertAdmin;


namespace WPFMonitor.DAL.AlertAdmin
{
    /// <summary>
    /// 
    /// </summary>
    public class AlarmGroupMembersDA : DALBase
    {
        
		#region 查询
public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
{
string sql = "select * from t_AlarmGroupMembers";
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

public AlarmGroupMembersOR selectARowDate(string m_id)
{string sql = string.Format("select * from t_AlarmGroupMembers where  Id='{0}'",m_id);
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
AlarmGroupMembersOR m_Alar=new AlarmGroupMembersOR(dr); 
 return m_Alar;

}


        public ObservableCollection<AlarmGroupMembersOR> selectAllDate()
        {
            string sql = @"select tm.*,s.StationName,ag.GroupName,als.LevelName,ts.FrequencyName from t_AlarmGroupMembers tm
inner join t_Station s on s.stationid=tm.stationid
inner join t_AlarmGroups ag on ag.AlarmGroupsID= tm.alarmgroupsid
inner join t_AlarmLevelSet als on als.id= tm.processlevel
inner join t_Scheduling ts on ts.Frequency= tm.scheduling ";
           
            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<AlarmGroupMembersOR> _List = new ObservableCollection<AlarmGroupMembersOR>();
            foreach (DataRow dr in dt.Rows)
            {
                AlarmGroupMembersOR obj = new AlarmGroupMembersOR(dr);
                _List.Add(obj);
            }
            return _List;
        }


		#endregion

		#region 插入
		/// <summary>
		/// 插入t_AlarmGroupMembers
		/// </summary>
		public virtual bool Insert(AlarmGroupMembersOR alarmGroupMembers)
		{
			string sql = @"insert into t_AlarmGroupMembers(StationID, AlarmGroupsID, Name, MobileNo, TelNo, 
Email, Scheduling, ProcessLevel) values (@StationID, @AlarmGroupsID, @Name, @MobileNo, @TelNo, @Email, @Scheduling, @ProcessLevel)";
			SqlParameter [] parameters = new SqlParameter[]
			{
				//new SqlParameter("@ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, alarmGroupMembers.Id),
				new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, alarmGroupMembers.Stationid),
				new SqlParameter("@AlarmGroupsID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "AlarmGroupsID", DataRowVersion.Default, alarmGroupMembers.Alarmgroupsid),
				new SqlParameter("@Name", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Default, alarmGroupMembers.Name),
				new SqlParameter("@MobileNo", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "MobileNo", DataRowVersion.Default, alarmGroupMembers.Mobileno),
				new SqlParameter("@TelNo", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "TelNo", DataRowVersion.Default, alarmGroupMembers.Telno),
				new SqlParameter("@Email", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Email", DataRowVersion.Default, alarmGroupMembers.Email),
				new SqlParameter("@Scheduling", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Scheduling", DataRowVersion.Default, alarmGroupMembers.Scheduling),
				new SqlParameter("@ProcessLevel", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ProcessLevel", DataRowVersion.Default, alarmGroupMembers.Processlevel)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region 修改
		/// <summary>
		/// 更新t_AlarmGroupMembers
		/// </summary>
		public virtual bool Update(AlarmGroupMembersOR alarmGroupMembers)
		{
			string sql = "update t_AlarmGroupMembers set  StationID = @StationID,  AlarmGroupsID = @AlarmGroupsID,  Name = @Name,  MobileNo = @MobileNo,  TelNo = @TelNo,  Email = @Email,  Scheduling = @Scheduling,  ProcessLevel = @ProcessLevel where  ID = @ID";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, alarmGroupMembers.Id),
				new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, alarmGroupMembers.Stationid),
				new SqlParameter("@AlarmGroupsID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "AlarmGroupsID", DataRowVersion.Default, alarmGroupMembers.Alarmgroupsid),
				new SqlParameter("@Name", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Default, alarmGroupMembers.Name),
				new SqlParameter("@MobileNo", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "MobileNo", DataRowVersion.Default, alarmGroupMembers.Mobileno),
				new SqlParameter("@TelNo", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "TelNo", DataRowVersion.Default, alarmGroupMembers.Telno),
				new SqlParameter("@Email", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Email", DataRowVersion.Default, alarmGroupMembers.Email),
				new SqlParameter("@Scheduling", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Scheduling", DataRowVersion.Default, alarmGroupMembers.Scheduling),
				new SqlParameter("@ProcessLevel", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ProcessLevel", DataRowVersion.Default, alarmGroupMembers.Processlevel)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region DELETE
		/// <summary>
		/// 删除t_AlarmGroupMembers
		/// </summary>
		public virtual bool Delete(IList mlist){
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (AlarmGroupMembersOR obj in mlist)
            {
                string sql = string.Format(" delete from t_AlarmGroupMembers where  Id = '{0}'", obj.Id);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
}
		#endregion
    }
}

