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
    public class AlramBindTimeDA : DALBase
    {

        #region 查询
        public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
        {
            string sql = "select * from t_AlramBindTime";
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

        public AlramBindTimeOR selectARowDate(string m_id)
        {
            string sql = string.Format("select * from t_AlramBindTime where  Id='{0}'", m_id);
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
            AlramBindTimeOR m_Alra = new AlramBindTimeOR(dr);
            return m_Alra;

        }


        public ObservableCollection<AlramBindTimeOR> selectAllDate()
        {
            string sql = @"select tl.*,t.StationName,d.DeviceName,c.ChannelName from t_AlramBindTime tl
inner join t_Station t  on tl.StationID=t.StationID
inner join t_Device d on tl.DeviceID=d.DeviceID
inner join t_Channel c on tl.ChannelNo=c.ChannelNo and c.DeviceID=d.DeviceID";

            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<AlramBindTimeOR> _List = new ObservableCollection<AlramBindTimeOR>();
            foreach (DataRow dr in dt.Rows)
            {
                AlramBindTimeOR obj = new AlramBindTimeOR(dr);
                _List.Add(obj);
            }
            return _List;
        }


        #endregion

        #region 插入
        /// <summary>
        /// 插入t_AlramBindTime
        /// </summary>
        public virtual bool Insert(AlramBindTimeOR alramBindTime)
        {
            string sql = "insert into t_AlramBindTime (StationID, DeviceID, ChannelNo, IntervalTime) values(@StationID, @DeviceID, @ChannelNo, @IntervalTime)";
            SqlParameter[] parameters = new SqlParameter[]
			{
				//new SqlParameter("@ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, alramBindTime.Id),
				new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, alramBindTime.Stationid),
				new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, alramBindTime.Deviceid),
				new SqlParameter("@ChannelNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChannelNo", DataRowVersion.Default, alramBindTime.Channelno),
				new SqlParameter("@IntervalTime", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "IntervalTime", DataRowVersion.Default, alramBindTime.Intervaltime)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 更新t_AlramBindTime
        /// </summary>
        public virtual bool Update(AlramBindTimeOR alramBindTime)
        {
            string sql = "update t_AlramBindTime set  StationID = @StationID,  DeviceID = @DeviceID,  ChannelNo = @ChannelNo,  IntervalTime = @IntervalTime where  ID = @ID";
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, alramBindTime.Id),
				new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, alramBindTime.Stationid),
				new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, alramBindTime.Deviceid),
				new SqlParameter("@ChannelNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChannelNo", DataRowVersion.Default, alramBindTime.Channelno),
				new SqlParameter("@IntervalTime", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "IntervalTime", DataRowVersion.Default, alramBindTime.Intervaltime)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region DELETE
        /// <summary>
        /// 删除t_AlramBindTime
        /// </summary>
        public virtual bool Delete(IList mlist)
        {
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (AlramBindTimeOR obj in mlist)
            {
                string sql = string.Format("delete from t_AlramBindTime where  ID = '{0}'", obj.Id);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
        }
        #endregion
    }
}

