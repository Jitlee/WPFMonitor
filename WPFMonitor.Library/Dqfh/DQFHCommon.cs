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

namespace WPFMonitor.Library.Dqfh
{
    public class DQFHCommon
    {

        public static Color DQFHFilleColor = Common.StringToColor("RGB(75,62,40)");
        public static Color DQFHFilleColor2 = Common.StringToColor("RGB(211,209,209)");

        /// <summary>
        /// 电力电子线条颜色
        /// </summary>
        public static Color DQFHLineColor = Colors.Black;

        /// <summary>
        /// 电力电子，线条宽度
        /// </summary>
        public static double DQFHLineWidth = 0.5;
    }
}
