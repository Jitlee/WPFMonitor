using DockingLibrary;
using WPFMonitor.Core.ViewModel.AlertAdmin;


namespace WPFMonitor.View.AlertAdmin
{
    /// <summary>
    /// DisarmTimeListView.xaml 的交互逻辑
    /// </summary>
    public partial class DisarmTimeListView : DockableContent
    {
        public DisarmTimeListView()
        {
            InitializeComponent();
            this.DataContext = new DisarmTimeListViewModel();
        }
    }
}

