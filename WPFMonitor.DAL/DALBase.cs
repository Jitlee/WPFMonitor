using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBUtility;

namespace WPFMonitor.DAL
{
    public class DALBase
    {
        protected SqlHelper db = new SqlHelper();
        protected SqlHelper MoniBase = new SqlHelper("MonitorBase");
    }
}
