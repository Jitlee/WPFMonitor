using DockingLibrary;
using WPFMonitor.Core.ViewModel.Alarm;


namespace WPFMonitor.View.Alarm
{
    /// <summary>
    /// FalseAlarmPolicyListView.xaml 的交互逻辑
    /// </summary>
    public partial class FalseAlarmPolicyListView : DockableContent
    {
        public FalseAlarmPolicyListView()
        {
            InitializeComponent();
            this.DataContext = new FalseAlarmPolicyListViewModel();
        }
    }
}

