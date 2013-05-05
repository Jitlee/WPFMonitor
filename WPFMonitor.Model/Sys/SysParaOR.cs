using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WPFMonitor.Model.Sys
{
    public class SysParaOR
    {

        private string _Keystr;
        /// <summary>
        /// 
        /// </summary>
        public string Keystr
        {
            get { return _Keystr; }
            set { _Keystr = value; }
        }

        private string _Valstr;
        /// <summary>
        /// 
        /// </summary>
        public string Valstr
        {
            get { return _Valstr; }
            set { _Valstr = value; }
        }

        /// <summary>
        /// SysPara构造函数
        /// </summary>
        public SysParaOR()
        {

        }

        /// <summary>
        /// SysPara构造函数
        /// </summary>
        public SysParaOR(DataRow row)
        {
            // 
            _Keystr = row["KeyStr"].ToString().Trim();
            // 
            _Valstr = row["ValStr"].ToString().Trim();
        }
    }
}
