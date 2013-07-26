using System.Windows;
using WPFMonitor.Model.ZTControls;
using WPFMonitor.DAL.ZTControls;

namespace MonitorSystem.MonitorSystemGlobal
{
    public partial class TPSetProperty : Window
    {
        public TPSetProperty()
        {
            InitializeComponent();
            InitProperty();
        }

        private t_Screen _Screen;
        /// <summary>
        /// 选择的场景
        /// </summary>
        public t_Screen Screen
        {
            get { return _Screen; }
            set {
                cbScreenList.SelectedItem = value;
                _Screen = value; 
            }
        }

        private bool _IsOK=false;
        /// <summary>
        /// 是否点击的OK按钮
        /// </summary>
        public bool IsOK
        {
            get { return _IsOK; }
        }

        private void InitProperty()
        {
            this.cbScreenList.ItemsSource = MonitorControl.ScreenList; ;
            this.cbScreenList.DisplayMemberPath = "ScreenName";
        }
       
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (cbScreenList.SelectedItem == null)
            {
                MessageBox.Show("请选择场景！");
                return;
            }
            _Screen = (t_Screen)cbScreenList.SelectedItem;
            
            _IsOK = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _IsOK = false;
            this.Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
            base.OnClosing(e);
        }
    }
}

