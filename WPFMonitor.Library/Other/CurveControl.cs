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
    public class CurveControl : UserControl
    {
        Canvas _Canv=new Canvas();

        /// <summary>
        /// 竖着那一条
        /// </summary>
        Line _LineX = new Line();
        /// <summary>
        /// 横着那一条
        /// </summary>
        Line _LinY = new Line();
        //显示值
        TextBlock tbX = new TextBlock();
        TextBlock tbY = new TextBlock();

        private Color _LineColor;
        /// <summary>
        /// 线和字颜色
        /// </summary>
        public Color LineColor
        {
            get { return _LineColor; }
            set
            {
                _LineColor = value;
                SolidColorBrush scb = new SolidColorBrush(value);
                tbX.Foreground = scb;
                tbY.Foreground = scb;

                _LineX.Stroke = _LinY.Stroke = scb;
            }
        }
        public CurveControl()
        {
            this.Background = new SolidColorBrush();
            this.Content = _Canv;
            this.SizeChanged+=new SizeChangedEventHandler(CurveControl_SizeChanged);
            
            tbX.SetValue(Canvas.TopProperty, -15.0);
            _Canv.Children.Add(tbX);

            tbY.SetValue(Canvas.LeftProperty, -28.0);
            _Canv.Children.Add(tbY);

            _Canv.Children.Add(_LineX);
            _Canv.Children.Add(_LinY);

            _LineX.Stroke = _LinY.Stroke = new SolidColorBrush(Colors.Red);
            _LineX.StrokeThickness = _LinY.StrokeThickness =1;
        }
        public void SetXShow(string val)
        {
            tbX.Text = val;
        }

        public void SetYShow(string val)
        {
            tbY.Text = val;
        }
        public void SetPosition(Point P)
        {            
            _LineX.SetValue(Canvas.LeftProperty, P.X);

            _LinY.SetValue(Canvas.TopProperty, P.Y);

            tbX.SetValue(Canvas.LeftProperty, P.X);
            tbY.SetValue(Canvas.TopProperty, P.Y);
        }

        public void CurveControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Height;

            Paint();
        }

        private void Paint()
        {
            _LineX.X1 = 0;
            _LineX.X2 =0;
            _LineX.Y1 = 0;
            _LineX.Y2 = this.Height;

            _LinY.X1 = 0;
            _LinY.X2 = this.Width;
            _LinY.Y1 = 0;
            _LinY.Y2 = 0;   
        }
             
    }
}
