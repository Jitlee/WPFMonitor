using DockingLibrary;
using WPFMonitor.Core.ViewModel.Linkage;


namespace WPFMonitor.View.Linkage
{
    /// <summary>
    /// TimeLinkageListView.xaml 的交互逻辑
    /// </summary>
    public partial class TimeLinkageListView : DockableContent
    {
        public TimeLinkageListView()
        {
            InitializeComponent();
            this.DataContext = new TimeLinkageListViewModel();
        }
    }
}

