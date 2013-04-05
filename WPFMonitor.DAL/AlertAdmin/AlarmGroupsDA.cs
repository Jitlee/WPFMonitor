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
    public class AlarmGroupsDA : DALBase
    {

        #region 查询
        public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
        {
            string sql = "select * from t_AlarmGroups";
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

        public AlarmGroupsOR selectARowDate(string m_id)
        {
            string sql = string.Format("select * from t_AlarmGroups where  Alarmgroupsid='{0}'", m_id);
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
            AlarmGroupsOR m_Alar = new AlarmGroupsOR(dr);
            return m_Alar;

        }

        public ObservableCollection<AlarmGroupsOR> SelectAllDateByStationID(int StationID)
        {
            string sql = string.Format(@"select ag.*,s.StationName from t_AlarmGroups ag
inner join t_Station s on s.StationID=ag.StationID
where ag.stationid={0}", StationID);

            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<AlarmGroupsOR> _List = new ObservableCollection<AlarmGroupsOR>();
            foreach (DataRow dr in dt.Rows)
            {
                AlarmGroupsOR obj = new AlarmGroupsOR(dr);
                _List.Add(obj);
            }
            return _List;
        }

        public ObservableCollection<AlarmGroupsOR> selectAllDate()
        {
            string sql = @"select ag.*,s.StationName from t_AlarmGroups ag
inner join t_Station s on s.StationID=ag.StationID";

            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<AlarmGroupsOR> _List = new ObservableCollection<AlarmGroupsOR>();
            foreach (DataRow dr in dt.Rows)
            {
                AlarmGroupsOR obj = new AlarmGroupsOR(dr);
                _List.Add(obj);
            }
            return _List;
        }


        #endregion

        #region 插入
        /// <summary>
        /// 插入t_AlarmGroups
        /// </summary>
        public virtual bool Insert(AlarmGroupsOR alarmGroups)
        {
            string sql = "insert into t_AlarmGroups (StationID, GroupName) values (@StationID, @GroupName)";
            SqlParameter[] parameters = new SqlParameter[]
			{
				//new SqlParameter("@AlarmGroupsID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "AlarmGroupsID", DataRowVersion.Default, alarmGroups.Alarmgroupsid),
				new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, alarmGroups.Stationid),
				new SqlParameter("@GroupName", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "GroupName", DataRowVersion.Default, alarmGroups.Groupname)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 更新t_AlarmGroups
        /// </summary>
        public virtual bool Update(AlarmGroupsOR alarmGroups)
        {
            string sql = "update t_AlarmGroups set  StationID = @StationID,  GroupName = @GroupName where  AlarmGroupsID = @AlarmGroupsID";
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@AlarmGroupsID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "AlarmGroupsID", DataRowVersion.Default, alarmGroups.Alarmgroupsid),
				new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, alarmGroups.Stationid),
				new SqlParameter("@GroupName", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "GroupName", DataRowVersion.Default, alarmGroups.Groupname)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region DELETE
        /// <summary>
        /// 删除t_AlarmGroups
        /// </summary>
        public virtual bool Delete(IList mlist)
        {
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (AlarmGroupsOR obj in mlist)
            {
                string sql = string.Format(" delete from t_AlarmGroups where  Alarmgroupsid = '{0}'", obj.Alarmgroupsid);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
        }
        #endregion
    }
}

