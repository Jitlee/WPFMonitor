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

namespace MonitorSystem.Other
{
    /// <summary>
    /// 实时曲线信息显示
    /// </summary>
    public class RealLineShow : UserControl
    {
        Border _Border = new Border();
        StackPanel _Stack = new StackPanel();
        RealTimeLineOR _LineOR;
        public RealLineShow(RealTimeLineOR obj)
        {
            Init();
            _LineOR = obj;
            ShowInfo();
        }
        public RealLineShow()
        {
            Init();
            TextBlock tb = new TextBlock();
            tb.Text = "所有曲线";
            _Stack.Children.Add(tb);
        }
        private void Init()
        {
            this.Content = _Border;
            this.MouseEnter += new MouseEventHandler(boder_MouseEnter);
            this.MouseLeave += new MouseEventHandler(boder_MouseLeave);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(RealLineShow_MouseLeftButtonUp);
            _Border.Child = _Stack;
        }

        TextBlock tbY = new TextBlock();
        TextBlock tbMinValue = new TextBlock();
        TextBlock tbMaxValue = new TextBlock();
        TextBlock tb = new TextBlock();
        private void ShowInfo()
        {
            tb.Text = _LineOR.LineInfo.LineName;            
            _Stack.Children.Add(tb);

            tbY.Text = string.Format("Y[{0}]", _LineOR.YValue);
            _Stack.Children.Add(tbY);

            tbMaxValue.Text = string.Format("Max[{0}]", _LineOR.MaxValue);            
            _Stack.Children.Add(tbMaxValue);

            tbMinValue.Text = string.Format("Min[{0}]", _LineOR.MinValue);            
            _Stack.Children.Add(tbMinValue);

            SetShowColor();
        }

        /// <summary>
        /// 设置显示对象的颜色
        /// </summary>
        public void SetShowColor()
        {
            SolidColorBrush colorB = new SolidColorBrush(Common1.StringToColor( _LineOR.LineInfo.LineColor));
            tb.Foreground = colorB;
            tbY.Foreground = colorB;
            tbMaxValue.Foreground = colorB;
            tbMinValue.Foreground = colorB;
        }

        public void SetDataValue(double Value)
        {
            tbY.Text = string.Format("Y[{0}]", Value);
            tbMinValue.Text = string.Format("Min[{0}]", _LineOR.MinValue);
            tbMaxValue.Text = string.Format("Max[{0}]", _LineOR.MaxValue);
        }

        protected void RealLineShow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RealLineArgs eL = new RealLineArgs();
            if (_LineOR == null)
            { eL = null; }
            else
            {
                eL.Name = _LineOR.LineInfo.LineName;
            }
            OnChangeLineShow(eL);
        }

        protected void boder_MouseEnter(object sender, MouseEventArgs e)
        {
            _Border.BorderBrush = new SolidColorBrush(Colors.Red);
            _Border.BorderThickness = new Thickness(1.0);
        }

        protected void boder_MouseLeave(object sender, MouseEventArgs e)
        {
            _Border.BorderBrush = new SolidColorBrush();
        }


        #region 事件
        public event EventHandler ChangeLineShow;
        /// <summary>
        /// 改变选中场景
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnChangeLineShow(RealLineArgs e)
        {
            if (ChangeLineShow != null)
            {
                ChangeLineShow(this, e);
            }
        }
        #endregion

    }

    public class RealLineArgs : EventArgs
    {
        string _Name;
        /// <summary>
        /// 场景对象 
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
    }

}
