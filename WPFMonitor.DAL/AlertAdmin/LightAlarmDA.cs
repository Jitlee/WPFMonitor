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
    public class LightAlarmDA : DALBase
    {

        #region 查询
        public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
        {
            string sql = "select * from t_LightAlarm";
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

        public LightAlarmOR selectARowDate(string m_id)
        {
            string sql = string.Format("select * from t_LightAlarm where  Lightid='{0}'", m_id);
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
            LightAlarmOR m_Ligh = new LightAlarmOR(dr);
            return m_Ligh;

        }


        public ObservableCollection<LightAlarmOR> selectAllDate()
        {
            string sql = "select * from t_LightAlarm";

            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<LightAlarmOR> _List = new ObservableCollection<LightAlarmOR>();
            foreach (DataRow dr in dt.Rows)
            {
                LightAlarmOR obj = new LightAlarmOR(dr);
                _List.Add(obj);
            }
            return _List;
        }


        #endregion

        #region 插入
        /// <summary>
        /// 插入t_LightAlarm
        /// </summary>
        public virtual bool Insert(LightAlarmOR lightAlarm)
        {
            string sql = "insert into t_LightAlarm (LightID, LightName, DeviceID, ChannelNO) values (@LightID, @LightName, @DeviceID, @ChannelNO)";
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@LightID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LightID", DataRowVersion.Default, lightAlarm.Lightid),
				new SqlParameter("@LightName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "LightName", DataRowVersion.Default, lightAlarm.Lightname),
				new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, lightAlarm.Deviceid),
				new SqlParameter("@ChannelNO", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChannelNO", DataRowVersion.Default, lightAlarm.Channelno)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 更新t_LightAlarm
        /// </summary>
        public virtual bool Update(LightAlarmOR lightAlarm)
        {
            string sql = "update t_LightAlarm set  LightName = @LightName,  DeviceID = @DeviceID,  ChannelNO = @ChannelNO where  LightID = @LightID";
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@LightID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LightID", DataRowVersion.Default, lightAlarm.Lightid),
				new SqlParameter("@LightName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "LightName", DataRowVersion.Default, lightAlarm.Lightname),
				new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, lightAlarm.Deviceid),
				new SqlParameter("@ChannelNO", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChannelNO", DataRowVersion.Default, lightAlarm.Channelno)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region DELETE
        /// <summary>
        /// 删除t_LightAlarm
        /// </summary>
        public virtual bool Delete(IList mlist)
        {
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (LightAlarmOR obj in mlist)
            {
                string sql = string.Format(" delete from t_LightAlarm where  Lightid = '{0}'", obj.Lightid);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
        }
        #endregion
    }
}

