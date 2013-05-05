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

namespace WPFMonitor.Library.ZTControls
{
    public class BeautyFont
    {
        PathGeometry _pathGeometry;
        static byte[,] bySegment ={
            {1,1,1,0,1,1,1},  //0
            {0,0,1,0,0,1,0},  //1
            {1,0,1,1,1,0,1},  //2
            {1,0,1,1,0,1,1},  //3
            {0,1,1,1,0,1,0},  //4
            {1,1,0,1,0,1,1},  //5
            {1,1,0,1,1,1,1},  //6
            {1,0,1,0,0,1,0},  //7
            {1,1,1,1,1,1,1},  //8
            {1,1,1,1,0,1,1},  //9
        };
        
        readonly Point[][] apt = new Point[7][];


        readonly double DPI_X = System.Windows.SystemParameters.PrimaryScreenWidth;

        readonly double DPI_Y = System.Windows.SystemParameters.PrimaryScreenHeight; 

        public BeautyFont(PathGeometry pathGeometry)
        {
            this._pathGeometry = pathGeometry;

            _pathGeometry.Figures.Clear();

            apt[0] = new Point[]{new Point(3,2),new Point(39,2),
                                 new Point(31,10),new Point(11,10)
            };

            apt[1] = new Point[]{new Point(2,3),new Point(10,11),
                                 new Point(10,31),new Point(2,35)
            };

            apt[2] = new Point[]{new Point(40,3),new Point(40,35),
                                 new Point(32,31),new Point(32,11)
            };

            apt[3] = new Point[]{new Point(3,36),new Point(11,32),
                                 new Point(31,32),new Point(39,36),
                                 new Point(31,40),new Point(11,40)
            };

            apt[4] = new Point[]{new Point(2,37),new Point(10,41),
                                 new Point(10,61),new Point(2,69)
            };

            apt[5] = new Point[]{new Point(40,37),new Point(40,69),
                                 new Point(32,61),new Point(32,41)
            };

            apt[6] = new Point[]{new Point(11,62),new Point(31,62),
                                 new Point(39,70),new Point(3,70)
            };
        }

        public Point MeasureString(string str, double fontSize)
        {
            var sizef = new Point(0, DPI_X * fontSize / 72D);

            for (int i = 0; i < str.Length; i++)
            {
                if (Char.IsDigit(str[i]))
                    sizef.X += 42d * DPI_X * fontSize / 72d / 72d;
                else
                    if (str[i] == '.')
                        sizef.X += 12d * DPI_X * fontSize / 72d / 72d;
            }
            return sizef;
        }

        public void DrawString(string str, double fontSize, double x, double y)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (Char.IsDigit(str[i]))
                    x = Number(str[i] - '0', fontSize, x, y);
                else
                    if (str[i] == '.')
                        x = Colon(fontSize, x, y);
            }
        }
        private double Number(int num, double fontSize, double x, double y)
        {
            for (int i = 0; i < apt.Length; i++)
                if (bySegment[num, i] == 1)
                    Fill(apt[i], fontSize, x, y);
            return x + 42 * DPI_X * fontSize / 72d / 72d;
        }

        private double Colon(double fontSize, double x, double y)
        {
            Point[] apt = new Point[] { new Point(2, 66), new Point(6, 62), new Point(10, 66), new Point(6, 70) };
            Fill(apt, fontSize, x, y);
            return x + 12d * DPI_X * fontSize / 72d / 72d;
        }

        private void Fill(Point[] apt, double fontSize, double x, double y)
        {
            Point[] aptf = new Point[apt.Length];

            if(apt.Length > 1)
            {
                var pathFigure = new PathFigure();
                pathFigure.StartPoint = new Point(x + apt[0].X * DPI_X * fontSize / 72d / 72d, y + apt[0].Y * DPI_Y * fontSize / 72d / 72d);
                for (int i = 1; i < apt.Length; i++)
                {
                    pathFigure.Segments.Add(new LineSegment()
                    {
                        Point = new Point(x + apt[i].X * DPI_X * fontSize / 72d / 72d, y + apt[i].Y * DPI_Y * fontSize / 72d / 72d)
                    });
                }
                _pathGeometry.Figures.Add(pathFigure);
            }
        }
    }
}
