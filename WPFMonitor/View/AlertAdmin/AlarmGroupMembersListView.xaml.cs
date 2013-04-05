using DockingLibrary;
using WPFMonitor.Core.ViewModel.AlertAdmin;


namespace WPFMonitor.View.AlertAdmin
{
    /// <summary>
    /// AlarmGroupMembersListView.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmGroupMembersListView : DockableContent
    {
        public AlarmGroupMembersListView()
        {
            InitializeComponent();
            this.DataContext = new AlarmGroupMembersListViewModel();
        }
    }
}

