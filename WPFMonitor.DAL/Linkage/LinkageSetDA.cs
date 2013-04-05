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
    public class LinkageSetDA : DALBase
    {

        #region 查询
        public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
        {
            string sql = "select * from t_LinkageSet";
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

        public LinkageSetOR selectARowDate(string m_id)
        {
            string sql = string.Format("select * from t_LinkageSet where  Id='{0}'", m_id);
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
            LinkageSetOR m_Link = new LinkageSetOR(dr);
            return m_Link;

        }


        public ObservableCollection<LinkageSetOR> selectAllDate()
        {
            string sql = @"select tl.*,t.StationName,d.DeviceName,c.ChannelName
 ,tLine.StationName as LineStationName,dLine.DeviceName as LineDeviceName,cLine.ChannelName  as LineChannelName
from t_LinkageSet tl
inner join t_Station t  on tl.StationID=t.StationID
inner join t_Device d on tl.DeviceID=d.DeviceID
inner join t_Channel c on tl.ChannelNo=c.ChannelNo and c.DeviceID=d.DeviceID
inner join t_Station tLine  on tl.LinkageStationID=tLine.StationID
inner join t_Device dLine on tl.LinkageDeviceID=dLine.DeviceID
inner join t_Channel cLine on tl.LinkageChannelNo=cLine.ChannelNo and cLine.DeviceID=dLine.DeviceID";

            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<LinkageSetOR> _List = new ObservableCollection<LinkageSetOR>();
            foreach (DataRow dr in dt.Rows)
            {
                LinkageSetOR obj = new LinkageSetOR(dr);
                _List.Add(obj);
            }
            return _List;
        }


        #endregion

        #region 插入
        /// <summary>
        /// 插入t_LinkageSet
        /// </summary>
        public virtual bool Insert(LinkageSetOR linkageSet)
        {
            string sql = @"insert into t_LinkageSet(StationID, DeviceID, ChannelNo, ValueType, TriggerValue, 
LinkageStationID, LinkageDeviceID, LinkageChannelNo) values (@StationID, @DeviceID, @ChannelNo, @ValueType, @TriggerValue, @LinkageStationID, @LinkageDeviceID, @LinkageChannelNo)";
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, linkageSet.Stationid),
				new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, linkageSet.Deviceid),
				new SqlParameter("@ChannelNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChannelNo", DataRowVersion.Default, linkageSet.Channelno),
				new SqlParameter("@ValueType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ValueType", DataRowVersion.Default, linkageSet.Valuetype),
				new SqlParameter("@TriggerValue", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "TriggerValue", DataRowVersion.Default, linkageSet.Triggervalue),
				new SqlParameter("@LinkageStationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LinkageStationID", DataRowVersion.Default, linkageSet.Linkagestationid),
				new SqlParameter("@LinkageDeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LinkageDeviceID", DataRowVersion.Default, linkageSet.Linkagedeviceid),
				new SqlParameter("@LinkageChannelNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LinkageChannelNo", DataRowVersion.Default, linkageSet.Linkagechannelno)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 更新t_LinkageSet
        /// </summary>
        public virtual bool Update(LinkageSetOR linkageSet)
        {
            string sql = "update t_LinkageSet set  StationID = @StationID,  DeviceID = @DeviceID,  ChannelNo = @ChannelNo,  ValueType = @ValueType,  TriggerValue = @TriggerValue,  LinkageStationID = @LinkageStationID,  LinkageDeviceID = @LinkageDeviceID,  LinkageChannelNo = @LinkageChannelNo where  ID = @ID";
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, linkageSet.Id),
				new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, linkageSet.Stationid),
				new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, linkageSet.Deviceid),
				new SqlParameter("@ChannelNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChannelNo", DataRowVersion.Default, linkageSet.Channelno),
				new SqlParameter("@ValueType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ValueType", DataRowVersion.Default, linkageSet.Valuetype),
				new SqlParameter("@TriggerValue", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "TriggerValue", DataRowVersion.Default, linkageSet.Triggervalue),
				new SqlParameter("@LinkageStationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LinkageStationID", DataRowVersion.Default, linkageSet.Linkagestationid),
				new SqlParameter("@LinkageDeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LinkageDeviceID", DataRowVersion.Default, linkageSet.Linkagedeviceid),
				new SqlParameter("@LinkageChannelNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LinkageChannelNo", DataRowVersion.Default, linkageSet.Linkagechannelno)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region DELETE
        /// <summary>
        /// 删除t_LinkageSet
        /// </summary>
        public virtual bool Delete(IList mlist)
        {
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (LinkageSetOR obj in mlist)
            {
                string sql = string.Format(" delete from t_LinkageSet where  Id = '{0}'", obj.Id);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
        }
        #endregion
    }
}

