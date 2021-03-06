﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Documents;
using System.Windows.Media;
using MonitorSystem.MonitorSystemGlobal;
using WPFMonitor.Model.ZTControls;
using System.Data.SqlClient;
using System.Data;

namespace MonitorSystem.ZTControls
{
    /// <summary>
    /// 58	zedGraphCtrl	2	Text.jpg	组态控件	柱状图
    /// </summary>
    public class zedGraphCtrl : MonitorControl
    {
        Chart _Chart = new Chart();
        public zedGraphCtrl()
        {
            this.Content = _Chart;

            _Chart.Title = "标题";
            _Chart.Background = new SolidColorBrush(Colors.White);
            _Chart.Width = 100;
            _Chart.Height = 100;

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
            "Translate", "ConnectString","TalbeName","TitleName","XaxisName","YaxisName","BarName"};

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
                }//
                else if (name == "BarName".ToUpper())
                {
                    _BarName = value;
                }
                else if (name == "TitleName".ToUpper())
                {
                    _TitleName = value;
                }
                else if (name == "XaxisName".ToUpper())
                {
                    _XaxisName = value;
                }
                else if (name == "YaxisName".ToUpper())
                {
                    _YaxisName = value;
                }
            }
            LoadData();
        }

        private static readonly DependencyProperty TransparentProperty =
         DependencyProperty.Register("Transparent",
         typeof(int), typeof(zedGraphCtrl), new PropertyMetadata(0));
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

     
        private static readonly DependencyProperty ConnectStringProperty =DependencyProperty.Register("ConnectString",
       typeof(string), typeof(zedGraphCtrl), new PropertyMetadata(""));
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
      typeof(string), typeof(zedGraphCtrl), new PropertyMetadata(""));
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

        private static readonly DependencyProperty TitleNameProperty =DependencyProperty.Register("TitleName",
       typeof(string), typeof(zedGraphCtrl), new PropertyMetadata(""));
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


        private static readonly DependencyProperty XaxisNameProperty =DependencyProperty.Register("XaxisName",
       typeof(string), typeof(zedGraphCtrl), new PropertyMetadata(""));
        private string _XaxisName;
        [DefaultValue(""), Description("X轴名称"), Category("我的属性")]
        public string XaxisName
        {
            get { return _XaxisName; }
            set
            {
                SetAttrByName("XaxisName", value);
                _XaxisName = value;
                LoadData();
            }
        }


        private static readonly DependencyProperty YaxisNameProperty =DependencyProperty.Register("YaxisName",
       typeof(string), typeof(zedGraphCtrl), new PropertyMetadata(""));
        private string _YaxisName;
        [DefaultValue(""), Description("Y轴名称"), Category("我的属性")]
        public string YaxisName
        {
            get { return _YaxisName; }
            set
            {
                SetAttrByName("YaxisName", value);
                _YaxisName = value;
                LoadData();
            }
        }
          private static readonly DependencyProperty BarNameProperty = DependencyProperty.Register("BarName",
       typeof(string), typeof(zedGraphCtrl), new PropertyMetadata(""));
        private string _BarName;
        [DefaultValue(""), Description("栏目名字"), Category("我的属性")]
        public string BarName
        {
            get { return _BarName; }
            set
            {
                SetAttrByName("BarName", value);
                _BarName = value;
                LoadData();
            }
        }
        #endregion

        
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
            DataTable dt = LoadDataDA.GetDataBySql(_ConnectString, sql);
            if (dt != null)
            {
                Bind(dt);
            }
        }
        private void Bind(DataTable dt)
        {  
            var _ColumnSeri = new ColumnSeries();
            _ColumnSeri.ItemsSource = dt.AsDataView();
            _ColumnSeri.Title = _BarName;
            
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
            _Chart.Axes.Clear();
            _Chart.Axes.Add(new CategoryAxis() { Title = _XaxisName, Orientation = AxisOrientation.X });
            _Chart.Axes.Add(new LinearAxis() { Title = _YaxisName, Orientation = AxisOrientation.Y });
            _Chart.Series.Add(_ColumnSeri);

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
    }
}
