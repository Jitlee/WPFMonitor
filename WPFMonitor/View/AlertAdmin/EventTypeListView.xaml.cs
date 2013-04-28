using DockingLibrary;
using WPFMonitor.Core.ViewModel.AlertAdmin;


namespace WPFMonitor.View.AlertAdmin
{
    /// <summary>
    /// EventTypeListView.xaml 的交互逻辑
    /// </summary>
    public partial class EventTypeListView : DockableContent
    {
        public EventTypeListView()
        {
            InitializeComponent();
            this.DataContext = new EventTypeListViewModel();
        }
    }
}

