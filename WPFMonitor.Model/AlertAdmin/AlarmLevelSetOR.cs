using System;
using System.Data;

namespace WPFMonitor.Model.AlertAdmin
{
    /// <summary>
    /// 
    /// </summary>
    public class AlarmLevelSetOR : ORBase
    {

        private int _Id;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                NotifyPropertyChanged("Id");
            }
        }

        private int _Priority;
        /// <summary>
        /// 级别
        /// </summary>
        public int Priority
        {
            get { return _Priority; }
            set
            {
                _Priority = value;
                NotifyPropertyChanged("Priority");
            }
        }

        private string _Levelname;
        /// <summary>
        /// 等级名称
        /// </summary>
        public string Levelname
        {
            get { return _Levelname; }
            set
            {
                _Levelname = value;
                NotifyPropertyChanged("Levelname");
            }
        }

        private int _Upinterval;
        /// <summary>
        /// 自动升级间隔时间
        /// </summary>
        public int Upinterval
        {
            get { return _Upinterval; }
            set
            {
                _Upinterval = value;
                NotifyPropertyChanged("Upinterval");
            }
        }

        /// <summary>
        /// AlarmLevelSet构造函数
        /// </summary>
        public AlarmLevelSetOR()
        {

        }

        /// <summary>
        /// AlarmLevelSet构造函数
        /// </summary>
        public AlarmLevelSetOR(DataRow row)
        {
            // 
            _Id = Convert.ToInt32(row["ID"]);
            // 级别
            _Priority = Convert.ToInt32(row["Priority"]);
            // 等级名称
            _Levelname = row["LevelName"].ToString().Trim();
            // 自动升级间隔时间
            _Upinterval = Convert.ToInt32(row["UpInterval"]);
        }

        public void Clone(AlarmLevelSetOR obj)
        {
            //
            Id = obj.Id;
            //级别
            Priority = obj.Priority;
            //等级名称
            Levelname = obj.Levelname;
            //自动升级间隔时间
            Upinterval = obj.Upinterval;

        }

    }
}

