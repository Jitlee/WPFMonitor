using System.Windows;
using WPFMonitor.Model.AlertAdmin;
using WPFMonitor.Core.ViewModel.AlertAdmin;


namespace  WPFMonitor.View.AlertAdmin
{
    /// <summary>
    /// EventTypeEdit.xaml 的交互逻辑
    /// </summary>
    public partial class EventTypeEditView : Window
    {
        public EventTypeEditView(EventTypeListViewModel _List)
        {
            InitializeComponent();
            this.DataContext = new EventTypeEditViewModel(_List, this);
        }

        public EventTypeEditView(EventTypeListViewModel _List, EventTypeOR _EventType)
        {
            InitializeComponent();
            this.DataContext = new EventTypeEditViewModel(_List, this, _EventType);
        }
    }
}

