using DockingLibrary;
using WPFMonitor.Core.ViewModel.Alarm;


namespace WPFMonitor.View.Alarm
{
    /// <summary>
    /// InspectionListView.xaml 的交互逻辑
    /// </summary>
    public partial class InspectionListView : DockableContent
    {
        public InspectionListView()
        {
            InitializeComponent();
            this.DataContext = new InspectionListViewModel();
        }
    }
}

