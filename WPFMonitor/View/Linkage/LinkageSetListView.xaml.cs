using DockingLibrary;
using WPFMonitor.Core.ViewModel.Linkage;


namespace WPFMonitor.View.Linkage
{
    /// <summary>
    /// LinkageSetListView.xaml 的交互逻辑
    /// </summary>
    public partial class LinkageSetListView : DockableContent
    {
        public LinkageSetListView()
        {
            InitializeComponent();
            this.DataContext = new LinkageSetListViewModel();
        }
    }
}

