using System.Windows;
using WPFMonitor.Model.Alarm;
using WPFMonitor.Core.ViewModel.Alarm;


namespace  WPFMonitor.View.Alarm
{
    /// <summary>
    /// InspectionEdit.xaml 的交互逻辑
    /// </summary>
    public partial class InspectionEditView : Window
    {
        public InspectionEditView(InspectionListViewModel _List)
        {
            InitializeComponent();
            this.DataContext = new InspectionEditViewModel(_List, this);
        }

        public InspectionEditView(InspectionListViewModel _List, InspectionOR _Inspection)
        {
            InitializeComponent();
            this.DataContext = new InspectionEditViewModel(_List, this, _Inspection);
        }
    }
}

