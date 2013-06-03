using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFMonitor.Model.ZTControls;

namespace MonitorSystem.MonitorSystemGlobal
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
