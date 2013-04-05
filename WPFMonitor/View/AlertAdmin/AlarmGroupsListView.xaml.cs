using DockingLibrary;
using WPFMonitor.Core.ViewModel.AlertAdmin;


namespace WPFMonitor.View.AlertAdmin
{
    /// <summary>
    /// AlarmGroupsListView.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmGroupsListView : DockableContent
    {
        public AlarmGroupsListView()
        {
            InitializeComponent();
            this.DataContext = new AlarmGroupsListViewModel();
        }
    }
}

