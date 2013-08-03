using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MonitorSystem.ZTControls
{
    public class LoadDataDA
    {
        public static DataTable GetDataBySql(string ConnStr, string SQL)
        {
            try
            {
                return new WPFMonitor.DAL.GetData.GetData().GetDataSetData(ConnStr, SQL);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
