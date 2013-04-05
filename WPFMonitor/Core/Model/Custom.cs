using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFMonitor.Core.Model
{
    internal class Custom : EntityObject
    {
        #region 变量

        // 保存用户名称
        private string _displayName;

        // 保存代办业务
        private string _business;

        // 保存客户类型
        private string _customType;

        // 保存服务等级
        private string _serviceLevel;
        
        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置客户名称
        /// </summary>
        public string DisplayName { get { return _displayName; } set { _displayName = value; RaisePropertyChanged("DisplayName"); } }

        /// <summary>
        /// 获取或设置代办业务
        /// </summary>
        public string Business { get { return _business; } set { _business = value; RaisePropertyChanged("Business"); } }

        /// <summary>
        /// 获取或设置客户类型
        /// </summary>
        public string CustomType { get { return _customType; } set { _customType = value; RaisePropertyChanged("CustomType"); } }

        /// <summary>
        /// 获取或设置服务等级
        /// </summary>
        public string ServiceLevel { get { return _serviceLevel; } set { _serviceLevel = value; RaisePropertyChanged("ServiceLevel"); } }

        #endregion
    }
}
