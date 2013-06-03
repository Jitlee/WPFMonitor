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
    public class JTControl : UserControl
    {
        Border b=new Border();
        Canvas _canvas = new Canvas();
        public event EventHandler Click;

        
        public JTControl()
        {
            b.Child = _canvas;
            b.BorderBrush = new SolidColorBrush(Colors.White);
            b.BorderThickness =new Thickness ( 0.5);
            this.Content = b;
            
            this.SizeChanged+=new SizeChangedEventHandler(JTControl_SizeChanged);
            _canvas.Background = new SolidColorBrush(Common1.StringToColor("#FFD5D5FF"));
            
            this.MouseEnter+=new MouseEventHandler(JTControl_MouseEnter);
            this.MouseLeave +=new MouseEventHandler(JTControl_MouseLeave);

            this.MouseLeftButtonDown+=new MouseButtonEventHandler(JTControl_MouseLeftButtonDown);
            this.MouseLeftButtonUp+=new MouseButtonEventHandler(JTControl_MouseLeftButtonUp);
        }

        public SolidColorBrush BGColor
        {
            set { _canvas.Background = value; }
        }

        public void JTControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        public void JTControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Click != null)
                Click(this, null);
        }

        public void JTControl_MouseLeave(object sender, MouseEventArgs e)
        {
            b.BorderBrush = new SolidColorBrush(Colors.White);
        }
        
        public void JTControl_MouseEnter(object sender, MouseEventArgs e)
        {
            b.BorderBrush = new SolidColorBrush(Colors.Black);
        }


        public void JTControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Width = e.NewSize.Width;
            Height = e.NewSize.Height;
            PintJT();
        }

        private int _JTType = 0;
        /// <summary>
        /// 方向类型
        /// </summary>
        public int JTType
        {
            get { return _JTType; }
            set { _JTType = value;
                PintJT();
            }
        }
        Polyline pl = new Polyline();
        Polyline pl1 = new Polyline();
        private void PintJT()
        {
            if (_JTType == 4)
            {
                return;
            }
            pl.Stroke = new SolidColorBrush(Colors.Black);
            pl.StrokeThickness = 1.0;
            pl.Points.Clear();

            pl1.Stroke = new SolidColorBrush(Colors.Black);
            pl1.StrokeThickness = 1.0;
            pl1.Points.Clear();
            if (_JTType == 0)//Y,两向下
            {
                pl.Points.Add(new Point(this.Width * 0.1, this.Height * 0.2));
                pl.Points.Add(new Point(this.Width * 0.5, this.Height * 0.5));
                pl.Points.Add(new Point(this.Width * 0.8, this.Height * 0.2));

                pl1.Points.Add(new Point(this.Width * 0.1, this.Height * 0.5));
                pl1.Points.Add(new Point(this.Width * 0.5, this.Height * 0.8));
                pl1.Points.Add(new Point(this.Width * 0.8, this.Height * 0.5));

                _canvas.Children.Clear();
                _canvas.Children.Add(pl); _canvas.Children.Add(pl1);
            }
            else  if (_JTType == 1)//Y一向下
            {
                pl.Points.Add(new Point(this.Width * 0.2, this.Height * 0.3));
                pl.Points.Add(new Point(this.Width * 0.5, this.Height * 0.8));
                pl.Points.Add(new Point(this.Width * 0.8, this.Height * 0.3));
                _canvas.Children.Clear();
                _canvas.Children.Add(pl);

            }
            else if (_JTType == 2)//Y一向上
            {
                pl.Points.Add(new Point(this.Width * 0.1, this.Height * 0.8));
                pl.Points.Add(new Point(this.Width * 0.5, this.Height * 0.2));
                pl.Points.Add(new Point(this.Width * 0.8, this.Height * 0.8));

                _canvas.Children.Clear();
                _canvas.Children.Add(pl);

            }
            else if (_JTType == 3) //Y两向上
            {
                pl.Points.Add(new Point(this.Width * 0.1, this.Height * 0.5));
                pl.Points.Add(new Point(this.Width * 0.5, this.Height * 0.2));
                pl.Points.Add(new Point(this.Width * 0.8, this.Height * 0.5));

                pl1.Points.Add(new Point(this.Width * 0.1, this.Height * 0.8));
                pl1.Points.Add(new Point(this.Width * 0.5, this.Height * 0.5));
                pl1.Points.Add(new Point(this.Width * 0.8, this.Height * 0.8));

                _canvas.Children.Clear();
                _canvas.Children.Add(pl); _canvas.Children.Add(pl1);
            }
           
            else if (_JTType == 5)//X,两向左
            {
                pl.Points.Add(new Point(this.Width * 0.5, this.Height * 0.2));
                pl.Points.Add(new Point(this.Width * 0.2, this.Height * 0.5));
                pl.Points.Add(new Point(this.Width * 0.5, this.Height * 0.8));

                pl1.Points.Add(new Point(this.Width * 0.8, this.Height * 0.2));
                pl1.Points.Add(new Point(this.Width * 0.5, this.Height * 0.5));
                pl1.Points.Add(new Point(this.Width * 0.8, this.Height * 0.8));

                _canvas.Children.Clear();
                _canvas.Children.Add(pl); _canvas.Children.Add(pl1);
            }

            else
                if (_JTType == 6)//X一向左
                {
                    pl.Points.Add(new Point(this.Width * 0.7, this.Height * 0.2));
                    pl.Points.Add(new Point(this.Width * 0.2, this.Height * 0.5));
                    pl.Points.Add(new Point(this.Width * 0.7, this.Height * 0.8));
                    _canvas.Children.Clear();
                    _canvas.Children.Add(pl);

                }
                else if (_JTType == 7)//Y一向上
                {
                    pl.Points.Add(new Point(this.Width * 0.3, this.Height * 0.2));
                    pl.Points.Add(new Point(this.Width * 0.8, this.Height * 0.5));
                    pl.Points.Add(new Point(this.Width * 0.3, this.Height * 0.8));

                    _canvas.Children.Clear();
                    _canvas.Children.Add(pl);

                }
                else if (_JTType == 8) //X两个向右
                {
                    pl.Points.Add(new Point(this.Width * 0.2, this.Height * 0.2));
                    pl.Points.Add(new Point(this.Width * 0.5, this.Height * 0.5));
                    pl.Points.Add(new Point(this.Width * 0.2, this.Height * 0.8));

                    pl1.Points.Add(new Point(this.Width * 0.5, this.Height * 0.2));
                    pl1.Points.Add(new Point(this.Width * 0.8, this.Height * 0.5));
                    pl1.Points.Add(new Point(this.Width * 0.5, this.Height * 0.8));

                    _canvas.Children.Clear();
                    _canvas.Children.Add(pl); _canvas.Children.Add(pl1);
                }
                
                
        }
    }
}
