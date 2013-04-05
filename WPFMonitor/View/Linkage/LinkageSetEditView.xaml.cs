using System.Windows;
using WPFMonitor.Model.Linkage;
using WPFMonitor.Core.ViewModel.Linkage;


namespace  WPFMonitor.View.Linkage
{
    /// <summary>
    /// LinkageSetEdit.xaml 的交互逻辑
    /// </summary>
    public partial class LinkageSetEditView : Window
    {
        public LinkageSetEditView(LinkageSetListViewModel _List)
        {
            InitializeComponent();
            this.DataContext = new LinkageSetEditViewModel(_List, this);
        }

        public LinkageSetEditView(LinkageSetListViewModel _List, LinkageSetOR _LinkageSet)
        {
            InitializeComponent();
            this.DataContext = new LinkageSetEditViewModel(_List, this, _LinkageSet);
        }
    }
}

