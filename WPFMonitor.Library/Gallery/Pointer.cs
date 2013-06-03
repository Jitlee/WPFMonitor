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

namespace MonitorSystem.Gallery
{
    /// <summary>
    /// 图库的指针类
    /// </summary>
    public class Pointer : MonitorControl
    {
        /// <summary>
        /// 图库的指针类 构造函数
        /// </summary>
        public Pointer()
        {
            this.Content = new PointerControl();
        }

        public override void DesignMode()
        {
        }

        public override void UnDesignMode()
        {
        }

        public override FrameworkElement GetRootControl()
        {
            return this;
        }

        public override event EventHandler Selected;

        public override event EventHandler Unselected;

        public override void SetPropertyValue()
        {
        }

        public override void SetCommonPropertyValue()
        {
        }

        public override string[] BrowsableProperties { get; set; }

        private void OnSelected()
        {
            if (null != Selected)
            {
                Selected(this, EventArgs.Empty);
            }
        }

        private void OnUnselected()
        {
            if (null != Unselected)
            {
                Unselected(this, EventArgs.Empty);
            }
        }
    }
}
