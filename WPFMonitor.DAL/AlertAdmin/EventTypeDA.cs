using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DBUtility;
using System.Collections;
using System.Collections.ObjectModel;
using WPFMonitor.Model.AlertAdmin;


namespace WPFMonitor.DAL.AlertAdmin
{
    /// <summary>
    /// 
    /// </summary>
    public class EventTypeDA : DALBase
    {

        #region 查询
        public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
        {
            string sql = "select * from t_EventType";
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

        public EventTypeOR selectARowDate(string m_id)
        {
            string sql = string.Format("select * from t_EventType where  Eventid='{0}'", m_id);
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
            EventTypeOR m_Even = new EventTypeOR(dr);
            return m_Even;

        }


        public ObservableCollection<EventTypeOR> selectAllDate()
        {
            string sql = "select * from t_EventType";

            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<EventTypeOR> _List = new ObservableCollection<EventTypeOR>();
            foreach (DataRow dr in dt.Rows)
            {
                EventTypeOR obj = new EventTypeOR(dr);
                _List.Add(obj);
            }
            return _List;
        }


        #endregion

        #region 插入
        /// <summary>
        /// 插入t_EventType
        /// </summary>
        public virtual bool Insert(EventTypeOR eventType)
        {
            string sql = @"insert into t_EventType ( EventName, AlarmLevel, AlarmTarget, AlarmWay, IsEnableFrequency, 
AlarmAudioFile, DisAlarmAudioFile, SmsMsg, DisarmID) values (@EventName, @AlarmLevel, @AlarmTarget,
@AlarmWay, @IsEnableFrequency, @AlarmAudioFile, @DisAlarmAudioFile, @SmsMsg, @DisarmID)";
            SqlParameter[] parameters = new SqlParameter[]
			{
				//new SqlParameter("@EventID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "EventID", DataRowVersion.Default, eventType.Eventid),
				new SqlParameter("@EventName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "EventName", DataRowVersion.Default, eventType.Eventname),
				new SqlParameter("@AlarmLevel", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "AlarmLevel", DataRowVersion.Default, eventType.Alarmlevel),
				new SqlParameter("@AlarmTarget", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "AlarmTarget", DataRowVersion.Default, eventType.Alarmtarget),
				new SqlParameter("@AlarmWay", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "AlarmWay", DataRowVersion.Default, eventType.Alarmway),
				new SqlParameter("@IsEnableFrequency", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "IsEnableFrequency", DataRowVersion.Default, eventType.Isenablefrequency),
				new SqlParameter("@AlarmAudioFile", SqlDbType.VarChar, 1024, ParameterDirection.Input, false, 0, 0, "AlarmAudioFile", DataRowVersion.Default, eventType.Alarmaudiofile),
				new SqlParameter("@DisAlarmAudioFile", SqlDbType.VarChar, 1024, ParameterDirection.Input, false, 0, 0, "DisAlarmAudioFile", DataRowVersion.Default, eventType.Disalarmaudiofile),
				new SqlParameter("@SmsMsg", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "SmsMsg", DataRowVersion.Default, eventType.Smsmsg),
				new SqlParameter("@DisarmID", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "DisarmID", DataRowVersion.Default, eventType.Disarmid)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 更新t_EventType
        /// </summary>
        public virtual bool Update(EventTypeOR eventType)
        {
            string sql = "update t_EventType set  EventName = @EventName,  AlarmLevel = @AlarmLevel,  AlarmTarget = @AlarmTarget,  AlarmWay = @AlarmWay,  IsEnableFrequency = @IsEnableFrequency,  AlarmAudioFile = @AlarmAudioFile,  DisAlarmAudioFile = @DisAlarmAudioFile,  SmsMsg = @SmsMsg,  DisarmID = @DisarmID where  EventID = @EventID";
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@EventID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "EventID", DataRowVersion.Default, eventType.Eventid),
				new SqlParameter("@EventName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "EventName", DataRowVersion.Default, eventType.Eventname),
				new SqlParameter("@AlarmLevel", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "AlarmLevel", DataRowVersion.Default, eventType.Alarmlevel),
				new SqlParameter("@AlarmTarget", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "AlarmTarget", DataRowVersion.Default, eventType.Alarmtarget),
				new SqlParameter("@AlarmWay", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "AlarmWay", DataRowVersion.Default, eventType.Alarmway),
				new SqlParameter("@IsEnableFrequency", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "IsEnableFrequency", DataRowVersion.Default, eventType.Isenablefrequency),
				new SqlParameter("@AlarmAudioFile", SqlDbType.VarChar, 1024, ParameterDirection.Input, false, 0, 0, "AlarmAudioFile", DataRowVersion.Default, eventType.Alarmaudiofile),
				new SqlParameter("@DisAlarmAudioFile", SqlDbType.VarChar, 1024, ParameterDirection.Input, false, 0, 0, "DisAlarmAudioFile", DataRowVersion.Default, eventType.Disalarmaudiofile),
				new SqlParameter("@SmsMsg", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "SmsMsg", DataRowVersion.Default, eventType.Smsmsg),
				new SqlParameter("@DisarmID", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "DisarmID", DataRowVersion.Default, eventType.Disarmid)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region DELETE
        /// <summary>
        /// 删除t_EventType
        /// </summary>
        public virtual bool Delete(IList mlist)
        {
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (EventTypeOR obj in mlist)
            {
                string sql = string.Format(" delete from t_EventType where  Eventid = '{0}'", obj.Eventid);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
        }
        #endregion
    }
}

