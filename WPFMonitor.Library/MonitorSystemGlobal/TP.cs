using System;
using System.Collections.Generic;
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
    public class TP : MonitorControl
    {

        public TP()
        {
            Content = _image;
            Stretch = Stretch.Fill;

            this.MouseLeftButtonUp +=new MouseButtonEventHandler(TP_MouseLeftButtonUp);
        }

        public void TP_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("");
           t_Screen ts= GetChildScreenID();
		   OnLoadScreen(this, ts);
        }


        #region 属性
        private string[] m_BrowsableProperties = new string[] { "Left", "Top", "Width", "Height", "FontFamily", "FontSize","Translate", "Foreground",
            "Transparent"};
        private static readonly DependencyProperty TransparentProperty =
       
            DependencyProperty.Register("Transparent",
        typeof(int), typeof(TP), new PropertyMetadata(0));
        private int _Transparent;
        [DefaultValue(""), Description("透明"), Category("杂项")]
        public int Transparent
        {
            get { return _Transparent; }
            set
            {
                _Transparent = value;
                if (ScreenElement != null)
                    ScreenElement.Transparent = value;
            }
        }


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

        private Image _image = new Image();

        private static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source",
            typeof(ImageSource), typeof(TP), new PropertyMetadata(null));

        public  ImageSource Source
        {
            get { return (ImageSource)_image.GetValue(Image.SourceProperty); }
            set { _image.SetValue(Image.SourceProperty, value); }
        }

        private static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register("Stretch",
            typeof(Stretch), typeof(TP), new PropertyMetadata(Stretch.Fill));

        public Stretch Stretch
        {
            get { return (Stretch)_image.GetValue(Image.StretchProperty); }
            set { _image.SetValue(Image.StretchProperty, value); }
        }
        #endregion

        #region 场景,TP属性
        /// <summary>
        /// 将对象的ScreenElement的ChildScreenID解析为场景 
        /// </summary>
        /// <returns></returns>
        private t_Screen GetChildScreenID()
        {
            string mScreenID = base.ScreenElement.ChildScreenID;
            if (string.IsNullOrEmpty(mScreenID) || mScreenID == "0")
            {
                return null;
            }
            mScreenID = mScreenID.Replace(";", "");
            string[] attr=mScreenID.Split('#');
            if (attr.Length == 2)
            {
                int Scrennid=Convert.ToInt32(attr[1]);
                t_Screen t = ScreenList.Single(a => a.ScreenID == Scrennid);
                //return LoadScreen.listScreen.Where().First();
                return t;
            }
            return null;
        }
        #endregion

      
        public override void DesignMode()
        {
            if (!IsDesignMode)
            {
                AdornerLayer = new Adorner(this);
                AdornerLayer.Selected += OnSelected;

                var menu = new ContextMenu();
                var menuItem = new MenuItem() { Header = "属性" };
                menuItem.Click += PropertyMenuItemSet_Click;
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

        TPSetProperty tppSet = new TPSetProperty();
        private void PropertyMenuItemSet_Click(object sender, RoutedEventArgs e)
        {
            tppSet.Closing += tppSet_Closing;
            tppSet.Screen = GetChildScreenID();
            tppSet.Show();
        }

        protected void tppSet_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tppSet.IsOK)
            {
                this.ScreenElement.ChildScreenID = string.Format("{0}#{1};", tppSet.Screen.ScreenName,
                    tppSet.Screen.ScreenID);
                //MessageBox.Show(tpp.Screen.ScreenName);
            }
        }

        public override void SetPropertyValue()
        {
           
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
