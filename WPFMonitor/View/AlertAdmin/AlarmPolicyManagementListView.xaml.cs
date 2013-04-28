using DockingLibrary;
using WPFMonitor.Core.ViewModel.AlertAdmin;


namespace WPFMonitor.View.AlertAdmin
{
    /// <summary>
    /// AlarmPolicyManagementListView.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmPolicyManagementListView : DockableContent
    {
        public AlarmPolicyManagementListView()
        {
            InitializeComponent();
            this.DataContext = new AlarmPolicyManagementListViewModel(this);
        }
    }
}

