using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WPFMonitor.Model.Alarm
{
    public class PolicyChannelOR
    {
        private int _ChannelNo;
        /// <summary>
        /// 通道号
        /// </summary>
        public int ChannelNo
        {
            get { return _ChannelNo; }
            set { _ChannelNo = value; }
        }

        private string _ChannelName;
        /// <summary>
        /// 通道名称
        /// </summary>
        public string ChannelName
        {
            get { return _ChannelName; }
            set { _ChannelName = value; }
        }

        private int _AlarmPolicyManagementID;
        /// <summary>
        /// 告警策略ID
        /// </summary>
        public int AlarmPolicyManagementID
        {
            get { return _AlarmPolicyManagementID; }
            set { _AlarmPolicyManagementID = value; }
        }

        public PolicyChannelOR(DataRow dr)
        {
            ChannelNo = Convert.ToInt32(dr["ChannelNo"]);
            ChannelName = dr["ChannelName"].ToString();
            AlarmPolicyManagementID = Convert.ToInt32(dr["AlarmPolicyManagementID"]);
        }
    }
}
