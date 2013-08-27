using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WPFMonitor.Model.AlertAdmin
{
	public class AlarmLogOR
	{
		public string Content { get; set; }
		public DateTime HappenTime { get; set; }
		public int AlarmLevel { get; set; }
		public string StationName { get; set; }
        public string DeviceName { get; set; }
        public string AlarmType { get; set; }
		public int AlarmLogID { get; set; }

		public AlarmLogOR(DataRow dr)
		{
			AlarmLogID = Convert.ToInt32(dr["AlarmLogID"]);
			Content = dr["Content"].ToString();
			HappenTime = Convert.ToDateTime(dr["HappenTime"]);
			AlarmLevel = Convert.ToInt32(dr["AlarmLevel"]);
			StationName = dr["StationName"].ToString();
            DeviceName = dr["DeviceName"].ToString();
            AlarmType = dr["AlarmType"].ToString();
		}
	}
}
