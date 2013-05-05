using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WPFMonitor.Model.ZTControls;
using System.Windows;
using System.Threading.Tasks;

namespace WPFMonitor.Core.TPControls
{
    public class ScreenTreeVM : EntityObject
    {
        #region 变量

        private static readonly ScreenTreeVM _instance = new ScreenTreeVM();

        private bool _isLoaded = false;

        private List<t_Screen> _allScreens = new List<t_Screen>();

        private ObservableCollection<t_Screen> _screens = new ObservableCollection<t_Screen>();

        #endregion

        #region 属性

        public static ScreenTreeVM Instance { get { return _instance; } }

        public bool IsLoaded { get { return _isLoaded; } private set { _isLoaded = value; RaisePropertyChanged("IsLoaded"); } }

        public List<t_Screen> AllScreens { get { return _allScreens; } }

        public ObservableCollection<t_Screen> Screens { get { return _screens; } private set { _screens = value; RaisePropertyChanged("Screens"); } }

        #endregion

        #region 构造函数

        private ScreenTreeVM()
        {
            Task.Factory.StartNew(Load);
        }

        #endregion

        #region 公共方法 
        public t_Screen GetScreen(int ScreenID)
        {
            var v = from f in AllScreens where f.ScreenID == ScreenID select f;
            if (v.Count() > 0)
                return v.First();
            return null;
        }
        #endregion

        #region 私有方法

        private void Load()
        {
            _allScreens = new DAL.ZTControls.ScreenDA().selectAllDate();
            List<t_Screen> _list = new List<t_Screen>();

            foreach (var screen in _allScreens)
            {
                if (screen.ParentScreenID == 0)
                {
                    _list.Add(screen);
                }
                else if(_allScreens.Any(s => s.ScreenID == screen.ParentScreenID))
                {
                    _allScreens.First(s => s.ScreenID == screen.ParentScreenID).Children.Add(screen);
                }
            }
            Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                Screens = new ObservableCollection<t_Screen>(_list);
                IsLoaded = true;
            }));
        }

        #endregion

       
    }
}
