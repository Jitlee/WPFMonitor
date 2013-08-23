using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DBUtility;
using System.Collections;
using System.Collections.ObjectModel;
using WPFMonitor.Model.ZTControls;


namespace WPFMonitor.DAL.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class ScreenMonitorValueDA : DALBase
    {
        
		#region 查询
        public List<V_ScreenMonitorValue> selectAllDate(int ScrennID)
        {
			string sql = string.Format("select * from V_ScreenMonitorValue where ScreenID={0}",ScrennID);
            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            List<V_ScreenMonitorValue> _List = new List<V_ScreenMonitorValue>();
            foreach (DataRow dr in dt.Rows)
            {
                V_ScreenMonitorValue obj = new V_ScreenMonitorValue(dr);
                _List.Add(obj);
            }
            return _List;
        }
		#endregion
    }
}

