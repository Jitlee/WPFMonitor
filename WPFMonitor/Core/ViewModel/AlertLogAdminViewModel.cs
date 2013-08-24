using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFMonitor.DAL.AlertAdmin;
using System.Collections.ObjectModel;
using WPFMonitor.Model.AlertAdmin;
using System.Threading;

namespace WPFMonitor.Core.ViewModel
{
	public class AlertLogAdminViewModel : ListBaseViewModel
	{
		public AlertLogAdminViewModel()
		{
			Init();
		}
		ObservableCollection<AlarmLogOR> _AlarmORList = null;

		public ObservableCollection<AlarmLogOR> AlarmORList
		{
			get { return _AlarmORList; }
		}
		public void Init()
		{
			AlarmLogDA mDA = new AlarmLogDA();
			if (null != _AlarmORList)
				_AlarmORList.Clear();
			else
				_AlarmORList = new ObservableCollection<AlarmLogOR>();

			var vList = mDA.SelectAllLog();
			if (vList != null)
			{
				foreach (AlarmLogOR obj in vList)
				{
					_AlarmORList.Add(obj);
				}
			}
			Thread Th = new Thread(RefList);
			Th.IsBackground = true;
			Th.Start();
		}

		public void RefList()
		{
			int TimeOutLen = 1 * 60 * 1000;
			do
			{
				try
				{
					Thread.Sleep(TimeOutLen);
					AlarmLogDA mDA = new AlarmLogDA();
					if (null == _AlarmORList)
						_AlarmORList = new ObservableCollection<AlarmLogOR>();

					var vList = mDA.SelectAllLog();
					if (vList != null)
					{
						if (vList.Count == 0 && _AlarmORList.Count > 0)
							_AlarmORList.Clear();

						if (vList.Count == _AlarmORList.Count && vList.First().AlarmLogID == _AlarmORList.First().AlarmLogID)
							return;

						foreach (AlarmLogOR obj in vList)
						{
							_AlarmORList.Add(obj);
						}
					}
				}
				catch (Exception ex)
				{

				}

			} while (true);
		}

		public override void Insert()
		{
			throw new NotImplementedException();
		}

		public override void Update(object parameter)
		{
			if (parameter is Int32)
			{
				int LogID = Convert.ToInt32(parameter);
				AlarmLogDA mDA = new AlarmLogDA();
				if (mDA.Confirm(LogID))
				{
					var vObj = AlarmORList.Where(a => a.AlarmLogID == LogID);
					if (vObj.Count() > 0)
					{
						AlarmORList.Remove(vObj.First());
					}
				}
			}
		}

		public override void Delete(object parameter)
		{
			throw new NotImplementedException();
		}
	}
}
