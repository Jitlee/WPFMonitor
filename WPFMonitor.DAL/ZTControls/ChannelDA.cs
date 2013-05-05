using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBUtility;
using WPFMonitor.Model.ZTControls;


namespace WPFMonitor.DAL.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class ChannelDA : DALBase
    {
        
		#region 查询
public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
{
string sql = "select * from t_Channel";
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

public t_Channel selectARowDate(string m_id)
{string sql = string.Format("select * from t_Channel where  Deviceid='{0}'",m_id);
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
t_Channel m_Chan=new t_Channel(dr); 
 return m_Chan;

}


        public ObservableCollection<t_Channel> selectAllDate()
        {
            string sql = "select * from t_Channel";
           
            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<t_Channel> _List = new ObservableCollection<t_Channel>();
            foreach (DataRow dr in dt.Rows)
            {
                t_Channel obj = new t_Channel(dr);
                _List.Add(obj);
            }
            return _List;
        }


		#endregion

		#region 插入
		/// <summary>
		/// 插入t_Channel
		/// </summary>
		public virtual bool Insert(t_Channel channel)
		{
			string sql = "insert into t_Channel (DeviceID, ChannelNo, ChannelName, Value0_Name, Value1_Name, CurrentValue, ChannelParam) values (@DeviceID, @ChannelNo, @ChannelName, @Value0_Name, @Value1_Name, @CurrentValue, @ChannelParam)";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, channel.DeviceID),
				new SqlParameter("@ChannelNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChannelNo", DataRowVersion.Default, channel.ChannelNo),
				new SqlParameter("@ChannelName", SqlDbType.NVarChar, 512, ParameterDirection.Input, false, 0, 0, "ChannelName", DataRowVersion.Default, channel.ChannelName),
				new SqlParameter("@Value0_Name", SqlDbType.NVarChar, 256, ParameterDirection.Input, false, 0, 0, "Value0_Name", DataRowVersion.Default, channel.Value0Name),
				new SqlParameter("@Value1_Name", SqlDbType.NVarChar, 256, ParameterDirection.Input, false, 0, 0, "Value1_Name", DataRowVersion.Default, channel.Value1Name),
				new SqlParameter("@CurrentValue", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "CurrentValue", DataRowVersion.Default, channel.CurrentValue),
				new SqlParameter("@ChannelParam", SqlDbType.NVarChar, 256, ParameterDirection.Input, false, 0, 0, "ChannelParam", DataRowVersion.Default, channel.ChannelParam)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region 修改
		/// <summary>
		/// 更新t_Channel
		/// </summary>
		public virtual bool Update(t_Channel channel)
		{
			string sql = "update t_Channel set  ChannelNo = @ChannelNo,  ChannelName = @ChannelName,  Value0_Name = @Value0_Name,  Value1_Name = @Value1_Name,  CurrentValue = @CurrentValue,  ChannelParam = @ChannelParam where  DeviceID = @DeviceID";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, channel.DeviceID),
				new SqlParameter("@ChannelNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChannelNo", DataRowVersion.Default, channel.ChannelNo),
				new SqlParameter("@ChannelName", SqlDbType.NVarChar, 512, ParameterDirection.Input, false, 0, 0, "ChannelName", DataRowVersion.Default, channel.ChannelName),
				new SqlParameter("@Value0_Name", SqlDbType.NVarChar, 256, ParameterDirection.Input, false, 0, 0, "Value0_Name", DataRowVersion.Default, channel.Value0Name),
				new SqlParameter("@Value1_Name", SqlDbType.NVarChar, 256, ParameterDirection.Input, false, 0, 0, "Value1_Name", DataRowVersion.Default, channel.Value1Name),
				new SqlParameter("@CurrentValue", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "CurrentValue", DataRowVersion.Default, channel.CurrentValue),
				new SqlParameter("@ChannelParam", SqlDbType.NVarChar, 256, ParameterDirection.Input, false, 0, 0, "ChannelParam", DataRowVersion.Default, channel.ChannelParam)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region DELETE
		/// <summary>
		/// 删除t_Channel
		/// </summary>
		public virtual bool Delete(IList mlist){
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (t_Channel obj in mlist)
            {
                string sql = string.Format(" delete from t_Channel where  Deviceid = '{0}'", obj.DeviceID);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
}
		#endregion
    }
}

