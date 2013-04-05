using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBUtility;
using WPFMonitor.Model.Linkage;


namespace WPFMonitor.DAL.Linkage
{
    /// <summary>
    /// 
    /// </summary>
    public class TimeLinkageDA : DALBase
    {

        #region 查询
        public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
        {
            string sql = "select * from t_TimeLinkage";
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

        public TimeLinkageOR selectARowDate(string m_id)
        {
            string sql = string.Format("select * from t_TimeLinkage where  Linkageid='{0}'", m_id);
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
            TimeLinkageOR m_Time = new TimeLinkageOR(dr);
            return m_Time;

        }


        public ObservableCollection<TimeLinkageOR> selectAllDate()
        {
            string sql = @"select tl.*,t.StationName,d.DeviceName,c.ChannelName from t_TimeLinkage tl
inner join t_Station t  on tl.LinkageStationID=t.StationID
inner join t_Device d on tl.LinkageDeviceID=d.DeviceID
inner join t_Channel c on tl.LinkageChannelNo=c.ChannelNo and c.DeviceID=d.DeviceID";

            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<TimeLinkageOR> _List = new ObservableCollection<TimeLinkageOR>();
            foreach (DataRow dr in dt.Rows)
            {
                TimeLinkageOR obj = new TimeLinkageOR(dr);
                _List.Add(obj);
            }
            return _List;
        }


        #endregion

        #region 插入
        /// <summary>
        /// 插入t_TimeLinkage
        /// </summary>
        public virtual bool Insert(TimeLinkageOR timeLinkage)
        {
            string sql = @"insert into t_TimeLinkage (LinkageStationID, LinkageDeviceID, LinkageChannelNo, TriggerTime) 
values (@LinkageStationID, @LinkageDeviceID, @LinkageChannelNo, @TriggerTime)";
            SqlParameter[] parameters = new SqlParameter[]
			{
				//new SqlParameter("@LinkageID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LinkageID", DataRowVersion.Default, timeLinkage.Linkageid),
				new SqlParameter("@LinkageStationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LinkageStationID", DataRowVersion.Default, timeLinkage.Linkagestationid),
				new SqlParameter("@LinkageDeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LinkageDeviceID", DataRowVersion.Default, timeLinkage.Linkagedeviceid),
				new SqlParameter("@LinkageChannelNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LinkageChannelNo", DataRowVersion.Default, timeLinkage.Linkagechannelno),
				new SqlParameter("@TriggerTime", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "TriggerTime", DataRowVersion.Default, timeLinkage.Triggertime)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 更新t_TimeLinkage
        /// </summary>
        public virtual bool Update(TimeLinkageOR timeLinkage)
        {
            string sql = "update t_TimeLinkage set  LinkageStationID = @LinkageStationID,  LinkageDeviceID = @LinkageDeviceID,  LinkageChannelNo = @LinkageChannelNo,  TriggerTime = @TriggerTime where  LinkageID = @LinkageID";
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@LinkageID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LinkageID", DataRowVersion.Default, timeLinkage.Linkageid),
				new SqlParameter("@LinkageStationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LinkageStationID", DataRowVersion.Default, timeLinkage.Linkagestationid),
				new SqlParameter("@LinkageDeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LinkageDeviceID", DataRowVersion.Default, timeLinkage.Linkagedeviceid),
				new SqlParameter("@LinkageChannelNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LinkageChannelNo", DataRowVersion.Default, timeLinkage.Linkagechannelno),
				new SqlParameter("@TriggerTime", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "TriggerTime", DataRowVersion.Default, timeLinkage.Triggertime)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region DELETE
        /// <summary>
        /// 删除t_TimeLinkage
        /// </summary>
        public virtual bool Delete(IList mlist)
        {
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (TimeLinkageOR obj in mlist)
            {
                string sql = string.Format(" delete from t_TimeLinkage where  Linkageid = '{0}'", obj.Linkageid);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
        }
        #endregion
    }
}

