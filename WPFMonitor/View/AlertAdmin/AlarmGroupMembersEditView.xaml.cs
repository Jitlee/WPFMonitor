using System.Windows;
using WPFMonitor.Model.AlertAdmin;
using WPFMonitor.Core.ViewModel.AlertAdmin;


namespace  WPFMonitor.View.AlertAdmin
{
    /// <summary>
    /// AlarmGroupMembersEdit.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmGroupMembersEditView : Window
    {
        public AlarmGroupMembersEditView(AlarmGroupMembersListViewModel _List)
        {
            InitializeComponent();
            this.DataContext = new AlarmGroupMembersEditViewModel(_List, this);
        }

        public AlarmGroupMembersEditView(AlarmGroupMembersListViewModel _List, AlarmGroupMembersOR _AlarmGroupMembers)
        {
            InitializeComponent();
            this.DataContext = new AlarmGroupMembersEditViewModel(_List, this, _AlarmGroupMembers);
        }
    }
}

