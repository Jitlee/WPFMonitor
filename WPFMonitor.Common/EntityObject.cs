using System;
using System.ComponentModel;

namespace WPFMonitor
{
    /// <summary>
    /// 实体类
    /// </summary>
    internal class EntityObject : INotifyPropertyChanged
    {
        public ILogger Log = LoggerFactory.GetLogger(typeof(EntityObject).FullName);

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 实现属性更改通知
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
