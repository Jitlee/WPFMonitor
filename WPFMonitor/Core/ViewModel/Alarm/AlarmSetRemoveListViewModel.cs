using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFMonitor.DAL.Sys;
using System.Collections.ObjectModel;
using WPFMonitor.Model.Sys;
using WPFMonitor.View.Alarm;
using System.Windows.Controls;

namespace WPFMonitor.Core.ViewModel.Alarm
{
    public class AlarmSetRemoveListViewModel : ListBaseViewModel
    {
        AlarmSetRemoveView _Window;

        public AlarmSetRemoveListViewModel(AlarmSetRemoveView mWIn)
        {
            _Window = mWIn;
        }

        public override void Insert()
        {
        }

        
        private void Init()
        {
            _Window.cbStation.SelectionChanged += new SelectionChangedEventHandler(cbStation_SelectionChanged);

            StationDA _staDA = new StationDA();
            StationORList = _staDA.selectAllStation();

        }

        ObservableCollection<StationOR> _StationORList = new ObservableCollection<StationOR>();
        public ObservableCollection<StationOR> StationORList
        {
            set {
                _StationORList = value; 
                NotifyPropertyChanged("StationORList"); 
            }
            get { return _StationORList; }
        }
        public StationOR SelectStationOR { get; set; }
        public void cbStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }





        public override void Update(object par)
        {

        }

        public override void Delete(object parameter)
        {

        }
    }
}
