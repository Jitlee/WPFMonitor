using System;
using System.Data;

namespace WPFMonitor.Model.Alarm
{
    /// <summary>
    /// 
    /// </summary>
    public class FalseAlarmPolicyOR : ORBase
    {

        private int _Falsealarmid;
        /// <summary>
        /// 机房名称
        /// </summary>
        public int Falsealarmid
        {
            get { return _Falsealarmid; }
            set
            {
                _Falsealarmid = value;
                NotifyPropertyChanged("Falsealarmid");
            }
        }

        private int _Falsepolicyid;
        /// <summary>
        /// 设备名称
        /// </summary>
        public int Falsepolicyid
        {
            get { return _Falsepolicyid; }
            set
            {
                _Falsepolicyid = value;
                NotifyPropertyChanged("Falsepolicyid");
            }
        }

        private int _Policyid;
        /// <summary>
        /// 策略名
        /// </summary>
        public int Policyid
        {
            get { return _Policyid; }
            set
            {
                _Policyid = value;
                NotifyPropertyChanged("Policyid");
            }
        }

        private string _Falsetype;
        /// <summary>
        /// 限制类型
        /// </summary>
        public string Falsetype
        {
            get { return _Falsetype; }
            set
            {
                _Falsetype = value;
                NotifyPropertyChanged("Falsetype");
            }
        }

        private string _StationName;
        /// <summary>
        /// 站点名称
        /// </summary>
        public string StationName
        {
            get { return _StationName; }
            set { _StationName = value; }
        }

        private string _DeviceName;
        /// <summary>
        /// 
        /// </summary>
        public string DeviceName
        {
            get { return _DeviceName; }
            set { _DeviceName = value; }
        }


        private string _ChannelName;
        /// <summary>
        /// 通道
        /// </summary>
        public string ChannelName
        {
            get { return _ChannelName; }
            set { _ChannelName = value; }
        }

        /// <summary>
        /// FalseAlarmPolicy构造函数
        /// </summary>
        public FalseAlarmPolicyOR()
        {

        }

        /// <summary>
        /// FalseAlarmPolicy构造函数
        /// </summary>
        public FalseAlarmPolicyOR(DataRow row)
        {
            // 机房名称
            _Falsealarmid = Convert.ToInt32(row["FalseAlarmID"]);
            // 设备名称
            _Falsepolicyid = Convert.ToInt32(row["FalsePolicyID"]);
            // 策略名
            _Policyid = Convert.ToInt32(row["PolicyID"]);
            // 限制类型
            _Falsetype = row["FalseType"].ToString().Trim();

            StationName = row["StationName"].ToString();
            DeviceName = row["DeviceName"].ToString();
            ChannelName = row["ChannelName"].ToString();
        }

        public void Clone(FalseAlarmPolicyOR obj)
        {
            //机房名称
            Falsealarmid = obj.Falsealarmid;
            //设备名称
            Falsepolicyid = obj.Falsepolicyid;
            //策略名
            Policyid = obj.Policyid;
            //限制类型
            Falsetype = obj.Falsetype;

            StationName = obj.StationName;
            DeviceName = obj.DeviceName;
            ChannelName = obj.ChannelName;
        }

    }
}

