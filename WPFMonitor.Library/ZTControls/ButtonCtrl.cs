using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MonitorSystem.Controls;
using MonitorSystem.MonitorSystemGlobal;
using WPFMonitor.Model;
using WPFMonitor.Model.ZTControls;

namespace MonitorSystem.ZTControls
{
    /// <summary>
    /// 51	ButtonCtrl	2	MyButton.jpg	组态控件	跳转按钮
    /// </summary>
    public class ButtonCtrl : MonitorControl
    {
        public override void DesignMode()
        {
            if (!IsDesignMode)
            {
                AdornerLayer = new Adorner(this);
                AdornerLayer.Selected += OnSelected;

                var menu = new ContextMenu();
                var menuItem = new MenuItem() { Header = "属性" };
                menuItem.Click += PropertyMenuItem_Click;
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

        #region 属性设置

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
                    var vSelectScreen = MonitorControl.ScreenList.Where(a => a.ScreenID == Scrennid);
                    if(vSelectScreen.Count()==0)
                    {
                        continue;
                    }
                    t_Screen t =vSelectScreen.First();

                    ScreenAddShowName mShow = new ScreenAddShowName();
                    mShow.ScreenName = t.ScreenName;
                    mShow.Screen = t;
                    mShow.ScreenShowName = attr[0];
                    listScreenShow.Add(mShow);
                }
            }
            return listScreenShow;
        }

      
        private void PropertyMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var tpp = new TP_ButtonSetProperty(this);
            tpp.Show();
        }
        #endregion

        public override FrameworkElement GetRootControl()
        {
            return this;
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


        public override void SetPropertyValue()
        {
            // 2009-1-17
            foreach (var pro in ListElementProp)
            {
                try
                {

                    string name = pro.PropertyName.Trim().ToUpper();
                    string value = pro.PropertyValue.Trim();
                    if (name == "BackColor".ToUpper())
                    {
                        //this.button1.BackColor = GetColorFromStr(value);
                        BackColor = Common1.StringToColor(value);
                    }
                    else
                    {
                        if (name == "Font".ToUpper())
                        {
                        }
                        else
                        {
                            if (name == "ForeColor".ToUpper())
                            {
                                //this.button1.ForeColor = GetColorFromStr(value);
                                ForeColor = Common1.StringToColor(value);
                            }

                            else if (name == "MyText".ToUpper())
                            {
                                //this.button1.Text = value;
                                MyText = value;
                            }
                            else if (name == "BackImageName".ToUpper())
                            {
                                if (value != null && value.Trim() != "")
                                    BackImageName = value.Trim();
                            }
                            else if (name == "myTextImageRelation".ToUpper())
                            {
                                if (value != null && value.Trim() != "")
                                    myTextImageRelation = GetTextImageRelationFromStr(value.Trim());
                            }
                        }
                    }
                    //this.Invalidate();

                }
                catch
                {
                }

            }
        }

        public override void SetCommonPropertyValue()
        {
            this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
            this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);
            this.Width = (double)ScreenElement.Width;
            this.Height = (double)ScreenElement.Height;
           
            Transparent = ScreenElement.Transparent.Value;
        }

        private string[] _browsableProperties = new[] { "Width", "Height", "Left", "Top", "FontFamily", "FontSize",
            "BackColor", "ForeColor", "MyText", "myTextImageRelation", "BackImageName","Transparent" };
        public override string[] BrowsableProperties
        {
            get { return _browsableProperties; }
            set { _browsableProperties = value; }
        }

        #region 属性
        private static readonly DependencyProperty TransparentProperty = DependencyProperty.Register("Transparent",
        typeof(int), typeof(ButtonCtrl), new PropertyMetadata(0));
        private int _Transparent = 0;
        [DefaultValue(""), Description("透明"), Category("杂项")]
        public int Transparent
        {
            get { return _Transparent; }
            set
            {
                _Transparent = value;
                PaintBackground();
                if (ScreenElement != null)
                    ScreenElement.Transparent = value;
            }
        }


        private static readonly DependencyProperty BackColorProperty =
            DependencyProperty.Register("BackColor",
            typeof(Color), typeof(ButtonCtrl), new PropertyMetadata(Colors.White, new PropertyChangedCallback(BackColor_Changed)));

        public Color BackColor
        {
            get { return (Color)this.GetValue(BackColorProperty); }
            set { this.SetValue(BackColorProperty, value);
                SetAttrByName("BackColor", value); 
                PaintBackground(); 
            }
        }

        private static void BackColor_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            ButtonCtrl ButtonCtrl = (ButtonCtrl)element;
            ButtonCtrl.OnBackColorChanged((Color)e.NewValue, (Color)e.OldValue);
        }

        public void OnBackColorChanged(Color oldValue, Color newValue)
        {
            PaintBackground();
        }

        private static readonly DependencyProperty ForeColorProperty =
            DependencyProperty.Register("ForeColor",
            typeof(Color), typeof(ButtonCtrl), new PropertyMetadata(Colors.Black, new PropertyChangedCallback(ForeColor_Changed)));

        public Color ForeColor
        {
            get { return (Color)this.GetValue(ForeColorProperty); }
            set { this.SetValue(ForeColorProperty, value); SetAttrByName("ForeColor", value); }
        }

        private static void ForeColor_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            ButtonCtrl ButtonCtrl = (ButtonCtrl)element;
            ButtonCtrl.OnForeColorChanged((Color)e.NewValue, (Color)e.OldValue);
        }

        public void OnForeColorChanged(Color oldValue, Color newValue)
        {
            SetForeground();
        }

        public static readonly DependencyProperty MyTextProperty =
            DependencyProperty.Register("MyText", typeof(string), typeof(ButtonCtrl), new PropertyMetadata(string.Empty));

        public string MyText
        {
            get { return (string)_text.GetValue(TextBlock.TextProperty); }
            set { _text.SetValue(TextBlock.TextProperty, value); SetAttrByName("MyText", value); }
        }

        // 摘要:
        //     指定控件上的文本和图像彼此之间的相对位置。
        public enum TextImageRelation
        {
            // 摘要:
            //     指定图像和文本共享控件上的同一空间。
            Overlay = 0,
            //
            // 摘要:
            //     指定图像垂直显示在控件文本的上方。
            ImageAboveText = 1,
            //
            // 摘要:
            //     指定文本垂直显示在控件图像的上方。
            TextAboveImage = 2,
            //
            // 摘要:
            //     指定图像水平显示在控件文本的前方。
            ImageBeforeText = 4,
            //
            // 摘要:
            //     指定文本水平显示在控件图像的前方。
            TextBeforeImage = 8,
        }

        private TextImageRelation GetTextImageRelationFromStr(string s)
        {
            //System .Windows .Forms .TextImageRelation .ImageAboveText;
            //System .Windows .Forms .TextImageRelation.ImageBeforeText
            //System .Windows .Forms .TextImageRelation.Overlay;
            //System .Windows .Forms .TextImageRelation.TextAboveImage;
            //System .Windows .Forms .TextImageRelation.TextBeforeImage;
            if (string.IsNullOrEmpty(s))
            {
                return TextImageRelation.ImageBeforeText;
            }
            TextImageRelation t;
            switch (s.Trim())
            {
                case "ImageAboveText":
                    t = TextImageRelation.ImageAboveText;
                    break;
                case "ImageBeforeText":
                    t = TextImageRelation.ImageBeforeText;
                    break;
                case "Overlay":
                    t = TextImageRelation.Overlay;
                    break;
                case "TextAboveImage":
                    t = TextImageRelation.TextAboveImage;
                    break;
                case "TextBeforeImage":
                    t = TextImageRelation.TextBeforeImage;
                    break;
                default:
                    t = TextImageRelation.ImageBeforeText;
                    break;

            }
            return t;
        }

        private string GetStrFromTextImageRelation(TextImageRelation t)
        {
            string s = null;
            if (t == TextImageRelation.ImageAboveText)
            {
                s = "ImageAboveText";
            }
            else if (t == TextImageRelation.ImageBeforeText)
            {
                s = "ImageBeforeText";
            }
            else if (t == TextImageRelation.Overlay)
            {
                s = "Overlay";
            }
            else if (t == TextImageRelation.TextAboveImage)
            {
                s = "TextAboveImage";
            }
            else if (t == TextImageRelation.TextBeforeImage)
            {
                s = "TextBeforeImage";
            }
            if (string.IsNullOrEmpty(s))
            {
                s = "ImageBeforeText";
                return s;
            }
            else
            {
                return s;
            }
        }

        public static readonly DependencyProperty myTextImageRelationProperty =
            DependencyProperty.Register("myTextImageRelation", typeof(TextImageRelation), typeof(ButtonCtrl), new PropertyMetadata(default(TextImageRelation), new PropertyChangedCallback(myTextImageRelation_Changed)));

        [DefaultValue(""), Description("设置文字字体摆放样式"), Category("我的属性")]
        public TextImageRelation myTextImageRelation
        {
            get { return (TextImageRelation)this.GetValue(myTextImageRelationProperty); }
            set { this.SetValue(myTextImageRelationProperty, value); SetAttrByName("myTextImageRelation", GetStrFromTextImageRelation(value)); }
        }

        private static void myTextImageRelation_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            ButtonCtrl ButtonCtrl = (ButtonCtrl)element;
            ButtonCtrl.OnmyTextImageRelationChanged((TextImageRelation)e.OldValue, (TextImageRelation)e.NewValue);
        }

        public void OnmyTextImageRelationChanged(TextImageRelation oldValue, TextImageRelation newValue)
        {
            SetTextImageRelation();
        }

        public static readonly DependencyProperty BackImageNameProperty =
           DependencyProperty.Register("BackImageName", typeof(string), typeof(ButtonCtrl), new PropertyMetadata("", new PropertyChangedCallback(BackImageName_Changed)));

        [ImageAttribute("PIC")]
        [DefaultValue(""), Description("背景图片名字\r\n注意：\r\n背景图片一定要放在程序\r\n所在目录的\\Upload\\PIC\\下\r\n必须带后缀名"), Category("我的属性")]
        public string BackImageName
        {
            get { return (string)this.GetValue(BackImageNameProperty); }
            set { this.SetValue(BackImageNameProperty, value); SetAttrByName("BackImageName", value); }
        }

        private static void BackImageName_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            ButtonCtrl ButtonCtrl = (ButtonCtrl)element;
            ButtonCtrl.OnBackImageNameChanged((string)e.OldValue, (string)e.NewValue);
        }

        public void OnBackImageNameChanged(string oldValue, string newValue)
        {
            if (System.IO.File.Exists(newValue))
            {
                _image.Source = new BitmapImage(new Uri(newValue, UriKind.Absolute));
            }
        }

        #endregion

        private TextBlock _text = new TextBlock() { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, };
        private Image _image = new Image();
        private Border _button = new Border();
        private Grid _grid = new Grid();
        private readonly DelegateCommand<t_Screen> _command;

        public ButtonCtrl()
        {
            var grid = new Grid();
            this.Content = _button;

            _grid.Children.Add(_image);
            _grid.Children.Add(_text);

            _button.Child = _grid;

            SetForeground();
            PaintBackground();
            SetTextImageRelation();

            _button.MouseRightButtonDown += Button_MouseRightButtonDown;
            this.AddHandler(FrameworkElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(Button_MouseLeftButtonDown), true);
            _command = new DelegateCommand<t_Screen>(ShowName);
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
            menu.VerticalAlignment = VerticalAlignment.Top;
            menu.HorizontalAlignment = HorizontalAlignment.Left;
            var point = e.GetPosition(null);
            menu.HorizontalOffset = point.X;
            menu.VerticalOffset = point.Y;
            menu.IsOpen = true;
        }

        private void ShowName(t_Screen screen)
        {
            //LoadScreen.Load(screen);
            OnLoadScreen(this, screen);
        }

        private void SetTextImageRelation()
        {
            _image.SetValue(Grid.RowProperty, 0);
            _text.SetValue(Grid.RowProperty, 0);
            _image.SetValue(Grid.ColumnProperty, 0);
            _text.SetValue(Grid.ColumnProperty, 0);
            _grid.RowDefinitions.Clear();
            _grid.ColumnDefinitions.Clear();

            switch (myTextImageRelation)
            {
                case TextImageRelation.ImageAboveText:
                    _grid.RowDefinitions.Add(new RowDefinition());
                    _grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1d, GridUnitType.Auto) });

                    _text.SetValue(Grid.RowProperty, 1);
                    break;
                case TextImageRelation.ImageBeforeText:
                    _grid.ColumnDefinitions.Add(new ColumnDefinition());
                    _grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1d, GridUnitType.Auto) });
                    _text.SetValue(Grid.ColumnProperty, 1);
                    break;
                case TextImageRelation.Overlay:
                    break;
                case TextImageRelation.TextAboveImage:
                    _grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1d, GridUnitType.Auto) });
                    _grid.RowDefinitions.Add(new RowDefinition());
                    _image.SetValue(Grid.RowProperty, 1);
                    break;
                case TextImageRelation.TextBeforeImage:
                    _grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1d, GridUnitType.Auto) });
                    _grid.ColumnDefinitions.Add(new ColumnDefinition());
                    _image.SetValue(Grid.ColumnProperty, 1);
                    break;
            }
        }

        private void Text_Changed(object sender, TextChangedEventArgs e)
        {
            if (null != AdornerLayer)
            {
                AdornerLayer.OnSelected();
            }
            SetAttrByName("MyText", MyText);
        }

        private void SetForeground()
        {
                _text.Foreground = new SolidColorBrush(ForeColor);
         
        }

        private void PaintBackground()
        {
            if (_Transparent == 1)
            {
                _grid.Background = new SolidColorBrush();
                _button.Background = new SolidColorBrush();
            }
            else
            {
                _grid.Background = new SolidColorBrush(BackColor);
                _button.Background = new SolidColorBrush(BackColor);
            }
        }
    }
}
