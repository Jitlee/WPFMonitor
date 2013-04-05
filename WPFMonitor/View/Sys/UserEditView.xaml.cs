using System.Windows;
using WPFMonitor.Model.Sys;
using WPFMonitor.Core.ViewModel.Sys;


namespace  WPFMonitor.View.Sys
{
    /// <summary>
    /// UserEdit.xaml 的交互逻辑
    /// </summary>
    public partial class UserEditView : Window
    {
        public UserEditView(UserListViewModel _List)
        {
            InitializeComponent();
            this.DataContext = new UserEditViewModel(_List, this);
        }

        public UserEditView(UserListViewModel _List, UserOR _User)
        {
            InitializeComponent();
            this.DataContext = new UserEditViewModel(_List, this, _User);
        }

        public void Init()
        {
           
        }
    }
}

