using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WPFMonitor.Model.SerMonitor
{
    public class AlarmLogOR
    {
        public string StationName { get; set; }
        public string EventsName { get; set; }
        public string AlarmLevel { get; set; }
        public string HappenTime { get; set; }
        public string AlarmType { get; set; }
        public string DeviceName { get; set; }
        public string Content { get; set; }
        public string MonitorValue { get; set; }

        public AlarmLogOR(DataRow dr)
        {
            StationName = dr["StationName"].ToString();
            EventsName = dr["EventsName"].ToString();
            AlarmLevel = dr["AlarmLevel"].ToString();
            HappenTime = dr["HappenTime"].ToString();
            AlarmType = dr["AlarmType"].ToString();
            DeviceName = dr["DeviceName"].ToString();
            Content = dr["Content"].ToString();
            MonitorValue = dr["MonitorValue"].ToString();
        }
    }
}
