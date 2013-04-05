using DockingLibrary;
using WPFMonitor.Core.ViewModel.Alarm;


namespace WPFMonitor.View.Alarm
{
    /// <summary>
    /// KeyWordListView.xaml 的交互逻辑
    /// </summary>
    public partial class KeyWordListView : DockableContent
    {
        public KeyWordListView()
        {
            InitializeComponent();
            this.DataContext = new KeyWordListViewModel();
        }
    }
}

