using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MonitorSystem.Other
{
    public partial class CWLineProperty : Window
    {
        RealTimeLineOR _RealLineOr = null;
        RealTimeT _RealTimeData;
        public CWLineProperty(RealTimeLineOR LineOR, RealTimeT _timeT)
        {
            InitializeComponent();

            _RealLineOr = LineOR;
            _RealTimeData = _timeT;
            this.lineProperty1.SetRealTimeLineOR(_RealLineOr.LineInfo);
        }

        /// <summary>
        /// 隐藏曲线信息，只显示时间
        /// </summary>
        /// <param name="LineOR"></param>
        /// <param name="ISHideInfo"></param>
        public CWLineProperty(RealTimeLineOR LineOR,bool ISHideInfo)
        {
            InitializeComponent();

            _RealLineOr = LineOR;
            this.lineProperty1.HideInfo();
            this.Title = "曲线时间";
            this.lineProperty1.SetRealTimeLineOR(_RealLineOr.LineInfo);
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            _RealTimeData._CanvasPoint.Children.Clear();            

            _RealLineOr.LineInfo = lineProperty1.SaveRealTimeLineOR();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

