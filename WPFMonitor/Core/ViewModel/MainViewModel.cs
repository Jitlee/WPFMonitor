using System.Collections.ObjectModel;
using WPFMonitor.Core.Model;

namespace WPFMonitor.Core.ViewModel
{
    internal class MainViewModel : EntityObject
    {
        #region 属性

        private static readonly MainViewModel _instance = new MainViewModel();
        public static MainViewModel Instance { get { return _instance; } }

        private readonly DelegateCommand<string> _command;
        public DelegateCommand<string> Command { get { return _command; } }

     

        #endregion

        

      
    }
}
