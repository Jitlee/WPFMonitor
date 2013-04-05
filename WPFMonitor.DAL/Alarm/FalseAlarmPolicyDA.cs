using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBUtility;
using WPFMonitor.Model.Alarm;


namespace WPFMonitor.DAL.Alarm
{
    /// <summary>
    /// 
    /// </summary>
    public class FalseAlarmPolicyDA : DALBase
    {

        #region 查询
        public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
        {
            string sql = "select * from t_FalseAlarmPolicy";
            if (!string.IsNullOrEmpty(where))
            {
                sql = string.Format(" {0} where {1}", sql, where);
            }
            DataTable dt = null;
            int returnC = 0; try
            {
                dt = db.ExecuteQuery(sql, pageCrrent, pageSize, out returnC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            pageCount = returnC;
            return dt;
        }

        public FalseAlarmPolicyOR selectARowDate(string m_id)
        {
            string sql = string.Format("select * from t_FalseAlarmPolicy where  Falsealarmid='{0}'", m_id);
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
            FalseAlarmPolicyOR m_Fals = new FalseAlarmPolicyOR(dr);
            return m_Fals;

        }


        public ObservableCollection<FalseAlarmPolicyOR> selectAllDate()
        {
            string sql = @"select fp.*,t.StationName,d.DeviceName,c.ChannelName  from t_FalseAlarmPolicy fp
inner join t_AlarmPolicyManagement am on fp.PolicyID= am.AlarmPolicyManagementID
inner join t_Station t  on am.StationID=t.StationID
inner join t_Device d on am.DeviceID=d.DeviceID
inner join t_Channel c on am.DeviceChannelID=c.ChannelNo and c.DeviceID=d.DeviceID
";

            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<FalseAlarmPolicyOR> _List = new ObservableCollection<FalseAlarmPolicyOR>();
            foreach (DataRow dr in dt.Rows)
            {
                FalseAlarmPolicyOR obj = new FalseAlarmPolicyOR(dr);
                _List.Add(obj);
            }
            return _List;
        }


        #endregion

        #region 插入
        /// <summary>
        /// 插入t_FalseAlarmPolicy
        /// </summary>
        public virtual bool Insert(FalseAlarmPolicyOR falseAlarmPolicy)
        {
            string sql = @"insert into t_FalseAlarmPolicy ( FalsePolicyID, PolicyID, FalseType) 
values (@FalsePolicyID, @PolicyID, @FalseType)";
            SqlParameter[] parameters = new SqlParameter[]
			{
				//new SqlParameter("@FalseAlarmID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "FalseAlarmID", DataRowVersion.Default, falseAlarmPolicy.Falsealarmid),
				new SqlParameter("@FalsePolicyID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "FalsePolicyID", DataRowVersion.Default, falseAlarmPolicy.Falsepolicyid),
				new SqlParameter("@PolicyID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "PolicyID", DataRowVersion.Default, falseAlarmPolicy.Policyid),
				new SqlParameter("@FalseType", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "FalseType", DataRowVersion.Default, falseAlarmPolicy.Falsetype)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 更新t_FalseAlarmPolicy
        /// </summary>
        public virtual bool Update(FalseAlarmPolicyOR falseAlarmPolicy)
        {
            string sql = "update t_FalseAlarmPolicy set  FalsePolicyID = @FalsePolicyID,  PolicyID = @PolicyID,  FalseType = @FalseType where  FalseAlarmID = @FalseAlarmID";
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@FalseAlarmID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "FalseAlarmID", DataRowVersion.Default, falseAlarmPolicy.Falsealarmid),
				new SqlParameter("@FalsePolicyID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "FalsePolicyID", DataRowVersion.Default, falseAlarmPolicy.Falsepolicyid),
				new SqlParameter("@PolicyID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "PolicyID", DataRowVersion.Default, falseAlarmPolicy.Policyid),
				new SqlParameter("@FalseType", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "FalseType", DataRowVersion.Default, falseAlarmPolicy.Falsetype)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region DELETE
        /// <summary>
        /// 删除t_FalseAlarmPolicy
        /// </summary>
        public virtual bool Delete(IList mlist)
        {
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (FalseAlarmPolicyOR obj in mlist)
            {
                string sql = string.Format("delete from t_FalseAlarmPolicy where  FalseAlarmID = '{0}'", obj.Falsealarmid);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
        }
        #endregion
    }
}

