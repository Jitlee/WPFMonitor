﻿using DockingLibrary;
using WPFMonitor.Core.ViewModel.AlertAdmin;


namespace WPFMonitor.View.AlertAdmin
{
    /// <summary>
    /// AlarmLevelSetListView.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmLevelSetListView : DockableContent
    {
        public AlarmLevelSetListView()
        {
            InitializeComponent();
            this.DataContext = new AlarmLevelSetListViewModel();
        }
    }
}

