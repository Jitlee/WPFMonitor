using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Runtime.Serialization;

namespace WPFMonitor.DAL.GetData
{
    /// <summary>
    /// Summary description for DataColunmInfo
    /// </summary>
    public class DataColumnInfo
    {
        public string ColumnName { get; set; }

        public string ColumnTitle { get; set; }

        public string DataTypeName { get; set; }

        public bool IsRequired { get; set; }

        public bool IsKey { get; set; }

        public bool IsReadOnly { get; set; }

        public int DisplayIndex { get; set; }

        public string EditControlType { get; set; }

        public int MaxLength { get; set; }
    }
}
