using System.Windows;
using WPFMonitor.Model.Alarm;
using WPFMonitor.Core.ViewModel.Alarm;


namespace  WPFMonitor.View.Alarm
{
    /// <summary>
    /// FalseAlarmPolicyEdit.xaml 的交互逻辑
    /// </summary>
    public partial class FalseAlarmPolicyEditView : Window
    {
        public FalseAlarmPolicyEditView(FalseAlarmPolicyListViewModel _List)
        {
            InitializeComponent();
            this.DataContext = new FalseAlarmPolicyEditViewModel(_List, this);
        }

        public FalseAlarmPolicyEditView(FalseAlarmPolicyListViewModel _List, FalseAlarmPolicyOR _FalseAlarmPolicy)
        {
            InitializeComponent();
            this.DataContext = new FalseAlarmPolicyEditViewModel(_List, this, _FalseAlarmPolicy);
        }
    }
}

