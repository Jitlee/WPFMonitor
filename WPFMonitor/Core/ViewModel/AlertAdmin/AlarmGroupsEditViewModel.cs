using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WPFMonitor.DAL.AlertAdmin;
using WPFMonitor.DAL.Sys;
using WPFMonitor.Model.AlertAdmin;
using WPFMonitor.Model.Sys;
using WPFMonitor.View.AlertAdmin;


namespace WPFMonitor.Core.ViewModel.AlertAdmin
{
    public class AlarmGroupsEditViewModel : EditBaseViewModel
    {
        AlarmGroupsListViewModel _AlarmGroupsListVM;
        AlarmGroupsOR _SourceObj;
        AlarmGroupsEditView _Window;
        public AlarmGroupsEditViewModel(AlarmGroupsListViewModel _vm, AlarmGroupsEditView _mw)
        {
            _AlarmGroupsListVM = _vm;
            _Window = _mw;
            OperationType = OpType.Add;
            AlarmGroupsObj = new AlarmGroupsOR();
            //UpdatetxtSource(_Window.gridContent);

            Init();
        }

        public AlarmGroupsEditViewModel(AlarmGroupsListViewModel _vm, AlarmGroupsEditView _mw, AlarmGroupsOR _AlarmGroupsObj)
        {
            _AlarmGroupsListVM = _vm;
            _Window = _mw;
            _SourceObj = _AlarmGroupsObj;

            OperationType = OpType.Alert;
            AlarmGroupsObj = new AlarmGroupsOR();
            AlarmGroupsObj.Clone(_AlarmGroupsObj);

            Init();
        }
        private void Init()
        {
            StationDA _staDA = new StationDA();
            StationORList = _staDA.selectAllStation();

            if (OperationType == OpType.Alert)
            {
                if (StationORList != null && AlarmGroupsObj != null)
                {
                    var vS = from f in StationORList where f.Stationid == AlarmGroupsObj.Stationid select f;
                    if (vS.Count() != 0)
                        SelectStationOR = vS.First();
                }
            }
        }
        /// <summary>
        /// 当前站点
        /// </summary>
        public AlarmGroupsOR AlarmGroupsObj { get; set; }

        ObservableCollection<StationOR> _StationORList = new ObservableCollection<StationOR>();
        public ObservableCollection<StationOR> StationORList
        {
            set { _StationORList = value; NotifyPropertyChanged("StationORList"); }
            get { return _StationORList; }
        }
        public StationOR SelectStationOR { get; set; }


        public AlarmGroupsEditViewModel(AlarmGroupsOR _Sta)
        {
            OperationType = OpType.Alert;
            AlarmGroupsObj = _Sta;
        }

        public bool SetValue()
        {
            StringBuilder sbError = new StringBuilder();
            if (SelectStationOR == null)
            {
                sbError.AppendLine("请选择站点；");
            }
            else
            {
                AlarmGroupsObj.Stationid = SelectStationOR.Stationid;
            }
            if (string.IsNullOrEmpty(AlarmGroupsObj.Groupname))
            {
                sbError.AppendLine("组名称不能为空！");
            }

            string ErrorMsg = sbError.ToString();
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                ShowMsgError(ErrorMsg);
                return false;
            }

            return true;
        }

        public override void OK()
        {
            string errMsg = "";
            if (GetErrors(_Window.gridContent, out errMsg))
            {
                ShowMsgError(errMsg);
                return;
            }
            if (!SetValue())
                return;
            try
            {
                if (OperationType == OpType.Alert)
                {
                    new AlarmGroupsDA().Update(AlarmGroupsObj);
                    _SourceObj.Clone(AlarmGroupsObj);
                }
                else
                {
                    new AlarmGroupsDA().Insert(AlarmGroupsObj);
		    //_AlarmGroupsListVM.AlarmGroupsORList.Add(AlarmGroupsObj);
                    _AlarmGroupsListVM.Init();
                }
                _Window.Close();
            }
            catch (Exception ex)
            {
                ShowMsgError(ex.Message);
            }

        }


      
    }
}

