using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DockingLibrary;
using WPFMonitor.Core.TPControls;
using WPFMonitor.Model.ZTControls;
using WPFMonitor.DAL.ZTControls;

namespace WPFMonitor.View.TPControls
{
    /// <summary>
    /// ScreenTreeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ScreenTreeWindow : DockableContent
    {
        public ScreenTreeWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ScreenTreeWindow_Loaded);

            InitMenu();
             
        }

        void ScreenTreeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ScreenTreeVM.Instance;
        }
        #region 菜单 处理
        ContextMenu Vmenu = new ContextMenu();
        /// <summary>
        /// 初使化右键菜单
        /// </summary>
        protected void InitMenu()
        {
            Vmenu.Items.Add(GetMenuItem("miAdd", "添加"));
            Vmenu.Items.Add(GetMenuItem("miEdit", "修改"));
            Vmenu.Items.Add(GetMenuItem("miDelete", "删除"));
            Vmenu.Items.Add(GetMenuItem("miOpen", "打开"));
            Vmenu.Items.Add(GetMenuItem("miCopy", "复制"));
            Vmenu.Items.Add(GetMenuItem("miSetDeftult", "设为首页"));
            trList.ContextMenu = Vmenu;
            trList.ContextMenuOpening += new ContextMenuEventHandler(trList_ContextMenuOpening);
        }

        t_Screen _select = null;
        void trList_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            _select = (e.OriginalSource as FrameworkElement).DataContext as t_Screen;
            

            if (null != _select)
            {
                t_Screen _TreeSelect = trList.SelectedItem as t_Screen;
                if (_TreeSelect != null)
                {
                    if (_TreeSelect.ScreenID != _select.ScreenID)//选择与右键不一致
                    {
                        e.Handled = true;
                        return;
                    }
                }
                MenuStatus(true);
            }
            else
            {
                MenuStatus(false);
            }
           
        }

        private void MenuStatus(bool isEnable)
        {
            foreach (MenuItem mitem in Vmenu.Items)
            {
                if (mitem.Name == "miAdd")
                {
                    continue;
                }
                mitem.IsEnabled = isEnable;

            }
        }

        protected void treeMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi == null)
                return;
            if (mi.Name != "miAdd" && _select == null)
            {
                GlobalData.ShowMsgError("请选择要操作的场景!");
            }

            if (mi.Name == "miAdd")
            {
                ScreenEdit sedit = new ScreenEdit(_select,OpType.Add);
                sedit.Owner = Global._MainWindow;
                sedit.ShowDialog();
            }
            else if (mi.Name == "miEdit")
            {
                if (null == _select)
                {
                    return;                    
                }
                ScreenEdit sedit = new ScreenEdit(_select,OpType.Alert);
                sedit.Owner = Global._MainWindow;
                sedit.ShowDialog();
            }
            else if (mi.Name == "miDelete")
            {
                if (MessageBox.Show(string.Format("您确定要删除场景:{0} 吗?", _select.ScreenName),
                    "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel)
                    return;

                ScreenDA m_DA = new ScreenDA();
                if(m_DA.Delete(_select.ScreenID))
                {
                    if (_select.ParentScreenID != 0)
                    {
                        t_Screen obj = ScreenTreeVM.Instance.GetScreen(_select.ParentScreenID);
                        if (obj == null)
                        {
                            GlobalData.ShowMsgError("找不到父节点");
                            return;
                        }
                        obj.Children.Remove(_select);
                    }
                    else
                    {
                        ScreenTreeVM.Instance.Screens.Remove(_select);
                    }
                    ScreenTreeVM.Instance.AllScreens.Remove(_select);                       
                }

            }
            else if (mi.Name == "miOpen")
            {

            }
            else if (mi.Name == "miCopy")
            {
                ScreenCopy mCopy = new ScreenCopy();
                mCopy.Owner = Global._MainWindow;
                mCopy.oldScreen = _select;
                mCopy.Show();
            }
            else if (mi.Name == "miSetDeftult")
            {

            }           
        }

        protected MenuItem GetMenuItem(string name, string strHeader)
        {            
            MenuItem vitem = new MenuItem();
            vitem.Click += new RoutedEventHandler(treeMenu_Click);
            vitem.Name = name;
            vitem.Header = strHeader;
            return vitem;
        }

        #endregion
    }
}
