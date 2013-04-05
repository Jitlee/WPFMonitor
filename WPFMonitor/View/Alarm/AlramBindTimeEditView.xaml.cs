using System.Windows;
using WPFMonitor.Model.Alarm;
using WPFMonitor.Core.ViewModel.Alarm;


namespace  WPFMonitor.View.Alarm
{
    /// <summary>
    /// AlramBindTimeEdit.xaml 的交互逻辑
    /// </summary>
    public partial class AlramBindTimeEditView : Window
    {
        public AlramBindTimeEditView(AlramBindTimeListViewModel _List)
        {
            InitializeComponent();
            this.DataContext = new AlramBindTimeEditViewModel(_List, this);
        }

        public AlramBindTimeEditView(AlramBindTimeListViewModel _List, AlramBindTimeOR _AlramBindTime)
        {
            InitializeComponent();
            this.DataContext = new AlramBindTimeEditViewModel(_List, this, _AlramBindTime);
        }
    }
}

