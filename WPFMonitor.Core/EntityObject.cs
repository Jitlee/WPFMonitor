using System;
using System.ComponentModel;

namespace WPFMonitor.Core
{
    public abstract class EntityObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 实现属性更改通知
        /// </summary>
        /// <param name="info"></param>
        public void RaisePropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
