using System.Windows;
using WPFMonitor.Model.AlertAdmin;
using WPFMonitor.Core.ViewModel.AlertAdmin;


namespace  WPFMonitor.View.AlertAdmin
{
    /// <summary>
    /// SchedulingEdit.xaml 的交互逻辑
    /// </summary>
    public partial class SchedulingEditView : Window
    {
        public SchedulingEditView(SchedulingListViewModel _List)
        {
            InitializeComponent();
            this.DataContext = new SchedulingEditViewModel(_List, this);
        }

        public SchedulingEditView(SchedulingListViewModel _List, SchedulingOR _Scheduling)
        {
            InitializeComponent();
            this.DataContext = new SchedulingEditViewModel(_List, this, _Scheduling);
        }
    }
}

