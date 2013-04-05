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
    public class InspectionDA : DALBase
    {

        #region 查询
        public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
        {
            string sql = "select * from t_Inspection";
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

        public InspectionOR selectARowDate(string m_id)
        {
            string sql = string.Format("select * from t_Inspection where  Inspectionid='{0}'", m_id);
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
            InspectionOR m_Insp = new InspectionOR(dr);
            return m_Insp;

        }


        public ObservableCollection<InspectionOR> selectAllDate()
        {
            string sql = @"select fp.*
,t.StationName,d.DeviceName,c.ChannelName,ty.TypeName  from t_Inspection fp
inner join t_Station t  on fp.StationID=t.StationID
inner join t_Device d on fp.DeviceID=d.DeviceID
inner join t_DeviceType ty on ty.DeviceTypeID=d.DeviceTypeID
inner join t_Channel c on fp.ChannelNO=c.ChannelNo and c.DeviceID=d.DeviceID";

            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<InspectionOR> _List = new ObservableCollection<InspectionOR>();
            foreach (DataRow dr in dt.Rows)
            {
                InspectionOR obj = new InspectionOR(dr);
                _List.Add(obj);
            }
            return _List;
        }


        #endregion

        #region 插入
        /// <summary>
        /// 插入t_Inspection
        /// </summary>
        public virtual bool Insert(InspectionOR inspection)
        {
            string sql = @"insert into t_Inspection (StationID, DeviceTypeID, DeviceID, ChannelNO, AlarmWay, SmsEmail, PhoneMedia, InspectionTime, InspectionType, ValueType) 
values (@StationID, @DeviceTypeID, @DeviceID, @ChannelNO, @AlarmWay, @SmsEmail, @PhoneMedia, @InspectionTime, @InspectionType, @ValueType)";
            SqlParameter[] parameters = new SqlParameter[]
			{
		//		new SqlParameter("@InspectionID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "InspectionID", DataRowVersion.Default, inspection.Inspectionid),
				new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, inspection.Stationid),
				new SqlParameter("@DeviceTypeID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceTypeID", DataRowVersion.Default, inspection.Devicetypeid),
				new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, inspection.Deviceid),
				new SqlParameter("@ChannelNO", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChannelNO", DataRowVersion.Default, inspection.Channelno),
				new SqlParameter("@AlarmWay", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "AlarmWay", DataRowVersion.Default, inspection.Alarmway),
				new SqlParameter("@SmsEmail", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "SmsEmail", DataRowVersion.Default, inspection.Smsemail),
				new SqlParameter("@PhoneMedia", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "PhoneMedia", DataRowVersion.Default, inspection.Phonemedia),
				new SqlParameter("@InspectionTime", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "InspectionTime", DataRowVersion.Default, inspection.Inspectiontime),
				new SqlParameter("@InspectionType", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "InspectionType", DataRowVersion.Default, inspection.Inspectiontype),
				new SqlParameter("@ValueType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ValueType", DataRowVersion.Default, inspection.Valuetype)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 更新t_Inspection
        /// </summary>
        public virtual bool Update(InspectionOR inspection)
        {
            string sql = "update t_Inspection set  StationID = @StationID,  DeviceTypeID = @DeviceTypeID,  DeviceID = @DeviceID,  ChannelNO = @ChannelNO,  AlarmWay = @AlarmWay,  SmsEmail = @SmsEmail,  PhoneMedia = @PhoneMedia,  InspectionTime = @InspectionTime,  InspectionType = @InspectionType,  ValueType = @ValueType where  InspectionID = @InspectionID";
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@InspectionID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "InspectionID", DataRowVersion.Default, inspection.Inspectionid),
				new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, inspection.Stationid),
				new SqlParameter("@DeviceTypeID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceTypeID", DataRowVersion.Default, inspection.Devicetypeid),
				new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, inspection.Deviceid),
				new SqlParameter("@ChannelNO", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChannelNO", DataRowVersion.Default, inspection.Channelno),
				new SqlParameter("@AlarmWay", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "AlarmWay", DataRowVersion.Default, inspection.Alarmway),
				new SqlParameter("@SmsEmail", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "SmsEmail", DataRowVersion.Default, inspection.Smsemail),
				new SqlParameter("@PhoneMedia", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "PhoneMedia", DataRowVersion.Default, inspection.Phonemedia),
				new SqlParameter("@InspectionTime", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "InspectionTime", DataRowVersion.Default, inspection.Inspectiontime),
				new SqlParameter("@InspectionType", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "InspectionType", DataRowVersion.Default, inspection.Inspectiontype),
				new SqlParameter("@ValueType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ValueType", DataRowVersion.Default, inspection.Valuetype)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region DELETE
        /// <summary>
        /// 删除t_Inspection
        /// </summary>
        public virtual bool Delete(IList mlist)
        {
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (InspectionOR obj in mlist)
            {
                string sql = string.Format("delete from t_Inspection where  InspectionID = '{0}'", obj.Inspectionid);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
        }
        #endregion
    }
}

