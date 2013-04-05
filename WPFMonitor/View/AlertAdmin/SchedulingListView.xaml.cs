using DockingLibrary;
using WPFMonitor.Core.ViewModel.AlertAdmin;


namespace WPFMonitor.View.AlertAdmin
{
    /// <summary>
    /// SchedulingListView.xaml 的交互逻辑
    /// </summary>
    public partial class SchedulingListView : DockableContent
    {
        public SchedulingListView()
        {
            InitializeComponent();
            this.DataContext = new SchedulingListViewModel();
        }
    }
}

