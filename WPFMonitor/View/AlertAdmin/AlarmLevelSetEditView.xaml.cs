using System.Windows;
using WPFMonitor.Model.AlertAdmin;
using WPFMonitor.Core.ViewModel.AlertAdmin;


namespace  WPFMonitor.View.AlertAdmin
{
    /// <summary>
    /// AlarmLevelSetEdit.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmLevelSetEditView : Window
    {
        public AlarmLevelSetEditView(AlarmLevelSetListViewModel _List)
        {
            InitializeComponent();
            this.DataContext = new AlarmLevelSetEditViewModel(_List, this);
        }

        public AlarmLevelSetEditView(AlarmLevelSetListViewModel _List, AlarmLevelSetOR _AlarmLevelSet)
        {
            InitializeComponent();
            this.DataContext = new AlarmLevelSetEditViewModel(_List, this, _AlarmLevelSet);
        }
    }
}

