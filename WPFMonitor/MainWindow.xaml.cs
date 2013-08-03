﻿using System;
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
using WPFMonitor.View;
using DockingLibrary;
using WPFMonitor.View.Sys;
using WPFMonitor.View.Alarm;
using WPFMonitor.View.AlertAdmin;
using WPFMonitor.View.Linkage;
using WPFMonitor.View.TPControls;
using WPFMonitor.View.SerMonitor;
using MonitorSystem.MonitorSystemGlobal;
using WPFMonitor.Core.TPControls;

namespace WPFMonitor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly ScreenTreeWindow _screenTreeWindow = new ScreenTreeWindow();
        readonly ControlWindow _controlWindow = new ControlWindow();
        readonly LoadScreen _loadScreen = new LoadScreen();
        readonly PropertyWindow _propertyWindow = new PropertyWindow();
        readonly GalleryWindow _galleryWindow = new GalleryWindow() { FloatingWindowSize = new Size(600, 200),};

        public MainWindow()
        {
            //test t = new test();
            //t.ShowDialog();
            //return;

            InitializeComponent();
            Global._MainWindow = this;
            MonitorSystem.Common1.MainWin = Global._MainWindow;
            ThemeFactory.ChangeTheme("Leather");
            return;
        }

        private void dockManager_Loaded(object sender, RoutedEventArgs e)
        {
            var s = ScreenTreeVM.Instance;

            _loadScreen.ShowAsDocument(dockManager);

            MonitorControl.UpdatePropertyGrid = (properties, o) => 
            {
                _propertyWindow.ControlPropertyGrid.SelectedObject = o;
                _propertyWindow.ControlPropertyGrid.BrowsableProperties = properties;
            };
            MonitorControl.GetMainCanvas = () =>
                {
                    return _loadScreen.csScreen;
                };

            MonitorControl.InitElement = (ctrl) =>
                {
                    return _loadScreen.InitElement(ctrl);
                };

            MonitorControl.LoadElement = (c, obj, eleStae, listObj) =>
                {
                    return _loadScreen.ShowElement(c, obj, eleStae, listObj);
                };

            MonitorControl.LoadScreen = (sc) =>
                {
                    _loadScreen.LoadSence(sc);
                };

            MonitorControl.GetScreenList = () =>
                {
                    return ScreenTreeVM.Instance.AllScreens;
                };

            _loadScreen.ResetSelected = () =>
            {
                _controlWindow.ResetSelected();
                _galleryWindow.ResetSelected();
            };

            _loadScreen.TP = () =>
            {
                _galleryWindow.Show(dockManager, AnchorStyle.Bottom);
                _propertyWindow.Show(dockManager, AnchorStyle.Left);
                _screenTreeWindow.Show(dockManager, AnchorStyle.Left);
                _controlWindow.Show(dockManager, AnchorStyle.Left);
                //_galleryWindow.ShowAsFloatingWindow(dockManager, true);
            };

            _loadScreen.ZTExit = () => {
                _propertyWindow.Hide();
                _screenTreeWindow.Hide();
                _controlWindow.Hide();
                _galleryWindow.Hide();
            };

            _loadScreen.DesignVisilityChanged = () => {
                //if (_galleryWindow.Visibility != Visibility.Visible)
                //{
                //    _galleryWindow.ShowAsFloatingWindow(dockManager, true);
                //}
                //else
                //{
                //    _galleryWindow.Hide();
                //}
            };

            _loadScreen.GalleryVisibilityChanged = () => {
                //if (_galleryWindow.Visibility != Visibility.Visible)
                //{
                //    _galleryWindow.ShowAsFloatingWindow(dockManager, true);
                //}
                //else
                //{
                //    _galleryWindow.Hide();
                //}
            };
        }

        #region 菜单事件
        
        /// <summary>
        /// 系统管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Sysdmin_Click(object sender, RoutedEventArgs e)
        {
            var Menu = sender as MenuItem;
            if (Menu != null)
            {
                string menuName = Menu.Name;
                string mHeader = Menu.Header.ToString();
                DockableContent mWindow = null;
                switch (menuName)
                {
                    //一级  [系统管理]
                    case "StationLis"://[站点管理]
                        mWindow = new StationListView();
                        break;
                    case "UserList"://[用户管理]
                        mWindow = new UserListView();
                        break;
                }

                if (mWindow != null)
                {
                    mWindow.Title = mHeader;
                    mWindow.ShowAsDocument(dockManager);
                }
            }
        }

        /// <summary>
        /// 报警管理 菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AlermAdmin_Click(object sender, RoutedEventArgs e)
        {
            var Menu = sender as MenuItem;
            if (Menu != null)
            {
                string menuName = Menu.Name;
                string mHeader = Menu.Header.ToString();
                DockableContent mWindow = null;
                switch (menuName)
                {
                    //一级  [报警管理]
                    case "InspectionList"://[定时巡检设置]
                        mWindow = new InspectionListView();
                        break;
                    case "KeyWordList"://[关键字配置]
                        mWindow = new KeyWordListView();
                        break;
                    case "AlarmSetRemove"://[声光告警解除]
                        mWindow = new AlarmSetRemoveView();
                        break;
                    //二级 [报警管理]-[ 报警管理]
                    case "EventTypeList"://[事件定义]
                        mWindow = new EventTypeListView();
                        break;
                    case "AlarmPolicyManagementList"://[报警策略管理]
                        mWindow = new AlarmPolicyManagementListView();
                        break;
                    case "AlarmLevelSet":
                        mWindow = new AlarmLevelSetListView();
                        break;
                    case "SchedulingList":
                        mWindow = new SchedulingListView();
                        break;
                    case "AlarmGroupMembersList":
                        mWindow = new AlarmGroupMembersListView();
                        break;
                    case "AlarmGroupsList":
                        mWindow = new AlarmGroupsListView();
                        break;
                    case "MainteList":
                        mWindow = new MainteListView();
                        break;
                    case "DisarmTimeList":
                        mWindow = new DisarmTimeListView();
                        break;
                    //二级[报警管理]-[误报警管理]
                    case "AlramBindTimeList":
                        mWindow = new AlramBindTimeListView();
                        break;
                    case "FalseAlarmPolicyList":
                        mWindow = new FalseAlarmPolicyListView();
                        break;

                }

                if (mWindow != null)
                {
                    mWindow.Title = mHeader;
                    mWindow.ShowAsDocument(dockManager);
                }
            }
        }
        /// <summary>
        /// 联动管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TimeLink_Click(object sender, RoutedEventArgs e)
        {
            var Menu = sender as MenuItem;
            if (Menu != null)
            {
                string menuName = Menu.Name;
                string mHeader = Menu.Header.ToString();
                DockableContent mWindow = null;
                switch (menuName)
                {
                    //一级  [联动管理]
                    case "LinkageSetList"://[设备联动设置]
                        mWindow = new LinkageSetListView();
                        break;
                    case "TimeLinkageList"://[时间联动设置]
                        mWindow = new TimeLinkageListView();
                        break;
                }

                if (mWindow != null)
                {
                    mWindow.Title = mHeader;
                    mWindow.ShowAsDocument(dockManager);
                }
            }
        }
        /// <summary>
        /// 监控管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MonitorAdmin_Click(object sender, RoutedEventArgs e)
        {
            var Menu = sender as MenuItem;
            if (Menu != null)
            {
                string menuName = Menu.Name;
                string mHeader = Menu.Header.ToString();
                DockableContent mWindow = null;
                switch (menuName)
                {
                    //一级  [监控管理]
                    case "AlarmLog"://[设备联动设置]
                        mWindow = new AlarmLogView();
                        break;
                    case "Dailyreport"://[历史曲线]
                        mWindow = new DailyreportView();
                        break;
                    case "IntervalReport"://[统计报表]
                        mWindow = new IntervalReport_fanView();
                        break;
                    case "HistoryValue"://[历史值查询]
                        mWindow = new HistoryValueView();
                        break;
                }

                if (mWindow != null)
                {
                    mWindow.Title = mHeader;
                    mWindow.ShowAsDocument(dockManager);
                }
            }
        }
        #endregion



        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            ThemeFactory.ChangeTheme((string)((MenuItem)sender).Tag);
        }

    }
}