using System.Windows;
using WPFMonitor.Model.Linkage;
using WPFMonitor.Core.ViewModel.Linkage;


namespace  WPFMonitor.View.Linkage
{
    /// <summary>
    /// TimeLinkageEdit.xaml 的交互逻辑
    /// </summary>
    public partial class TimeLinkageEditView : Window
    {
        public TimeLinkageEditView(TimeLinkageListViewModel _List)
        {
            InitializeComponent();
            this.DataContext = new TimeLinkageEditViewModel(_List, this);
        }

        public TimeLinkageEditView(TimeLinkageListViewModel _List, TimeLinkageOR _TimeLinkage)
        {
            InitializeComponent();
            this.DataContext = new TimeLinkageEditViewModel(_List, this, _TimeLinkage);
        }
    }
}

