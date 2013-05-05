using System.Collections.ObjectModel;

namespace WPFMonitor.DAL.GetData
{
    /// <summary>
    /// Summary description for DataTableInfo
    /// </summary>
    public class DataTableInfo
    {
        public string TableName { get; set; }

        public ObservableCollection<DataColumnInfo> Columns { get; set; }
    }
}
