using DockingLibrary;
using WPFMonitor.Core.ViewModel.Sys;


namespace WPFMonitor.View.Sys
{
    /// <summary>
    /// UserListView.xaml 的交互逻辑
    /// </summary>
    public partial class UserListView : DockableContent
    {
        public UserListView()
        {
            InitializeComponent();
            this.DataContext = new UserListViewModel();
        }
    }
}

