using System;
using System.Data;

namespace WPFMonitor.Model.AlertAdmin
{
    /// <summary>
    /// 
    /// </summary>
    public class DisarmTimeOR : ORBase
    {

        private int _Disarmid;
        /// <summary>
        /// 
        /// </summary>
        public int Disarmid
        {
            get { return _Disarmid; }
            set
            {
                _Disarmid = value;
                NotifyPropertyChanged("Disarmid");
            }
        }

        private string _Disarmname;
        /// <summary>
        /// 撤防名称
        /// </summary>
        public string Disarmname
        {
            get { return _Disarmname; }
            set
            {
                _Disarmname = value;
                NotifyPropertyChanged("Disarmname");
            }
        }

        private string _Disarmstarttime;
        /// <summary>
        /// 撤防开始时间
        /// </summary>
        public string Disarmstarttime
        {
            get { return _Disarmstarttime; }
            set
            {
                _Disarmstarttime = value;
                NotifyPropertyChanged("Disarmstarttime");
            }
        }

        private string _Disarmendtime;
        /// <summary>
        /// 撤防结束时间
        /// </summary>
        public string Disarmendtime
        {
            get { return _Disarmendtime; }
            set
            {
                _Disarmendtime = value;
                NotifyPropertyChanged("Disarmendtime");
            }
        }

        /// <summary>
        /// DisarmTime构造函数
        /// </summary>
        public DisarmTimeOR()
        {

        }

        /// <summary>
        /// DisarmTime构造函数
        /// </summary>
        public DisarmTimeOR(DataRow row)
        {
            // 
            _Disarmid = Convert.ToInt32(row["DisarmID"]);
            // 撤防名称
            _Disarmname = row["DisarmName"].ToString().Trim();
            // 撤防开始时间
            _Disarmstarttime = row["DisarmStartTime"].ToString().Trim();
            // 撤防结束时间
            _Disarmendtime = row["DisarmEndTime"].ToString().Trim();
        }

        public void Clone(DisarmTimeOR obj)
        {
            //
            Disarmid = obj.Disarmid;
            //撤防名称
            Disarmname = obj.Disarmname;
            //撤防开始时间
            Disarmstarttime = obj.Disarmstarttime;
            //撤防结束时间
            Disarmendtime = obj.Disarmendtime;

        }

    }
}

