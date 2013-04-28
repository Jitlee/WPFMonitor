using DockingLibrary;
using WPFMonitor.Core.ViewModel;

namespace WPFMonitor.View.Sys
{
    /// <summary>
    /// StationListView.xaml 的交互逻辑
    /// </summary>
    public partial class StationListView : DockableContent
    {
        public StationListView()
        {
            InitializeComponent();
            this.DataContext = new StationListViewModel();
        }
    }
}
