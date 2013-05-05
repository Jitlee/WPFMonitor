using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using WPFMonitor.DAL.ZTControls;
using WPFMonitor.Model.ZTControls;

namespace WPFMonitor.Core.TPControls
{
    public class ControlVM : EntityObject
    {
        #region 变量

        private static readonly ControlVM _instance = new ControlVM();

        private bool _isLoaded = false;

        private List<t_Control> _controls = new List<t_Control>();

        private List<t_Control> _tpControls = new List<t_Control>();

        private List<t_Control> _ztControls = new List<t_Control>();

        private List<t_Control> _ggControls = new List<t_Control>();

        #endregion

        #region 属性

        public static ControlVM Instance { get { return _instance; } }

        public bool IsLoaded { get { return _isLoaded; } private set { _isLoaded = value; RaisePropertyChanged("IsLoaded"); } }

        public List<t_Control> Controls { get { return _controls; } private set { _controls = value; RaisePropertyChanged("Controls"); } }

        public List<t_Control> TPControls { get { return _tpControls; } private set { _tpControls = value; RaisePropertyChanged("TPControls"); } }

        public List<t_Control> ZTControls { get { return _ztControls; } private set { _ztControls = value; RaisePropertyChanged("ZTControls"); } }

        public List<t_Control> GGControls { get { return _ggControls; } private set { _ggControls = value; RaisePropertyChanged("GGControls"); } }

        #endregion

        #region 构造函数

        private ControlVM()
        {
            Task.Factory.StartNew(Load);
        }

        #endregion

        #region 公共方法 

        #endregion

        #region 私有方法

        private void Load()
        {
            _controls = new ControlDA().selectAllDate();
            
            _tpControls = _controls.Where(c => c.ControlType == 1).ToList();
            _ztControls = _controls.Where(c => c.ControlType == 2).ToList();
            _ggControls = _controls.Where(c => c.ControlType == 3).ToList();

            t_Control pointControl = new t_Control()
            {
                ControlID = -1,
                ControlCaption = "指针",
                ControlName = "指针",
                ImageURL = "point.jpg"
            };

            _tpControls.Insert(0, pointControl);
            _ztControls.Insert(0, pointControl);
            _ggControls.Insert(0, pointControl);

            Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                RaisePropertyChanged("TPControls");
                RaisePropertyChanged("ZTControls");
                RaisePropertyChanged("GGControls");
                IsLoaded = true;
            }));
        }

        #endregion
    }
}
