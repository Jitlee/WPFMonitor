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
    public class SchedulingDA : DALBase
    {
        
		#region 查询
public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
{
string sql = "select * from t_Scheduling";
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

public SchedulingOR selectARowDate(string m_id)
{string sql = string.Format("select * from t_Scheduling where  Frequency='{0}'",m_id);
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
SchedulingOR m_Sche=new SchedulingOR(dr); 
 return m_Sche;

}


        public ObservableCollection<SchedulingOR> selectAllDate()
        {
            string sql = "select * from t_Scheduling";
           
            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<SchedulingOR> _List = new ObservableCollection<SchedulingOR>();
            foreach (DataRow dr in dt.Rows)
            {
                SchedulingOR obj = new SchedulingOR(dr);
                _List.Add(obj);
            }
            return _List;
        }


		#endregion

		#region 插入
		/// <summary>
		/// 插入t_Scheduling
		/// </summary>
		public virtual bool Insert(SchedulingOR scheduling)
		{
			string sql = "insert into t_Scheduling ( FrequencyName, StartTime, EndTime) values (@FrequencyName, @StartTime, @EndTime)";
			SqlParameter [] parameters = new SqlParameter[]
			{
				//new SqlParameter("@Frequency", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Frequency", DataRowVersion.Default, scheduling.Frequency),
				new SqlParameter("@FrequencyName", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "FrequencyName", DataRowVersion.Default, scheduling.Frequencyname),
				new SqlParameter("@StartTime", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "StartTime", DataRowVersion.Default, scheduling.Starttime),
				new SqlParameter("@EndTime", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "EndTime", DataRowVersion.Default, scheduling.Endtime)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region 修改
		/// <summary>
		/// 更新t_Scheduling
		/// </summary>
		public virtual bool Update(SchedulingOR scheduling)
		{
			string sql = "update t_Scheduling set  FrequencyName = @FrequencyName,  StartTime = @StartTime,  EndTime = @EndTime where  Frequency = @Frequency";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@Frequency", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Frequency", DataRowVersion.Default, scheduling.Frequency),
				new SqlParameter("@FrequencyName", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "FrequencyName", DataRowVersion.Default, scheduling.Frequencyname),
				new SqlParameter("@StartTime", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "StartTime", DataRowVersion.Default, scheduling.Starttime),
				new SqlParameter("@EndTime", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "EndTime", DataRowVersion.Default, scheduling.Endtime)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region DELETE
		/// <summary>
		/// 删除t_Scheduling
		/// </summary>
		public virtual bool Delete(IList mlist){
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (SchedulingOR obj in mlist)
            {
                string sql = string.Format(" delete from t_Scheduling where  Frequency = '{0}'", obj.Frequency);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
}
		#endregion
    }
}

