using System.Windows;
using WPFMonitor.Model.AlertAdmin;
using WPFMonitor.Core.ViewModel.AlertAdmin;


namespace  WPFMonitor.View.AlertAdmin
{
    /// <summary>
    /// MainteEdit.xaml 的交互逻辑
    /// </summary>
    public partial class MainteEditView : Window
    {
        public MainteEditView(MainteListViewModel _List)
        {
            InitializeComponent();
            this.DataContext = new MainteEditViewModel(_List, this);
        }

        public MainteEditView(MainteListViewModel _List, MainteOR _Mainte)
        {
            InitializeComponent();
            this.DataContext = new MainteEditViewModel(_List, this, _Mainte);
        }
    }
}

