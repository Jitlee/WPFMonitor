using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Windows.Controls.DataVisualization.Charting;
using System.Data;

namespace WPFMonitor
{
    /// <summary>
    /// test.xaml 的交互逻辑
    /// </summary>
    public partial class test : Window
    {
        public ObservableCollection<xy> DataList { get; set; }

        public test()
        {
            InitializeComponent();

            DataList = new ObservableCollection<xy>();
            DataList.Add(new xy() { X = "一", Y = 20 });
            DataList.Add(new xy() { X = "二", Y = 22 });
            DataList.Add(new xy() { X = "三", Y = 23 });
            DataList.Add(new xy() { X = "四", Y = 24 });
            DataList.Add(new xy() { X = "五", Y = 25 });

            this.DataContext = this;
            //((ColumnSeries)this.mcChart.Series[0]).ItemsSource = DataList;
            //LineSeries1.IndependentValuePath = "X";
            //LineSeries1.DependentValuePath = "Y";
           // LineSeries1.ItemsSource = DataList;// dbTable.DefaultView;
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //((ColumnSeries)this.mcChart.Series[0]).ItemsSource = new KeyValuePair<int, int>[]{
            //    new KeyValuePair<int,int>(1,400),
            //    new KeyValuePair<int,int>(2,200),
            //    new KeyValuePair<int,int>(3,300),
            //    new KeyValuePair<int,int>(4,320),
            //    new KeyValuePair<int,int>(5,150)
            //};
            //((AreaSeries)this.mcChart.Series[1]).ItemsSource = new KeyValuePair<int, int>[]{
            //    new KeyValuePair<int,int>(1,400),
            //    new KeyValuePair<int,int>(2,200),
            //    new KeyValuePair<int,int>(3,300),
            //    new KeyValuePair<int,int>(4,320),
            //    new KeyValuePair<int,int>(5,150)
            //};
            //------------------------------------------------------
            //DataTable dt = new DataTable();
            //dt.Columns.Add("1");
            //dt.Columns.Add("2");
            //dt.Columns.Add("3");
            //dt.Columns.Add("4");
            //dt.Columns.Add("5");
            //dt.Columns.Add("6");
            //Random rand = new Random();
            //for (int i = 0; i < 10; i++)
            //{
            //    DataRow newRow = dt.NewRow();
            //    for (int j = 0; j < dt.Columns.Count; j++)
            //    {
            //        newRow[j] = rand.Next(300);
            //    }
            //    dt.Rows.Add(newRow);
            //}
            //Chart chart = new CreateToolKit(300, 250).GetNewChart(dt, GraphEnum.Area);
            //Canvas.SetLeft(chart, 200);
            //Canvas.SetTop(chart, 200);
            //this.CanChart.Children.Add(chart);
        }

        public void ShowLogData(LineSeries lineSerie, DataTable dbTable)
        {
            
        }



    }
    /// <summary>
        /// 绘制图形枚举。
        /// </summary>
        public enum GraphEnum
        {
            /// <summary>
            /// 区域图形。
            /// </summary>
            Area,
            /// <summary>
            /// 条形图形。
            /// </summary>
            Bar,
            /// <summary>
            /// 气泡图形。
            /// </summary>
            Bubble,/// <summary>
            /// 柱图形。
            /// </summary>
            Column,
            /// <summary>
            /// 线图形。
            /// </summary>
            Line,
            /// <summary>
            /// 饼图形。
            /// </summary>
            Pie,
            /// <summary>
            /// 散点图形
            /// </summary>
            Scatter,
        }



    /// <summary>
        /// DataGrid数据生成图形信息操作类。
        /// </summary>
        public class CreateToolKit
        {
            /// <summary>
            /// Chart的基本宽度。
            /// </summary>
            private double KitWitdth = 0;
            /// <summary>
            /// Chart的基本高度。
            /// </summary>
            private double KitHeight = 0;
            /// <summary>
            /// 绘制图表类。
            /// </summary>
            public Chart newChart = null;
            /// <summary>
            /// 设置图形表的基本属性内容。
            /// </summary>
            /// <param name="width"></param>
            /// <param name="height"></param>
            public CreateToolKit(double width, double height)
            {
                this.KitHeight = height;
                this.KitWitdth = width;
                this.newChart = new Chart();
                this.newChart.Width = this.KitWitdth;
                this.newChart.Height = this.KitHeight;
                this.newChart.LegendTitle = null;
                this.newChart.Background = Brushes.Azure;
            }

            #region================实现方法=====================
            /// <summary>
            /// 生成指定图形的表。
            /// </summary>
            /// <param name="table">表数据。</param>
            /// <param name="graph">绘制图形类型。</param>
            /// <returns></returns>
            public Chart GetNewChart(DataTable table, GraphEnum graph)
            {
                try
                {
                    switch (graph)
                    {
                        case GraphEnum.Area:
                            newChart.Series.Add(SetDrawData<AreaSeries>(table));
                            break;
                        case GraphEnum.Bar:
                            newChart.Series.Add(SetDrawData<BarSeries>(table));
                            break;
                        case GraphEnum.Bubble:
                            newChart.Series.Add(SetDrawData<BubbleSeries>(table));
                            break;
                        case GraphEnum.Column:
                            newChart.Series.Add(SetDrawData<ColumnSeries>(table));
                            break;
                        case GraphEnum.Line:
                            newChart.Series.Add(SetDrawData<LineSeries>(table));
                            break;
                            case GraphEnum.Pie:
                            newChart.Series.Add(SetDrawData<PieSeries>(table));
                            break;
                        case GraphEnum.Scatter:
                            newChart.Series.Add(SetDrawData<ScatterSeries>(table));
                            break;
                    }
                    return newChart;
                }
                catch (Exception TableEx)
                {
                    //错误是计算的数值有非数字时，就可能出错，返回空值！    
                    return null;
                }
            }
            /// <summary>
            /// 对图形进行值的添加。
            /// </summary>
            /// <param name="table">数据源。</param>
            /// <returns>图形对象。</returns>
            private T SetDrawData<T>(DataTable table) where T : new()
            {
                Binding KeyBind = new Binding("Key");
                Binding ValueBind = new Binding("Value");
                T ret = new T();
                Type obj = ret.GetType();
                obj.GetProperty("Title").SetValue(ret, "Example", null);
                obj.GetProperty("IndependentValueBinding").SetValue(ret, KeyBind, null);
                obj.GetProperty("DependentValueBinding").SetValue(ret, ValueBind, null);
                obj.GetProperty("ItemsSource").SetValue(ret, GetTableData(table), null);
                return ret;
            }

            /// <summary>
            /// 对数据进行标准转换,转成图形能加载的形式。
            /// 【DataTable转换成Dictionary】,是按列的统计来添加数据。
            /// </summary>
            /// <param name="table">DataTable数据源。</param>
            /// <returns></returns>
            private Dictionary<int, double> GetTableData(DataTable table)
            {
                Dictionary<int, double> ReDict = new Dictionary<int, double>();
                int i = 1;
                foreach (DataColumn col in table.Columns)
                {
                    double tempDouble = 0;
                    foreach (DataRow row in table.Rows)
                    {
                        if (!string.IsNullOrEmpty(row[col.ColumnName].ToString()))
                            tempDouble += Convert.ToDouble(row[col.ColumnName]);
                    }
                    ReDict.Add(i, tempDouble);
                    i += 1;
                }
                return ReDict;
            }
            #endregion
        }
    


    public class xy
    {
        public string X { get; set; }
        public int Y { get; set; }
    }
}
