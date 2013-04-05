using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WPFMonitor.Core.Model
{
    /// <summary>
    /// 实体对象基类
    /// </summary>
    public abstract class EntityObject : INotifyPropertyChanged
    {
        /// <summary>
        /// 属性通知更改通知事件
        /// </summary>
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
