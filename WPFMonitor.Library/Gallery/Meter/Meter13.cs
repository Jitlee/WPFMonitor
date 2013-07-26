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
using System.ComponentModel;
using MonitorSystem.ZTControls;
using WPFMonitor.Model.ZTControls;

namespace MonitorSystem.Gallery.Meter
{
    /// <summary>
    /// 仪表13
    /// </summary>
    public class Meter13 : MonitorControl
    {
        #region 重载

        public override void SetChannelValue(float fValue)
        {
            Value = (double)fValue;
        }

        public override void DesignMode()
        {
            if (!IsDesignMode)
            {
                AdornerLayer = new Adorner(this);
                AdornerLayer.Selected += OnSelected;
                AdornerLayer.IsLockScale = true;
                var menu = new ContextMenu();
                var menuItem = new MenuItem() { Header = "属性" };
                menuItem.Click += PropertyMenuItem_Click;
                menu.Items.Add(menuItem);
                AdornerLayer.SetValue(ContextMenuService.ContextMenuProperty, menu);
            }
        }

        private void OnSelected(object sender, EventArgs e)
        {
            if (null != Selected)
            {
                Selected(this, RoutedEventArgs.Empty);
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

        public override FrameworkElement GetRootControl()
        {
            return this;
        }

        public override event EventHandler Selected;
		
		public override event EventHandler Unselected;

		private void OnUnselected(object sender, EventArgs e)
		{
			if(null != Unselected)
			{
				Unselected(this, RoutedEventArgs.Empty);
			}
		}


        public override void SetPropertyValue()
        {
            foreach (t_ElementProperty pro in ListElementProp)
            {
                string name = pro.PropertyName.Trim();
                string value = pro.PropertyValue.Trim();
                if (name == "Text")
                {
                    Text = value;
                }
                else if (name == "Value")
                {
                    Value = double.Parse(value);
                }
                else if (name == "Maximum")
                {
                    Maximum = double.Parse(value);
                }
                else if (name == "Minimum")
                {
                    Minimum = double.Parse(value);
                }
                else if (name == "DecimalDigits")
                {
                    DecimalDigits = int.Parse(value);
                }
                else if (name == "MainScale")
                {
                    MainScale = int.Parse(value);
                }
                else if (name == "ViceScale")
                {
                    ViceScale = int.Parse(value);
                }
                else if (name == "NormalFrom")
                {
                    NormalFrom = double.Parse(value);
                }
                else if (name == "NormalTo")
                {
                    NormalTo = double.Parse(value);
                }
                else if (name == "Warring1From")
                {
                    Warring1From = double.Parse(value);
                }
                else if (name == "Warring1To")
                {
                    Warring1To = double.Parse(value);
                }
                else if (name == "Warring2From")
                {
                    Warring2From = double.Parse(value);
                }
                else if (name == "Warring2To")
                {
                    Warring2To = double.Parse(value);
                }
                else if (name == "Exception1From")
                {
                    Exception1From = double.Parse(value);
                }
                else if (name == "Exception1To")
                {
                    Exception1To = double.Parse(value);
                }
                else if (name == "Exception2From")
                {
                    Exception2From = double.Parse(value);
                }
                else if (name == "Exception2To")
                {
                    Exception2To = double.Parse(value);
                }
                else if (name == "LabelColor")
                {
                    LabelColor = Common1.StringToColor(value);
                }
                else if (name == "DialPlateBackColor")
                {
                    DialPlateBackColor = Common1.StringToColor(value);
                }
                else if (name == "CalibrationColor")
                {
                    CalibrationColor = Common1.StringToColor(value);
                }
                else if (name == "NormalColor")
                {
                    NormalColor = Common1.StringToColor(value);
                }
                else if (name == "WarringColor")
                {
                    WarringColor = Common1.StringToColor(value);
                }
                else if (name == "ExceptionColor")
                {
                    ExceptionColor = Common1.StringToColor(value);
                }
                else if (name == "ScaleColor")
                {
                    ScaleColor = Common1.StringToColor(value);
                }
            }
        }

        public override void SetCommonPropertyValue()
        {
            this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
            this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);

            this.Width = (double)ScreenElement.Width;
            this.Height = (double)ScreenElement.Height;

            //BackColor = Common1.StringToColor(ScreenElement.BackColor);
            //ForeColor = Common1.StringToColor(ScreenElement.ForeColor);
            //Transparent = ScreenElement.Transparent.Value;
        }

        private string[] _browsableProperties = new string[] { "Text", "Value", "Maximum", "Minimum", "DecimalDigits", "MainScale", "ViceScale",
            "NormalFrom",
            "NormalTo",
            "Warring1From",
            "Warring1To",
            "Warring2From",
            "Warring2To",
            "Exception1From",
            "Exception1To",
            "Exception2From",
            "Exception2To",
            "LabelColor",
            "DialPlateBackColor",
            "CalibrationColor",
            "NormalColor",
            "WarringColor",
            "ExceptionColor","FontFamily", "ForeColor","ScaleColor"};

        public override string[] BrowsableProperties
        {
            get { return _browsableProperties; }
            set { _browsableProperties = value; }
        }

        #endregion

        #region 属性
        #region 刻度颜色
        private static readonly DependencyProperty ScaleColorProperty =
            DependencyProperty.Register("ScaleColor",
            typeof(Color), typeof(Meter13), new PropertyMetadata(Colors.Blue));
        [DefaultValue(""), Description("刻度颜色"), Category("外观")]
        public Color ScaleColor
        {
            get { return (Color)this.GetValue(ScaleColorProperty); }
            set
            {
                this.SetValue(ScaleColorProperty, value);
                SetAttrByName("ScaleColor", value.ToString());

            }
        }
        #endregion
        #region 标签

        private static DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(Meter13), new PropertyMetadata("仪表", TextPropertyChanged));

        [DefaultValue("仪表"), Description("文本"), Category("标签")]
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
                SetAttrByName("Text", value.ToString());
            }
        }

        private static void TextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.Text_Changed((string)e.OldValue, (string)e.NewValue);
            }
        }

        private void Text_Changed(string oldValue, string newValue)
        {
            _label.Text = newValue;
        }

        #endregion

        #region 当前值

        private static DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(Meter13), new PropertyMetadata(0d, ValuePropertyChanged));


        [DefaultValue(0d), Description("当前值"), Category("里程")]
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set
            {
                SetValue(ValueProperty, value);

                SetAttrByName("Value", value.ToString());
            }
        }

        private static void ValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.Value_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void Value_Changed(double oldValue, double newValue)
        {
            PaintPoint(new Size(this.ActualWidth, this.ActualHeight));
        }

        #endregion

        #region 最大值

        private static DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(Meter13), new PropertyMetadata(100d, MaximumPropertyChanged));


        [DefaultValue(100d), Description("最大值"), Category("里程")]
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set
            {
                SetValue(MaximumProperty, value);
                SetAttrByName("Maximum", value.ToString());
            }
        }

        private static void MaximumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.Maximum_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void Maximum_Changed(double oldValue, double newValue)
        {
            PaintCalibration(new Size(this.ActualWidth, this.ActualHeight));
            PaintPoint(new Size(this.ActualWidth, this.ActualHeight));
        }

        #endregion

        #region 最小值

        private static DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(Meter13), new PropertyMetadata(0d, MinimumPropertyChanged));


        [DefaultValue(0d), Description("最小值"), Category("里程")]
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set
            {
                SetValue(MinimumProperty, value);
                SetAttrByName("Minimum", value.ToString());
            }
        }

        private static void MinimumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.Minimum_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void Minimum_Changed(double oldValue, double newValue)
        {
            PaintCalibration(new Size(this.ActualWidth, this.ActualHeight));
            PaintPoint(new Size(this.ActualWidth, this.ActualHeight));
        }

        #endregion

        #region 小数位数

        private static DependencyProperty DecimalDigitsProperty =
            DependencyProperty.Register("DecimalDigits", typeof(int), typeof(Meter13), new PropertyMetadata(0, DecimalDigitsPropertyChanged));

        [DefaultValue(0d), Description("小数位数(范围0-7)"), Category("里程")]
        public int DecimalDigits
        {
            get { return (int)GetValue(DecimalDigitsProperty); }
            set
            {
                SetValue(DecimalDigitsProperty, value);
                SetAttrByName("DecimalDigits", value.ToString());
            }
        }

        private static void DecimalDigitsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.DecimalDigits_Changed((int)e.OldValue, (int)e.NewValue);
            }
        }

        private void DecimalDigits_Changed(int oldValue, int newValue)
        {
            PaintCalibration(new Size(this.ActualWidth, this.ActualHeight));
        }

        #endregion

        #region 主刻度

        private static DependencyProperty MainScaleProperty =
            DependencyProperty.Register("MainScale", typeof(int), typeof(Meter13), new PropertyMetadata(10, MainScalePropertyChanged));

        [DefaultValue(0d), Description("主刻度"), Category("里程")]
        public int MainScale
        {
            get { return (int)GetValue(MainScaleProperty); }
            set 
            {
                SetValue(MainScaleProperty, value);
                SetAttrByName("MainScale", value.ToString());
            }
        }

        private static void MainScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.MainScale_Changed((int)e.OldValue, (int)e.NewValue);
            }
        }

        private void MainScale_Changed(int oldValue, int newValue)
        {
            PaintCalibration(new Size(this.ActualWidth, this.ActualHeight));
        }

        #endregion

        #region 副刻度

        private static DependencyProperty ViceScaleProperty =
            DependencyProperty.Register("ViceScale", typeof(int), typeof(Meter13), new PropertyMetadata(1, ViceScalePropertyChanged));

        [DefaultValue(0d), Description("副刻度"), Category("里程")]
        public int ViceScale
        {
            get { return (int)GetValue(ViceScaleProperty); }
            set 
            {
                SetValue(ViceScaleProperty, value);
                SetAttrByName("ViceScale", value.ToString());
            }
        }

        private static void ViceScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.ViceScale_Changed((int)e.OldValue, (int)e.NewValue);
            }
        }

        private void ViceScale_Changed(int oldValue, int newValue)
        {
            PaintCalibration(new Size(this.ActualWidth, this.ActualHeight));
        }

        #endregion

        #region 正常区间从

        private static DependencyProperty NormalFromProperty =
            DependencyProperty.Register("NormalFrom", typeof(double), typeof(Meter13), new PropertyMetadata(40d, NormalFromPropertyChanged));


        [DefaultValue(40d), Description("正常区间从"), Category("提醒标志")]
        public double NormalFrom
        {
            get { return (double)GetValue(NormalFromProperty); }
            set
            {
                SetValue(NormalFromProperty, value);
                SetAttrByName("NormalFrom", value.ToString());
            }
        }

        private static void NormalFromPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.NormalFrom_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void NormalFrom_Changed(double oldValue, double newValue)
        {
            PaintAlert(new Size(this.ActualWidth, this.ActualHeight), _normalPath, NormalFrom, NormalTo);
        }

        #endregion

        #region 正常区间到

        private static DependencyProperty NormalToProperty =
            DependencyProperty.Register("NormalTo", typeof(double), typeof(Meter13), new PropertyMetadata(60d, NormalToPropertyChanged));


        [DefaultValue(40d), Description("正常区间到"), Category("提醒标志")]
        public double NormalTo
        {
            get { return (double)GetValue(NormalToProperty); }
            set
            {
                SetValue(NormalToProperty, value);
                SetAttrByName("NormalTo", value.ToString());
            }
        }

        private static void NormalToPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.NormalTo_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void NormalTo_Changed(double oldValue, double newValue)
        {
            PaintAlert(new Size(this.ActualWidth, this.ActualHeight), _normalPath, NormalFrom, NormalTo);
        }

        #endregion

        #region 警告区间1从

        private static DependencyProperty Warring1FromProperty =
            DependencyProperty.Register("Warring1From", typeof(double), typeof(Meter13), new PropertyMetadata(20d, Warring1FromPropertyChanged));


        [DefaultValue(40d), Description("警告区间1从"), Category("提醒标志")]
        public double Warring1From
        {
            get { return (double)GetValue(Warring1FromProperty); }
            set
            {
                SetValue(Warring1FromProperty, value);
                SetAttrByName("Warring1From", value.ToString());
            }
        }

        private static void Warring1FromPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.Warring1From_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void Warring1From_Changed(double oldValue, double newValue)
        {
            PaintAlert(new Size(this.ActualWidth, this.ActualHeight), _warring1Path, Warring1From, Warring1To);
        }

        #endregion

        #region 警告区间1到

        private static DependencyProperty Warring1ToProperty =
            DependencyProperty.Register("Warring1To", typeof(double), typeof(Meter13), new PropertyMetadata(40d, Warring1ToPropertyChanged));


        [DefaultValue(40d), Description("警告区间1到"), Category("提醒标志")]
        public double Warring1To
        {
            get { return (double)GetValue(Warring1ToProperty); }
            set
            {
                SetValue(Warring1ToProperty, value);
                SetAttrByName("Warring1To", value.ToString());
            }
        }

        private static void Warring1ToPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.Warring1To_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void Warring1To_Changed(double oldValue, double newValue)
        {
            PaintAlert(new Size(this.ActualWidth, this.ActualHeight), _warring1Path, Warring1From, Warring1To);
        }

        #endregion

        #region 警告区间2从

        private static DependencyProperty Warring2FromProperty =
            DependencyProperty.Register("Warring2From", typeof(double), typeof(Meter13), new PropertyMetadata(60d, Warring2FromPropertyChanged));


        [DefaultValue(40d), Description("警告区间2从"), Category("提醒标志")]
        public double Warring2From
        {
            get { return (double)GetValue(Warring2FromProperty); }
            set
            {
                SetValue(Warring2FromProperty, value);
                SetAttrByName("Warring2From", value.ToString());
            }
        }

        private static void Warring2FromPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.Warring2From_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void Warring2From_Changed(double oldValue, double newValue)
        {
            PaintAlert(new Size(this.ActualWidth, this.ActualHeight), _warring2Path, Warring2From, Warring2To);
        }

        #endregion

        #region 警告区间2到

        private static DependencyProperty Warring2ToProperty =
            DependencyProperty.Register("Warring2To", typeof(double), typeof(Meter13), new PropertyMetadata(80d, Warring2ToPropertyChanged));


        [DefaultValue(40d), Description("警告区间2到"), Category("提醒标志")]
        public double Warring2To
        {
            get { return (double)GetValue(Warring2ToProperty); }
            set
            {
                SetValue(Warring2ToProperty, value);
                SetAttrByName("Warring2To", value.ToString());
            }
        }

        private static void Warring2ToPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.Warring2To_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void Warring2To_Changed(double oldValue, double newValue)
        {
            PaintAlert(new Size(this.ActualWidth, this.ActualHeight), _warring2Path, Warring2From, Warring2To);
        }

        #endregion

        #region 异常区间1从

        private static DependencyProperty Exception1FromProperty =
            DependencyProperty.Register("Exception1From", typeof(double), typeof(Meter13), new PropertyMetadata(0d, Exception1FromPropertyChanged));


        [DefaultValue(40d), Description("异常区间1从"), Category("提醒标志")]
        public double Exception1From
        {
            get { return (double)GetValue(Exception1FromProperty); }
            set
            {
                SetValue(Exception1FromProperty, value);
                SetAttrByName("Exception1From", value.ToString());
            }
        }

        private static void Exception1FromPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.Exception1From_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void Exception1From_Changed(double oldValue, double newValue)
        {
            PaintAlert(new Size(this.ActualWidth, this.ActualHeight), _exception1Path, Exception1From, Exception1To);
        }

        #endregion

        #region 异常区间1到

        private static DependencyProperty Exception1ToProperty =
            DependencyProperty.Register("Exception1To", typeof(double), typeof(Meter13), new PropertyMetadata(20d, Exception1ToPropertyChanged));


        [DefaultValue(40d), Description("异常区间1到"), Category("提醒标志")]
        public double Exception1To
        {
            get { return (double)GetValue(Exception1ToProperty); }
            set
            {
                SetValue(Exception1ToProperty, value);
                SetAttrByName("Exception1To", value.ToString());
            }
        }

        private static void Exception1ToPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.Exception1To_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void Exception1To_Changed(double oldValue, double newValue)
        {
            PaintAlert(new Size(this.ActualWidth, this.ActualHeight), _exception1Path, Exception1From, Exception1To);
        }

        #endregion

        #region 异常区间2从

        private static DependencyProperty Exception2FromProperty =
            DependencyProperty.Register("Exception2From", typeof(double), typeof(Meter13), new PropertyMetadata(80d, Exception2FromPropertyChanged));


        [DefaultValue(40d), Description("异常区间2从"), Category("提醒标志")]
        public double Exception2From
        {
            get { return (double)GetValue(Exception2FromProperty); }
            set
            {
                SetValue(Exception2FromProperty, value);
                SetAttrByName("Exception2From", value.ToString());
            }
        }

        private static void Exception2FromPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.Exception2From_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void Exception2From_Changed(double oldValue, double newValue)
        {
            PaintAlert(new Size(this.ActualWidth, this.ActualHeight), _exception2Path, Exception2From, Exception2To);
        }

        #endregion

        #region 异常区间2到

        private static DependencyProperty Exception2ToProperty =
            DependencyProperty.Register("Exception2To", typeof(double), typeof(Meter13), new PropertyMetadata(100d, Exception2ToPropertyChanged));


        [DefaultValue(40d), Description("异常区间2到"), Category("提醒标志")]
        public double Exception2To
        {
            get { return (double)GetValue(Exception2ToProperty); }
            set
            {
                SetValue(Exception2ToProperty, value);
                SetAttrByName("Exception2To", value.ToString());
            }
        }

        private static void Exception2ToPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.Exception2To_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void Exception2To_Changed(double oldValue, double newValue)
        {
            PaintAlert(new Size(this.ActualWidth, this.ActualHeight), _exception2Path, Exception2From, Exception2To);
        }

        #endregion

        #region 标签颜色

        private static DependencyProperty LabelColorProperty =
            DependencyProperty.Register("LabelColor", typeof(Color), typeof(Meter13), new PropertyMetadata(Colors.Black, LabelColorPropertyChanged));

        [Description("标签颜色"), Category("画笔")]
        public Color LabelColor
        {
            get { return (Color)GetValue(LabelColorProperty); }
            set
            {
                SetValue(LabelColorProperty, value);
                SetAttrByName("LabelColor", value.ToString());
            }
        }

        private static void LabelColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.LabelColor_Changed((Color)e.OldValue, (Color)e.NewValue);
            }
        }

        private void LabelColor_Changed(Color oldValue, Color newValue)
        {
            _label.Foreground = new SolidColorBrush(newValue);
        }

        #endregion

        #region 表盘颜色

        private static DependencyProperty DialPlateBackColorProperty =
            DependencyProperty.Register("DialPlateBackColor", typeof(Color), typeof(Meter13), new PropertyMetadata(Colors.White, DialPlateBackColorPropertyChanged));

        [Description("表盘颜色"), Category("画笔")]
        public Color DialPlateBackColor
        {
            get { return (Color)GetValue(DialPlateBackColorProperty); }
            set
            {
                SetValue(DialPlateBackColorProperty, value);
                SetAttrByName("DialPlateBackColor", value.ToString());
            }
        }

        private static void DialPlateBackColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.DialPlateBackColor_Changed((Color)e.OldValue, (Color)e.NewValue);
            }
        }

        private void DialPlateBackColor_Changed(Color oldValue, Color newValue)
        {
            _dialPlateEllipse.Fill = new SolidColorBrush(newValue);
        }

        #endregion

        #region 刻度文本颜色

        private static DependencyProperty CalibrationColorProperty =
            DependencyProperty.Register("CalibrationColor", typeof(Color), typeof(Meter13), new PropertyMetadata(Colors.Blue, CalibrationColorPropertyChanged));

        [Description("刻度文本颜色"), Category("画笔")]
        public Color CalibrationColor
        {
            get { return (Color)GetValue(CalibrationColorProperty); }
            set
            {
                SetValue(CalibrationColorProperty, value);
                SetAttrByName("CalibrationColor", value.ToString());
            }
        }

        private static void CalibrationColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.CalibrationColor_Changed((Color)e.OldValue, (Color)e.NewValue);
            }
        }

        private void CalibrationColor_Changed(Color oldValue, Color newValue)
        {
            PaintCalibration(new Size(this.ActualWidth, this.ActualHeight));
        }

        #endregion

        #region 正常区间颜色

        private static DependencyProperty NormalColorProperty =
            DependencyProperty.Register("NormalColor", typeof(Color), typeof(Meter13), new PropertyMetadata(Color.FromArgb(0xff, 0x00, 0xE7, 0x00), NormalColorPropertyChanged));

        [Description("正常区间颜色"), Category("画笔")]
        public Color NormalColor
        {
            get { return (Color)GetValue(NormalColorProperty); }
            set
            {
                SetValue(NormalColorProperty, value);
                SetAttrByName("NormalColor", value.ToString());
            }
        }

        private static void NormalColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.NormalColor_Changed((Color)e.OldValue, (Color)e.NewValue);
            }
        }

        private void NormalColor_Changed(Color oldValue, Color newValue)
        {
            _normalPath.Fill = new SolidColorBrush(newValue);
        }

        #endregion

        #region 警告区间颜色

        private static DependencyProperty WarringColorProperty =
            DependencyProperty.Register("WarringColor", typeof(Color), typeof(Meter13), new PropertyMetadata(Colors.Yellow, WarringColorPropertyChanged));

        [Description("警告区间颜色"), Category("画笔")]
        public Color WarringColor
        {
            get { return (Color)GetValue(WarringColorProperty); }
            set
            {
                SetValue(WarringColorProperty, value);
                SetAttrByName("WarringColor", value.ToString());
            }
        }

        private static void WarringColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.WarringColor_Changed((Color)e.OldValue, (Color)e.NewValue);
            }
        }

        private void WarringColor_Changed(Color oldValue, Color newValue)
        {
            _warring1Path.Fill = _warring2Path.Fill = new SolidColorBrush(newValue);
        }

        #endregion

        #region 异常区间颜色

        private static DependencyProperty ExceptionColorProperty =
            DependencyProperty.Register("ExceptionColor", typeof(Color), typeof(Meter13), new PropertyMetadata(Colors.Red, ExceptionColorPropertyChanged));

        [Description("异常区间颜色"), Category("画笔")]
        public Color ExceptionColor
        {
            get { return (Color)GetValue(ExceptionColorProperty); }
            set
            {
                SetValue(ExceptionColorProperty, value);
                SetAttrByName("ExceptionColor", value.ToString());
            }
        }

        private static void ExceptionColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter13;
            if (null != element)
            {
                element.ExceptionColor_Changed((Color)e.OldValue, (Color)e.NewValue);
            }
        }

        private void ExceptionColor_Changed(Color oldValue, Color newValue)
        {
            _exception1Path.Fill = _exception2Path.Fill = new SolidColorBrush(newValue);
        }

        #endregion

        #endregion

        #region 变量

        private Canvas _canvas = new Canvas();
        private Canvas _calibrationCanvas = new Canvas();
        private Line _pointLine = new Line();
        private TextBlock _label = new TextBlock() { TextAlignment = TextAlignment.Center };
        
        private Path _normalPath = new Path();
        private Path _warring1Path = new Path();
        private Path _warring2Path = new Path();
        private Path _exception1Path = new Path();
        private Path _exception2Path = new Path();
        private Ellipse _dialPlateEllipse = new Ellipse();

        #endregion

        #region 构造方法

        public Meter13()
        {
            var grid = new Grid();
            grid.Children.Add(_canvas);
            grid.Children.Add(_calibrationCanvas);

            this.Content = grid;

            _label.Text = Text;
            _label.Foreground = new SolidColorBrush(LabelColor);

            _pointLine.Stroke = new SolidColorBrush(Colors.Red);
            _pointLine.StrokeThickness = 3d;

            _normalPath.Fill = new SolidColorBrush(NormalColor);
            _normalPath.Stroke = new SolidColorBrush(Colors.Black);
            _normalPath.StrokeThickness = 1d;

            _warring1Path.Fill = new SolidColorBrush(WarringColor);
            _warring1Path.Stroke = new SolidColorBrush(Colors.Black);
            _warring1Path.StrokeThickness = 1d;

            _warring2Path.Fill = new SolidColorBrush(WarringColor);
            _warring2Path.Stroke = new SolidColorBrush(Colors.Black);
            _warring2Path.StrokeThickness = 1d;

            _exception1Path.Fill = new SolidColorBrush(ExceptionColor);
            _exception1Path.Stroke = new SolidColorBrush(Colors.Black);
            _exception1Path.StrokeThickness = 1d;

            _exception2Path.Fill = new SolidColorBrush(ExceptionColor);
            _exception2Path.Stroke = new SolidColorBrush(Colors.Black);
            _exception2Path.StrokeThickness = 1d;

            _canvas.Background = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0x77, 0x77));

            _dialPlateEllipse.Fill = new SolidColorBrush(DialPlateBackColor);
            _dialPlateEllipse.Stroke = new SolidColorBrush(Colors.Black);
            _dialPlateEllipse.StrokeThickness = 1d;
            this.SizeChanged += Meter_SizeChanged;
        }

        #endregion

        #region 私有方法

        private void Meter_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Width;
            Paint(new Size(this.ActualWidth, this.ActualHeight));
        }

        private void Paint(Size size)
        {
            try
            {
                var width = size.Width;
                _canvas.Children.Clear();

                PaintBorder(size);

                _canvas.Children.Add(_normalPath);
                PaintAlert(size, _normalPath, NormalFrom, NormalTo);

                _canvas.Children.Add(_warring1Path);
                PaintAlert(size, _warring1Path, Warring1From, Warring1To);

                _canvas.Children.Add(_warring2Path);
                PaintAlert(size, _warring2Path, Warring2From, Warring2To);

                _canvas.Children.Add(_exception1Path);
                PaintAlert(size, _exception1Path, Exception1From, Exception1To);

                _canvas.Children.Add(_exception2Path);
                PaintAlert(size, _exception2Path, Exception2From, Exception2To);

                _dialPlateEllipse.SetValue(Canvas.LeftProperty, width * 106d / 638d);
                _dialPlateEllipse.SetValue(Canvas.TopProperty, width * 106d / 638d);
                _dialPlateEllipse.Width = _dialPlateEllipse.Height = width * 426d / 638d;
                _canvas.Children.Add(_dialPlateEllipse);

                var ellipse1 = new Ellipse();
                ellipse1.Width = ellipse1.Height = width * 32d / 638d;
                ellipse1.SetValue(Canvas.LeftProperty, width * 303d / 638d);
                ellipse1.SetValue(Canvas.TopProperty, width * 303d / 638d);
                ellipse1.Fill = new SolidColorBrush(Colors.Black);
                _canvas.Children.Add(ellipse1);

                PaintCalibration(size);

                _label.Width = width;
                _label.FontSize = width * 50d / 638d;
                _label.SetValue(Canvas.TopProperty, width * 381d / 638d);
                _canvas.Children.Add(_label);

                _pointLine.X1 = _pointLine.Y1 = width * 0.5d;

                _canvas.Children.Add(_pointLine);
            }
            catch { }
        }

        private void PaintAlert(Size size, Path path, double from, double to)
        {
            var width = size.Width;
            var normalFrom = from;
            var normalTo = to;
            var maximum = Maximum;
            var minimum = Minimum;
            var mainScale = MainScale;
            var viceScale = ViceScale;
            var decimalDigits = DecimalDigits;  // 小数点位
            var beginAngle = 1.75d * Math.PI;
            var angleFrom = beginAngle - 1.5d * Math.PI * normalFrom / (maximum - minimum);
            var angleTo = beginAngle - 1.5d * Math.PI * normalTo / (maximum - minimum);

            var pathFigure = new PathFigure();
            pathFigure.StartPoint = new Point(Math.Sin(angleFrom) * width * 249d / 638d + width * 0.5d, Math.Cos(angleFrom) * width * 249d / 638d + width * 0.5d);
            pathFigure.IsClosed = true;
            pathFigure.IsFilled = true;

            var arcSegment1 = new ArcSegment();
            arcSegment1.Point = new Point(Math.Sin(angleTo) * width * 249d / 638d + width * 0.5d, Math.Cos(angleTo) * width * 249d / 638d + width * 0.5d);
            arcSegment1.Size = new Size(width * 249d / 638d, width * 249d / 638d);
            arcSegment1.SweepDirection = SweepDirection.Clockwise;
            pathFigure.Segments.Add(arcSegment1);

            var lineSegment = new LineSegment();
            lineSegment.Point = new Point(Math.Sin(angleTo) * width * 214d / 638d + width * 0.5d, Math.Cos(angleTo) * width * 214d / 638d + width * 0.5d);
            pathFigure.Segments.Add(lineSegment);

            var arcSegment2 = new ArcSegment();
            arcSegment2.Point = new Point(Math.Sin(angleFrom) * width * 214d / 638d + width * 0.5d, Math.Cos(angleFrom) * width * 214d / 638d + width * 0.5d);
            arcSegment2.Size = new Size(width * 214d / 638d, width * 214d / 638d);
            arcSegment2.SweepDirection = SweepDirection.Counterclockwise;
            pathFigure.Segments.Add(arcSegment2);

            var pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);

            path.Data = pathGeometry;
        }

        private void PaintBorder(Size size)
        {
            var width = size.Width;

            var brush1 = new SolidColorBrush(Color.FromArgb(0xff, 0xE3, 0xE3, 0xE3));
            var brush2 = new SolidColorBrush(Colors.White);
            var brush3 = new SolidColorBrush(Color.FromArgb(0xff, 0x69, 0x69, 0x69));
            var brush4 = new SolidColorBrush(Color.FromArgb(0xff, 0xA0, 0xA0, 0xA0));
            var brush5 = new SolidColorBrush(Color.FromArgb(0xff, 0xCE, 0xCB, 0xC5));

            var rectangle = new Rectangle();
            rectangle.SetValue(Canvas.LeftProperty, width * 22d / 638d);
            rectangle.SetValue(Canvas.TopProperty, width * 22d / 638d);
            rectangle.Width = rectangle.Height = width * 594d / 638d;
            rectangle.Fill = brush5;
            _canvas.Children.Add(rectangle);

            // 1
            _canvas.Children.Add(new Line()
            {
                X2 = width,
                Stroke = brush1,
                StrokeThickness = 1d
            });

            // 2
            _canvas.Children.Add(new Line()
            {
                Y2 = width,
                Stroke = brush1,
                StrokeThickness = 1d
            });

            // 3
            _canvas.Children.Add(new Line()
            {
                X1 = 1d,
                X2 = width - 1d,
                Y1 = 1d,
                Y2 = 1d,
                Stroke = brush2,
                StrokeThickness = 1d
            });

            // 4
            _canvas.Children.Add(new Line()
            {
                X1 = 1d,
                X2 = 1d,
                Y1 = 1d,
                Y2 = width - 1d,
                Stroke = brush2,
                StrokeThickness = 1d,
            });

            // 5
            _canvas.Children.Add(new Line()
            {
                X2 = width,
                Y1 = width,
                Y2 = width,
                Stroke = brush3,
                StrokeThickness = 1d
            });

            // 6
            _canvas.Children.Add(new Line()
            {
                X1 = width,
                X2 = width,
                Y2 = width,
                Stroke = brush3,
                StrokeThickness = 1d
            });

            // 7
            _canvas.Children.Add(new Line()
            {
                X1 = 1d,
                X2 = width - 1d,
                Y1 = width - 1d,
                Y2 = width - 1d,
                Stroke = brush4,
                StrokeThickness = 1d
            });

            // 8
            _canvas.Children.Add(new Line()
            {
                X1 = width - 1d,
                X2 = width - 1d,
                Y1 = 1d,
                Y2 = width - 1d,
                Stroke = brush4,
                StrokeThickness = 1d
            });

            // 9
            _canvas.Children.Add(new Line()
            {
                X1 = width * 22d / 638d,
                X2 = width * 616d / 638d,
                Y1 = width * 22d / 638d,
                Y2 = width * 22d / 638d,
                Stroke = brush4,
                StrokeThickness = 1d
            });

            // 10
            _canvas.Children.Add(new Line()
            {
                X1 = width * 22d / 638d,
                X2 = width * 22d / 638d,
                Y1 = width * 22d / 638d,
                Y2 = width * 616d / 638d,
                Stroke = brush4,
                StrokeThickness = 1d
            });

            // 11
            _canvas.Children.Add(new Line()
            {
                X1 = width * 23d / 638d,
                X2 = width * 615d / 638d,
                Y1 = width * 23d / 638d,
                Y2 = width * 23d / 638d,
                Stroke = brush3,
                StrokeThickness = 1d
            });

            // 12
            _canvas.Children.Add(new Line()
            {
                X1 = width * 23d / 638d,
                X2 = width * 23d / 638d,
                Y1 = width * 23d / 638d,
                Y2 = width * 615d / 638d,
                Stroke = brush3,
                StrokeThickness = 1d,
            });

            // 13
            _canvas.Children.Add(new Line()
            {
                X1 = width * 616d / 638d,
                X2 = width * 616d / 638d,
                Y1 = width * 22d / 638d,
                Y2 = width * 616d / 638d,
                Stroke = brush2,
                StrokeThickness = 1d
            });

            // 14
            _canvas.Children.Add(new Line()
            {
                X1 = width * 22d / 638d,
                X2 = width * 616d / 638d,
                Y1 = width * 616d / 638d,
                Y2 = width * 616d / 638d,
                Stroke = brush2,
                StrokeThickness = 1d
            });

            // 15
            _canvas.Children.Add(new Line()
            {
                X1 = width * 615d / 638d,
                X2 = width * 615d / 638d,
                Y1 = width * 23d / 638d,
                Y2 = width * 615d / 638d,
                Stroke = brush1,
                StrokeThickness = 1d
            });

            // 16
            _canvas.Children.Add(new Line()
            {
                X1 = width * 23d / 638d,
                X2 = width * 615d / 638d,
                Y1 = width * 615d / 638d,
                Y2 = width * 615d / 638d,
                Stroke = brush1,
                StrokeThickness = 1d
            });
        }

        /// <summary>
        /// 绘制刻度
        /// </summary>
        /// <param name="size"></param>
        private void PaintCalibration(Size size)
        {
            try
            {
                var width = size.Width;

                var maximum = Maximum;
                var minimum = Minimum;
                var mainScale = MainScale;
                var viceScale = ViceScale + 1;
                var decimalDigits = DecimalDigits;  // 小数点位
                var beginAngle = 1.75d * Math.PI;
                var avgMainAngle = 1.5d * Math.PI / mainScale;
                var avgViceAngle = avgMainAngle / viceScale;
                var avg = (maximum - minimum) / (double)mainScale;
                var fontSize = width * 27d / 638d;
                var foreground = new SolidColorBrush(CalibrationColor);

                if (decimalDigits < 0)
                {
                    decimalDigits = 0;
                }
                else if (decimalDigits > 7)
                {
                    decimalDigits = 7;
                }

                _calibrationCanvas.Children.Clear();

                var brush = new SolidColorBrush(Colors.Black);
                for (int i = 0; i <= mainScale; i++)
                {
                    var mainAngle = beginAngle - avgMainAngle * i;
                    var mainLine = new Line();
                    mainLine.Stroke = brush;
                    mainLine.StrokeThickness = 2d;
                    mainLine.X1 = width * Math.Sin(mainAngle) * 242d / 638d + width * 0.5d;
                    mainLine.Y1 = width * Math.Cos(mainAngle) * 242d / 638d + width * 0.5d;
                    mainLine.X2 = width * Math.Sin(mainAngle) * 219d / 638d + width * 0.5d;
                    mainLine.Y2 = width * Math.Cos(mainAngle) * 219d / 638d + width * 0.5d;
                    _calibrationCanvas.Children.Add(mainLine);

                    for (int j = 1; j < viceScale && i < mainScale; j++)
                    {
                        var viceAngle = mainAngle - j * avgViceAngle;
                        var viceLine = new Line();
                        viceLine.Stroke = brush;
                        viceLine.StrokeThickness = 1d;
                        viceLine.X1 = width * Math.Sin(viceAngle) * 242d / 638d + width * 0.5d;
                        viceLine.Y1 = width * Math.Cos(viceAngle) * 242d / 638d + width * 0.5d;
                        viceLine.X2 = width * Math.Sin(viceAngle) * 219d / 638d + width * 0.5d;
                        viceLine.Y2 = width * Math.Cos(viceAngle) * 219d / 638d + width * 0.5d;
                        _calibrationCanvas.Children.Add(viceLine);
                    }

                    var text = new TextBlock();
                    text.FontSize = fontSize;
                    text.Foreground = foreground;
                    text.Text = Math.Round(minimum + i * avg, decimalDigits).ToString();
                    text.SetValue(Canvas.LeftProperty, width * Math.Sin(mainAngle) * 263d / 638d - text.ActualWidth / 2d + width * 0.5d);
                    text.SetValue(Canvas.TopProperty, width * Math.Cos(mainAngle) * 263d / 638d - text.ActualHeight / 2d + width * 0.5d);
                    _calibrationCanvas.Children.Add(text);
                }

                PaintPoint(size);
            }
            catch { }
        }

        /// <summary>
        /// 绘制当前值
        /// </summary>
        private void PaintPoint(Size size)
        {
            //_valueCanvas.Width = size.Width;
            //_valueCanvas.Height = size.Height;
            //_valueCanvas.Children.Clear();

            try
            {
                var width = size.Width;
                var height = size.Height;

                var value = Value;
                var maximum = Maximum;
                var minimum = Minimum;
                if (value < minimum)
                {
                    value = minimum;
                }
                if (value > maximum)
                {
                    value = maximum;
                }
                value -= minimum;
                var mainScale = MainScale;
                var viceScale = ViceScale;
                var decimalDigits = DecimalDigits;  // 小数点位
                var beginAngle = 1.75d * Math.PI;
                var angle = beginAngle - 1.5d * Math.PI * value / (maximum - minimum);

                _pointLine.X2 = Math.Sin(angle) * height * 205d / 638d + width * 0.5d;
                _pointLine.Y2 = Math.Cos(angle) * height * 205d / 638d + width * 0.5d;
            }
            catch { }
        }

        #endregion
    }
}
