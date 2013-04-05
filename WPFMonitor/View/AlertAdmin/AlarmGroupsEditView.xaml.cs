using System.Windows;
using WPFMonitor.Model.AlertAdmin;
using WPFMonitor.Core.ViewModel.AlertAdmin;


namespace  WPFMonitor.View.AlertAdmin
{
    /// <summary>
    /// AlarmGroupsEdit.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmGroupsEditView : Window
    {
        public AlarmGroupsEditView(AlarmGroupsListViewModel _List)
        {
            InitializeComponent();
            this.DataContext = new AlarmGroupsEditViewModel(_List, this);
        }

        public AlarmGroupsEditView(AlarmGroupsListViewModel _List, AlarmGroupsOR _AlarmGroups)
        {
            InitializeComponent();
            this.DataContext = new AlarmGroupsEditViewModel(_List, this, _AlarmGroups);
        }
    }
}

