using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace WPFMonitor.DAL.GetData
{
    public class DataSetData
    {
        public ObservableCollection<DataTableInfo> Tables { get; set; }
        public string DataXML { get; set; }

        public static DataSetData FromDataSet(DataSet ds)
        {
            DataSetData dsd = new DataSetData();
            dsd.Tables = new ObservableCollection<DataTableInfo>();
            foreach (DataTable t in ds.Tables)
            {
                DataTableInfo tableInfo = new DataTableInfo { TableName = t.TableName };
                dsd.Tables.Add(tableInfo);
                tableInfo.Columns = new ObservableCollection<DataColumnInfo>();
                foreach (DataColumn c in t.Columns)
                {
                    DataColumnInfo col = new DataColumnInfo { ColumnName = c.ColumnName, ColumnTitle = c.ColumnName, DataTypeName = c.DataType.FullName, MaxLength = c.MaxLength, IsKey = c.Unique, IsReadOnly = (c.Unique || c.ReadOnly), IsRequired = !c.AllowDBNull };
                    if (c.DataType == typeof(System.Guid))
                    {
                        col.IsReadOnly = true;
                        col.DisplayIndex = -1;
                    }
                    tableInfo.Columns.Add(col);
                }
            }
            dsd.DataXML = ds.GetXml();
            return dsd;
        }

        public static DataSet ToDataSet(DataSetData dsd)
        {
            DataSet ds = new DataSet();
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(dsd.DataXML);
            MemoryStream stream = new MemoryStream(byteArray);
            XmlReader reader = new XmlTextReader(stream);
            ds.ReadXml(reader);
            XDocument xd = XDocument.Parse(dsd.DataXML);
            foreach (DataTable dt in ds.Tables)
            {
                var rs = from row in xd.Descendants(dt.TableName)
                         select row;
            }
            return ds;
        }
    }
}




