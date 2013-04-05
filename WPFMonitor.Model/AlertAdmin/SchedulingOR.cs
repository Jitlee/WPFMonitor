using System;
using System.Data;

namespace WPFMonitor.Model.AlertAdmin
{
    /// <summary>
    /// 
    /// </summary>
    public class SchedulingOR : ORBase
    {

        private int _Frequency;
        /// <summary>
        /// 
        /// </summary>
        public int Frequency
        {
            get { return _Frequency; }
            set
            {
                _Frequency = value;
                NotifyPropertyChanged("Frequency");
            }
        }

        private string _Frequencyname;
        /// <summary>
        /// 班次名称
        /// </summary>
        public string Frequencyname
        {
            get { return _Frequencyname; }
            set
            {
                _Frequencyname = value;
                NotifyPropertyChanged("Frequencyname");
            }
        }

        private string _Starttime;
        /// <summary>
        /// 开始时间
        /// </summary>
        public string Starttime
        {
            get { return _Starttime; }
            set
            {
                _Starttime = value;
                NotifyPropertyChanged("Starttime");
            }
        }

        private string _Endtime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public string Endtime
        {
            get { return _Endtime; }
            set
            {
                _Endtime = value;
                NotifyPropertyChanged("Endtime");
            }
        }

        /// <summary>
        /// Scheduling构造函数
        /// </summary>
        public SchedulingOR()
        {

        }

        /// <summary>
        /// Scheduling构造函数
        /// </summary>
        public SchedulingOR(DataRow row)
        {
            // 
            _Frequency = Convert.ToInt32(row["Frequency"]);
            // 班次名称
            _Frequencyname = row["FrequencyName"].ToString().Trim();
            // 开始时间
            _Starttime = row["StartTime"].ToString().Trim();
            // 结束时间
            _Endtime = row["EndTime"].ToString().Trim();
        }

        public void Clone(SchedulingOR obj)
        {
            //
            Frequency = obj.Frequency;
            //班次名称
            Frequencyname = obj.Frequencyname;
            //开始时间
            Starttime = obj.Starttime;
            //结束时间
            Endtime = obj.Endtime;

        }

    }
}

