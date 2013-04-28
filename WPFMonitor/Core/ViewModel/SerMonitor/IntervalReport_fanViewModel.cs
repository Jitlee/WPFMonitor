using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFMonitor.View.SerMonitor;
using System.Windows.Controls;
using WPFMonitor.DAL.Sys;
using System.Collections.ObjectModel;
using WPFMonitor.Model.Sys;

namespace WPFMonitor.Core.ViewModel.SerMonitor
{
    public class IntervalReport_fanViewModel:EditBaseViewModel
    {
        IntervalReport_fanView _Window;

        public IntervalReport_fanViewModel(IntervalReport_fanView mwin)
        {
            _Window = mwin;
            //Init();
        }

       

        

        #region 事件


        #endregion

        

        public override void OK()
        {

        }
    }
}
