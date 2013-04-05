using System.Windows;
using WPFMonitor.Model.Sys;
using WPFMonitor.Core.ViewModel;

namespace WPFMonitor.View.Sys
{
    /// <summary>
    /// StationEdit.xaml 的交互逻辑
    /// </summary>
    public partial class StationEdit : Window
    {
        public StationEdit(StationListViewModel _List)
        {
            InitializeComponent();
            this.DataContext = new StationEditViewModel(_List,this);
        }

        public StationEdit(StationListViewModel _List,StationOR _Station)
        {
            InitializeComponent();
            this.DataContext = new StationEditViewModel(_List,this,_Station);
        }
    }
}
