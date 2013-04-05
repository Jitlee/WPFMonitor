using System.Windows;
using WPFMonitor.Model.Alarm;
using WPFMonitor.Core.ViewModel.Alarm;


namespace  WPFMonitor.View.Alarm
{
    /// <summary>
    /// KeyWordEdit.xaml 的交互逻辑
    /// </summary>
    public partial class KeyWordEditView : Window
    {
        public KeyWordEditView(KeyWordListViewModel _List)
        {
            InitializeComponent();
            this.DataContext = new KeyWordEditViewModel(_List, this);
        }

        public KeyWordEditView(KeyWordListViewModel _List, KeyWordOR _KeyWord)
        {
            InitializeComponent();
            this.DataContext = new KeyWordEditViewModel(_List, this, _KeyWord);
        }
    }
}

