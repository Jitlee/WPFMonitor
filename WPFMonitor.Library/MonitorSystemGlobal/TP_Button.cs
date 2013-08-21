using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WPFMonitor.Model;
using WPFMonitor.Model.ZTControls;

namespace MonitorSystem.MonitorSystemGlobal
{
    public class ScreenAddShowName
    {
        public string ScreenShowName { get; set; }
        public string ScreenName { get; set; }

        public t_Screen Screen { get; set; }
    }

    public class TP_Button : MonitorControl
    {

        Border _mRect = new Border();
        TextBlock m_txt = new TextBlock();
        private readonly DelegateCommand<t_Screen> _command;
        public TP_Button()
        {
            //_mRect.StrokeThickness = 1;
            //_mRect.Stroke = new SolidColorBrush(Colors.Black);
            //_mRect.Fill = new SolidColorBrush(Colors.White);
            _mRect.Child = m_txt;
            Content = _mRect;
            //_mRect.IsReadOnly = true;
            _mRect.MouseRightButtonDown += Button_MouseRightButtonDown;
            this.AddHandler(FrameworkElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(Button_MouseLeftButtonDown), true);
            _command = new DelegateCommand<t_Screen>(ShowName);
        }
        private void ShowName(t_Screen screen)
        {
            base.OnLoadScreen(this, screen);
        }
        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowMenu(e);
        }

        private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowMenu(e);
            e.Handled = true;
        }
        private void ShowMenu(MouseButtonEventArgs e)
        {
            var list = GetChildScreenObj();
            if (list == null)
                return;
            var menu = new ContextMenu();
            menu.Items.Clear();
            foreach (var screen in list)
            {
                menu.Items.Add(new MenuItem() { Header = screen.ScreenName, Command = _command, CommandParameter = screen.Screen, });
            }
            menu.IsOpen = true;
			e.Handled = true;
        }
        #region 属性
        private string[] m_BrowsableProperties = new string[] { "Left", "Top", "Width", "Height", "FontFamily", "FontSize", "Foreground","Transparent",
            "HaveEdge","Title"};

        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }

        public override event EventHandler Selected;
		
		public override event EventHandler Unselected;

		private void OnUnselected(object sender, EventArgs e)
		{
			if(null != Unselected)
			{
				Unselected(this, RoutedEventArgs.Empty);
			}
		}


        private static readonly DependencyProperty TransparentProperty =
            DependencyProperty.Register("Transparent",
            typeof(int), typeof(TP_Button), new PropertyMetadata(0));
        private int _Transparent;
        [DefaultValue(""), Description("透明"), Category("杂项")]
        public int Transparent
        {
            get { return _Transparent; }
            set
            {
                if (value == 1)
                {
                    _mRect.Background = new SolidColorBrush();
                }
                else
                {
                    _mRect.Background = new SolidColorBrush(Colors.White);
                }

                _Transparent = value;
                if (ScreenElement != null)
                    ScreenElement.Transparent = value;
            }

        }

        private static readonly DependencyProperty HaveEdgeProperty =
          DependencyProperty.Register("HaveEdge",
          typeof(bool), typeof(TP_Button), new PropertyMetadata(false));
        private bool _HaveEdge;
        public bool HaveEdge
        {
            get { return _HaveEdge; }
            set
            {
                if (value)
                {
                    _mRect.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    _mRect.BorderBrush = new SolidColorBrush();
                }

                _HaveEdge = value;
                SetAttrByName("HaveEdge", value);
            }
        }
        
        private static readonly DependencyProperty TitleProperty =
          DependencyProperty.Register("Title",
          typeof(string), typeof(TP_Button), new PropertyMetadata(""));
         private string _Title;
         public string Title
         {
             get { return _Title; }
             set
             {
                 m_txt.Text = value;
                 _Title = value;
                 SetAttrByName("Title", value);
             }
         }
            
        #endregion



        #region 场景,TP属性
        /// <summary>
        /// 将对象的ScreenElement的ChildScreenID解析为场景 
        /// </summary>
        /// <returns></returns>
        public override ObservableCollection<ScreenAddShowName> GetChildScreenObj()
        {
            string mScreenID = base.ScreenElement.ChildScreenID;
            if (mScreenID == "0")
            {
                return null;
            }
            ObservableCollection<ScreenAddShowName> listScreenShow = new ObservableCollection<ScreenAddShowName>();
            string[] attrS = mScreenID.Split(';');
            foreach (string str in attrS)
            {
                mScreenID = str.Replace(";", "");
                string[] attr = mScreenID.Split('#');
                if (attr.Length == 2)
                {
                    int Scrennid = Convert.ToInt32(attr[1]);
                    t_Screen t = ScreenList.Single(a => a.ScreenID == Scrennid);

                    ScreenAddShowName mShow = new ScreenAddShowName();
                    mShow.ScreenName = t.ScreenName ;
                    mShow.Screen = t;
                    mShow.ScreenShowName = attr[0] ;
                    listScreenShow.Add(mShow);
                }
            }
            return listScreenShow;
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="litobj"></param>
        public override void SetChildScreen(ObservableCollection<ScreenAddShowName> litobj)
        {
            string strScreen = "";
            if (litobj == null)
                strScreen = "0";
            else if (litobj.Count == 0)
                strScreen = "0";
            else
            {
                foreach (ScreenAddShowName obj in litobj)
                {
                    strScreen += string.Format("{0}#{1};", obj.ScreenShowName, obj.Screen.ScreenID);
                }
            }
            ScreenElement.ChildScreenID = strScreen;
        }
        #endregion

      
        public override void DesignMode()
        {
            if (!IsDesignMode)
            {
                AdornerLayer = new Adorner(this);
                var menu = new ContextMenu();
                var menuItem = new MenuItem() { Header = "属性" };
                menuItem.Click += PropertyMenuItemSet_Click;
                AdornerLayer.Selected += OnSelected;
                menu.Items.Add(menuItem);
                AdornerLayer.SetValue(ContextMenuService.ContextMenuProperty, menu);
            }
        }

        public override void UnDesignMode()
        {
            if (IsDesignMode)
            {
                AdornerLayer.Selected -= OnSelected;
                AdornerLayer.ClearValue(ContextMenuService.ContextMenuProperty);
                AdornerLayer.Dispose();
                AdornerLayer = null;
            }
        }

        private void OnSelected(object sender, EventArgs e)
        {
            if (null != Selected)
            {
                Selected(this, RoutedEventArgs.Empty);
            }
        }

        TP_ButtonSetProperty tppSet; 
        private void PropertyMenuItemSet_Click(object sender, RoutedEventArgs e)
        {
            tppSet = new TP_ButtonSetProperty(this);
            tppSet.Owner = Common1.MainWin;
            tppSet.Show();
        }

        public override void SetPropertyValue()
        {
            foreach (t_ElementProperty pro in ListElementProp)
            {
                if (pro.PropertyName == "Title")
                {
                    Title = pro.PropertyValue;
                }
                else if (pro.PropertyName == "HaveEdge")
                {
                    HaveEdge =Common1.ConvertToBool( pro.PropertyValue);
                }
            }
        }

        public override void SetCommonPropertyValue()
        {
            this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
            this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);
            Transparent = ScreenElement.Transparent.Value;
            this.Width = (double)ScreenElement.Width;
            this.Height = (double)ScreenElement.Height;
        }

        public override FrameworkElement GetRootControl()
        {
            return this;
        }
    }
}
