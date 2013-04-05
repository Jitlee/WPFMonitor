using DockingLibrary;
using WPFMonitor.Core.ViewModel.AlertAdmin;


namespace WPFMonitor.View.AlertAdmin
{
    /// <summary>
    /// MainteListView.xaml 的交互逻辑
    /// </summary>
    public partial class MainteListView : DockableContent
    {
        public MainteListView()
        {
            InitializeComponent();
            this.DataContext = new MainteListViewModel();
        }
    }
}

