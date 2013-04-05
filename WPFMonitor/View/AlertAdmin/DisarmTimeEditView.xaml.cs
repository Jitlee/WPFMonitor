using System.Windows;
using WPFMonitor.Model.AlertAdmin;
using WPFMonitor.Core.ViewModel.AlertAdmin;


namespace  WPFMonitor.View.AlertAdmin
{
    /// <summary>
    /// DisarmTimeEdit.xaml 的交互逻辑
    /// </summary>
    public partial class DisarmTimeEditView : Window
    {
        public DisarmTimeEditView(DisarmTimeListViewModel _List)
        {
            InitializeComponent();
            this.DataContext = new DisarmTimeEditViewModel(_List, this);
        }

        public DisarmTimeEditView(DisarmTimeListViewModel _List, DisarmTimeOR _DisarmTime)
        {
            InitializeComponent();
            this.DataContext = new DisarmTimeEditViewModel(_List, this, _DisarmTime);
        }
    }
}

