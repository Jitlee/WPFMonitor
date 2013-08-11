using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MonitorSystem.MonitorSystemGlobal;
using WPFMonitor.Model;
using System.Collections.Generic;
using System.Windows.Controls.DataVisualization.Charting;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WPFMonitor.Model.ZTControls;
using System.Data;

namespace MonitorSystem.ZTControls
{
    /// <summary>
    /// 57	zedGraphPieCtrl	2	Text.jpg	组态控件	饼图
    /// </summary>
    public class zedGraphPieCtrl : MonitorControl
    {
        Chart _Chart = new Chart();
        public zedGraphPieCtrl()
        {
            this.Content = _Chart;

            _Chart.Title = "标题";
            _Chart.Background = new SolidColorBrush(Colors.White);
            _Chart.Width = 100;
            _Chart.Height = 100;

            //this.SetValue(Canvas.ZIndexProperty, 1);
            this.SizeChanged += new SizeChangedEventHandler(zedGraphCtrl_SizeChanged);
        }
        private void zedGraphCtrl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
           this.Width= _Chart.Width = e.NewSize.Width;
           this.Height= _Chart.Height = e.NewSize.Height;
        }

        #region 控件公共属性
        public override event EventHandler Selected;
		
		public override event EventHandler Unselected;

		private void OnUnselected(object sender, EventArgs e)
		{
			if(null != Unselected)
			{
				Unselected(this, RoutedEventArgs.Empty);
			}
		}

        public override void DesignMode()
        {
            if (!IsDesignMode)
            {
                AdornerLayer = new Adorner(this);
                AdornerLayer.Selected += OnSelected;
            }
        }
        public override void UnDesignMode()
        {
            if (IsDesignMode)
            {
                AdornerLayer.Selected -= OnSelected;
                AdornerLayer.ClearValue(ContextMenuService.ContextMenuProperty);
                AdornerLayer.Dispose();
                AdornerLayer = null;
            }
        }

        private void OnSelected(object sender, EventArgs e)
        {
            if (null != Selected)
            {
                Selected(this, RoutedEventArgs.Empty);
            }
        }

        private string[] m_BrowsableProperties = new string[] { "Left", "Top", "Width", "Height", "FontFamily", "FontSize","Transparent",
            "Translate", "ConnectString","TalbeName","TitleName"};

        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }

        public override void SetCommonPropertyValue()
        {
            if (ScreenElement != null)
            {
                this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
                this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);
                Transparent = ScreenElement.Transparent.Value;

                this.Width = _Chart.Width = (double)ScreenElement.Width.Value;
                this.Height = _Chart.Height = (double)ScreenElement.Height.Value;
            }
        }

        public List<t_ElementProperty> GetProperty()
        {
            return ListElementProp;
        }

        public override FrameworkElement GetRootControl()
        {
            return this;
        }

        #endregion        

        #region 属性
        public override void SetPropertyValue()
        {
            foreach (t_ElementProperty pro in ListElementProp)
            {
                string name = pro.PropertyName.ToUpper();
                string value = pro.PropertyValue;
                if (name == "ConnectString".ToUpper())
                {
                    _ConnectString = value;
                }
                else if (name == "TableName".ToUpper())
                {
                    _TalbeName = value;
                }
                else if (name == "TitleName".ToUpper())
                {
                    TitleName = value;
                }
            }
            LoadData();
        }

        private static readonly DependencyProperty TransparentProperty =
         DependencyProperty.Register("Transparent",
         typeof(int), typeof(zedGraphPieCtrl), new PropertyMetadata(0));
        private int _Transparent;
        [DefaultValue(""), Description("透明"), Category("杂项")]
        public int Transparent
        {
            get { return _Transparent; }
            set
            {
                _Transparent = value;
                PaintBackground();
                if (ScreenElement != null)
                    ScreenElement.Transparent = value;
            }
        }

     
        private static readonly DependencyProperty ConnectStringProperty = DependencyProperty.Register("ConnectString",
       typeof(string), typeof(zedGraphPieCtrl), new PropertyMetadata(""));
        private string _ConnectString;
        [DefaultValue(""), Description("连接字符串"), Category("我的属性")]
        public string ConnectString
        {
            get { return _ConnectString; }
            set
            {
                SetAttrByName("ConnectString", value);
                _ConnectString = value;
                LoadData();
            }
        }

        private static readonly DependencyProperty TableNameProperty =DependencyProperty.Register("TalbeName",
      typeof(string), typeof(zedGraphPieCtrl), new PropertyMetadata(""));
        private string _TalbeName;
        [DefaultValue(""), Description("表名"), Category("我的属性")]
        public string TalbeName
        {
            get { return _TalbeName; }
            set
            {
                SetAttrByName("TableName", value);
                _TalbeName = value;
                LoadData();
            }
        }

        private static readonly DependencyProperty TitleNameProperty = DependencyProperty.Register("TitleName",
       typeof(string), typeof(zedGraphPieCtrl), new PropertyMetadata(""));
        private string _TitleName;
        [DefaultValue(""), Description("标题"), Category("我的属性")]
        public string TitleName
        {
            get { return _TitleName; }
            set
            {
                SetAttrByName("TitleName", value);
                _TitleName = value;
                _Chart.Title = _TitleName;
            }
        }
        #endregion
        private void PaintBackground()
        {
            if (_Transparent == 1)
            {
                _Chart.Background = new SolidColorBrush();
            }
            else
            {
                _Chart.Background = new SolidColorBrush(Colors.White);
            }
        }
        
        #region 从wcf中加载数据
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            if (string.IsNullOrEmpty(_ConnectString))
                return;
            if (string.IsNullOrEmpty(_TalbeName))
                return;
            //添加top 100主要是为了防止，表的数据太多，程序无法加载而死掉
            string strSql =  string.Format("select top 100 * from {0}",  _TalbeName);

            GetData(strSql);
        }
        private void GetData(string sql)
        {
            DataTable dt=   LoadDataDA.GetDataBySql(_ConnectString, sql);
            if (dt != null)
            {
                Bind(dt);
            }
        }

        private void Bind(DataTable dt)
        {
            if (dt == null)
                return;

            var _ColumnSeri = new PieSeries();
            _ColumnSeri.ItemsSource = dt.AsDataView();
           // _ColumnSeri.Title = _BarName;
          
                int Number = 0;
                if (dt.Columns.Count >= 2)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (Number == 0)
                            _ColumnSeri.IndependentValuePath = column.ColumnName;
                        else if (Number == 1)
                        {
                            if (column.DataType == typeof(System.String))
                            {
                                return;
                            }
                            _ColumnSeri.DependentValuePath = column.ColumnName;
                        }
                        Number++;
                    }
                }
            

            _Chart.Title = _TitleName;
            _Chart.Series.Clear();
            _Chart.Series.Add(_ColumnSeri);
        }
        #endregion
    }
}
