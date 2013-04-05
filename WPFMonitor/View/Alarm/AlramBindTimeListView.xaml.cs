using DockingLibrary;
using WPFMonitor.Core.ViewModel.Alarm;


namespace WPFMonitor.View.Alarm
{
    /// <summary>
    /// AlramBindTimeListView.xaml 的交互逻辑
    /// </summary>
    public partial class AlramBindTimeListView : DockableContent
    {
        public AlramBindTimeListView()
        {
            InitializeComponent();
            this.DataContext = new AlramBindTimeListViewModel();
        }
    }
}

