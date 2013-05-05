using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.ObjectModel;
using WPFMonitor.Model.SerMonitor;

namespace WPFMonitor.DAL.SerMonitor
{
    public class AlertLogReportDA : DALBase
    {
        public ObservableCollection<AlarmLogOR>  GetAlarmLog(string sql)
        {
            DataTable dt= db.ExecuteQuery(sql);
            ObservableCollection<AlarmLogOR> AlarmLogS = new ObservableCollection<AlarmLogOR>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AlarmLogS.Add(new AlarmLogOR(dr));
                }
            }
            return AlarmLogS;
        }
    }
}
