using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFMonitor.Model.ZTControls;

namespace WPFMonitor.Library.MonitorSystemGlobal
{
    public class LoadScreenEventArgs : EventArgs
    {
        public t_Screen Screen { get; protected set; }

        public LoadScreenEventArgs(t_Screen screen)
        {
            this.Screen = screen;
        }
    }
}
