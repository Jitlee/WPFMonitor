using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using MonitorSystem.MonitorSystemGlobal;
using WPFMonitor.Model;
using WPFMonitor.Model.ZTControls;
using WPFMonitor.DAL.ZTControls;

namespace MonitorSystem
{
    [TemplatePart(Name = "BackgroundAdorner", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "ContentAdorner", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "TopLeftAdorner", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "TopCenterAdorner", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "TopRightAdorner", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "CenterLeftAdorner", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "CenterRightAdorner", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "BottomLeftAdorner", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "BottomCenterAdorner", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "BottomRightAdorner", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "ToolTipButton", Type = typeof(Button))]
    public class Adorner : ButtonBase, IDisposable
    {

        #region Fields

        private static readonly List<Adorner> _selectedAdorners = new List<Adorner>();
        public event EventHandler Selected;
        public event EventHandler Unselected;
        private Canvas _parent;
        Point _initialPoint;
        IEnumerable<Point> _originPoints; // 多选移动时  控件的原始坐标数组
        double _initialTop;
        double _initialLeft;
        double _offsetLeft;
        double _offsetTop;
        private bool _associatedIsTapStop;
        private readonly FrameworkElement _associatedElement;
        private Rectangle _backgroundAdorner;
        private Rectangle _contentAdorner;
        private FrameworkElement _topLeftAdorner;
        private FrameworkElement _topCenterAdorner;
        private FrameworkElement _topRightAdorner;
        private FrameworkElement _centerLeftAdorner;
        private FrameworkElement _centerRightAdorner;
        private FrameworkElement _bottomLeftAdorner;
        private FrameworkElement _bottomCenterAdorner;
        private FrameworkElement _bottomRightAdorner;
        private const double MIN_SIZE = 0;

        private Button _toolTipButton;
        public static ToolTipControl CurrenttoolTipControl { get; private set; }

        private bool _isSelected = false;
        #endregion

        #region Properties

        public static List<ScreenElementObj> CopyArray;

        //private static readonly DependencyProperty AllowMoveProperty =
        //  DependencyProperty.Register("AllowMove",
        //  typeof(bool), typeof(Adorner), new PropertyMetadata(true));

        //public bool AllowMove
        //{
        //    get { return (bool)this.GetValue(AllowMoveProperty); }
        //    set { this.SetValue(AllowMoveProperty, value); }
        //}

        //private static void IsAllowMove_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        //{
        //    Adorner adorner = (Adorner)element;
        //    adorner.OnAllowMoveChanged((bool)e.OldValue, (bool)e.NewValue);
        //}

        //public void OnAllowMoveChanged(bool oldValue, bool newValue)
        //{
        //    if (null != _contentAdorner
        //        && null != _backgroundAdorner)
        //    {
        //        if (newValue)
        //        {
        //            //_contentAdorner.IsHitTestVisible = false;
        //            //_backgroundAdorner.IsHitTestVisible = false;
        //            //_contentAdorner.Fill = null;
        //            //_backgroundAdorner.Fill = null;
        //        }
        //        else
        //        {
        //            //_contentAdorner.IsHitTestVisible = true;
        //            //_backgroundAdorner.IsHitTestVisible = true;
        //            //_contentAdorner.Fill = new SolidColorBrush(Colors.Transparent);
        //            //_backgroundAdorner.Fill = new SolidColorBrush(Colors.Transparent);
        //        }
        //    }
        //}

        #region 是否打开

        private static readonly DependencyProperty IsOpenProperty =
           DependencyProperty.Register("IsOpen",
           typeof(bool), typeof(Adorner), new PropertyMetadata(true, IsOpenPropertyChanged));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        private static void IsOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Adorner;
            if (null != element)
            {
                element.OnIsOpenChanged((bool)e.OldValue, (bool)e.NewValue);
            }
        }

        private void OnIsOpenChanged(bool oldValue, bool newValue)
        {
            IsHitTestVisible = newValue;
        }

        #endregion

        private static readonly DependencyProperty IsLockScaleProperty =
            DependencyProperty.Register("IsLockScale",
            typeof(bool), typeof(Adorner), new PropertyMetadata(default(bool), new PropertyChangedCallback(IsLockScale_Changed)));

        public bool IsLockScale
        {
            get { return (bool)this.GetValue(IsLockScaleProperty); }
            set { this.SetValue(IsLockScaleProperty, value); }
        }

        private static void IsLockScale_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            Adorner adorner = (Adorner)element;
            adorner.OnIsLockScaleChanged((bool)e.NewValue, (bool)e.OldValue);
        }

        public void OnIsLockScaleChanged(bool oldValue, bool newValue)
        {
            if (null != _topCenterAdorner
                && null != _centerLeftAdorner
                && null != _centerRightAdorner
                && null != _bottomCenterAdorner)
            {
                if (newValue)
                {
                    _topCenterAdorner.Visibility = Visibility.Collapsed;
                    _centerLeftAdorner.Visibility = Visibility.Collapsed;
                    _centerRightAdorner.Visibility = Visibility.Collapsed;
                    _bottomCenterAdorner.Visibility = Visibility.Collapsed;
                }
                else
                {
                    _topCenterAdorner.Visibility = Visibility.Visible;
                    _centerLeftAdorner.Visibility = Visibility.Visible;
                    _centerRightAdorner.Visibility = Visibility.Visible;
                    _bottomCenterAdorner.Visibility = Visibility.Visible;
                }
            }
        }

        private static readonly DependencyProperty AllToolTipProperty =
          DependencyProperty.Register("AllToolTip",
          typeof(bool), typeof(Adorner), new PropertyMetadata(true, new PropertyChangedCallback(AllToolTip_Changed)));

        public bool AllToolTip
        {
            get { return (bool)this.GetValue(AllToolTipProperty); }
            set { this.SetValue(AllToolTipProperty, value); }
        }

        private static void AllToolTip_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            Adorner adorner = (Adorner)element;
            adorner.OnAllToolTipChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        public void OnAllToolTipChanged(bool oldValue, bool newValue)
        {
            if (null != _toolTipButton)
            {
                _toolTipButton.Visibility = IsSelected && newValue ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private static readonly DependencyProperty EditabledProperty =
            DependencyProperty.Register("Editabled",
            typeof(bool), typeof(Adorner), new PropertyMetadata(default(bool), new PropertyChangedCallback(Editabled_Changed)));

        public bool Editabled
        {
            get { return (bool)this.GetValue(EditabledProperty); }
            set { this.SetValue(EditabledProperty, value); }
        }

        private static void Editabled_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            Adorner adorner = (Adorner)element;
            adorner.OnEditabledChanged((bool)e.NewValue, (bool)e.OldValue);
        }

        public void OnEditabledChanged(bool oldValue, bool newValue)
        {
            if (null != _backgroundAdorner
                && null != _associatedElement)
            {
                if (newValue)
                {
                    _backgroundAdorner.Visibility = Visibility.Collapsed;
                    _backgroundAdorner.PreviewMouseLeftButtonDown -= BackgroundAdorner_MouseLeftButtonDown;
                    _backgroundAdorner.PreviewMouseLeftButtonUp -= BackgroundAdorner_MouseLeftButtonUp;

                    _associatedElement.PreviewMouseLeftButtonDown -= BackgroundAdorner_MouseLeftButtonDown;
                    _associatedElement.PreviewMouseLeftButtonDown += BackgroundAdorner_MouseLeftButtonDown;
                    _associatedElement.PreviewMouseLeftButtonUp -= BackgroundAdorner_MouseLeftButtonUp;
                    _associatedElement.PreviewMouseLeftButtonUp += BackgroundAdorner_MouseLeftButtonUp;
                }
                else
                {
                    _backgroundAdorner.Visibility = Visibility.Visible;

                    _associatedElement.PreviewMouseLeftButtonDown -= BackgroundAdorner_MouseLeftButtonDown;
                    _associatedElement.PreviewMouseLeftButtonUp -= BackgroundAdorner_MouseLeftButtonUp;

                    _backgroundAdorner.PreviewMouseLeftButtonDown -= BackgroundAdorner_MouseLeftButtonDown;
                    _backgroundAdorner.PreviewMouseLeftButtonDown += BackgroundAdorner_MouseLeftButtonDown;
                    _backgroundAdorner.PreviewMouseLeftButtonUp -= BackgroundAdorner_MouseLeftButtonUp;
                    _backgroundAdorner.PreviewMouseLeftButtonUp += BackgroundAdorner_MouseLeftButtonUp;
                }
            }
        }

        private static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected",
            typeof(bool), typeof(Adorner), new PropertyMetadata(default(bool), new PropertyChangedCallback(IsSelected_Changed)));

        public bool IsSelected
        {
            get { return (bool)this.GetValue(IsSelectedProperty); }
            set { this.SetValue(IsSelectedProperty, value); }
        }

        private static void IsSelected_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            Adorner adorner = (Adorner)element;
            adorner.OnIsSelectedChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        public void OnIsSelectedChanged(bool oldValue, bool newValue)
        {
            _isSelected = newValue;
            if (newValue)
            {
                SetSelect();
            }
            else
            {
                SetUnselect();
            }
        }

        #endregion

        #region Methods

        public Adorner(FrameworkElement associatedElement)
        {
            base.DefaultStyleKey = typeof(Adorner);
            base.Visibility = System.Windows.Visibility.Visible;
            _associatedElement = associatedElement;
            if (_associatedElement is ButtonBase)
            {
                var button = _associatedElement as ButtonBase;
                _associatedIsTapStop = button.IsTabStop;
                button.IsTabStop = false;
            }

            _parent = _associatedElement.Parent as Canvas;
            _parent.Children.Add(this);
            this.LayoutUpdated += PopupLayoutUpdated;
            //this.SizeChanged += Adorner_SizeChanged;

            this.SetValue(MinHeightProperty, associatedElement.MinHeight + 5d);
            this.SetValue(MinWidthProperty, associatedElement.MinWidth + 5d);
            //_associatedElement.SizeChanged += _associatedElement_SizeChanged;
            //_popup = new Popup();
            //_popup.Child = this;
            //_popup.IsOpen = true;
            //_popup.LayoutUpdated += PopupLayoutUpdated;

            associatedElement.SizeChanged += associatedElement_SizeChanged;
        }

        private void associatedElement_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SynchroHost();
        }

        //protected override void OnGotFocus(RoutedEventArgs e)
        //{
        //    base.OnGotFocus(e);
        //    SetSelect();
        //}

        //protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        //{
        //    _SetSelect();
        //    base.OnPreviewMouseLeftButtonDown(e);
        //}

        private void _SetSelect()
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                if (_isSelected)
                {
                    RemoveMutiSelected(this);
                }
                else
                {
                    AddMutiSelected(this);
                }
            }
            else
            {
                SetSelect();
            }
        }

        //protected override void OnLostFocus(RoutedEventArgs e)
        //{
        //    base.OnLostFocus(e);
        //}

        private void SetSelect()
        {
            if (!_isSelected)
            {
                if (null != _contentAdorner
                       && null != _topLeftAdorner
                       && null != _topRightAdorner
                       && null != _bottomLeftAdorner
                       && null != _bottomLeftAdorner
                       && null != _bottomRightAdorner
                       && null != _topLeftAdorner
                       && null != _topCenterAdorner
                       && null != _centerLeftAdorner
                       && null != _centerRightAdorner
                       && null != _bottomCenterAdorner
                    && null != _toolTipButton)
                {
                    _contentAdorner.Opacity = 1;
                    _topLeftAdorner.Visibility = Visibility.Visible;
                    _topRightAdorner.Visibility = Visibility.Visible;
                    _bottomLeftAdorner.Visibility = Visibility.Visible;
                    _bottomRightAdorner.Visibility = Visibility.Visible;
                    _toolTipButton.Visibility = AllToolTip ? Visibility.Visible : Visibility.Collapsed;
                    if (!IsLockScale)
                    {
                        _topCenterAdorner.Visibility = Visibility.Visible;
                        _centerLeftAdorner.Visibility = Visibility.Visible;
                        _centerRightAdorner.Visibility = Visibility.Visible;
                        _bottomCenterAdorner.Visibility = Visibility.Visible;
                    }

                    if (!_selectedAdorners.Contains(this))
                    {
                        CancelSelected();
                        _selectedAdorners.Add(this);
                    }
                }
            }
            _isSelected = true;
        }

        private void SetMutiSelect()
        {
            _isSelected = true;
            IsSelected = true;
            if (null != _contentAdorner
                   && null != _topLeftAdorner
                   && null != _topRightAdorner
                   && null != _bottomLeftAdorner
                   && null != _bottomLeftAdorner
                   && null != _bottomRightAdorner
                   && null != _topLeftAdorner
                   && null != _topCenterAdorner
                   && null != _centerLeftAdorner
                   && null != _centerRightAdorner
                   && null != _bottomCenterAdorner
                && null != _toolTipButton)
            {
                _contentAdorner.Opacity = 1;
                _topLeftAdorner.Visibility = Visibility.Visible;
                _topRightAdorner.Visibility = Visibility.Visible;
                _bottomLeftAdorner.Visibility = Visibility.Visible;
                _bottomRightAdorner.Visibility = Visibility.Visible;
                _toolTipButton.Visibility = AllToolTip ? Visibility.Visible : Visibility.Collapsed;
                if (!IsLockScale)
                {
                    _topCenterAdorner.Visibility = Visibility.Visible;
                    _centerLeftAdorner.Visibility = Visibility.Visible;
                    _centerRightAdorner.Visibility = Visibility.Visible;
                    _bottomCenterAdorner.Visibility = Visibility.Visible;
                }
            }
        }

        private void SetUnselect()
        {
            if (_isSelected)
            {
                if (null != _contentAdorner
                    && null != _topLeftAdorner
                    && null != _topRightAdorner
                    && null != _bottomLeftAdorner
                    && null != _bottomLeftAdorner
                    && null != _bottomRightAdorner
                    && null != _topLeftAdorner
                    && null != _topCenterAdorner
                    && null != _centerLeftAdorner
                    && null != _centerRightAdorner
                    && null != _bottomCenterAdorner
                    && null != _toolTipButton)
                {
                    _contentAdorner.Opacity = 0;
                    _topLeftAdorner.Visibility = Visibility.Collapsed;
                    _topRightAdorner.Visibility = Visibility.Collapsed;
                    _bottomLeftAdorner.Visibility = Visibility.Collapsed;
                    _bottomRightAdorner.Visibility = Visibility.Collapsed;
                    _toolTipButton.Visibility = Visibility.Collapsed;
                    if (!IsLockScale)
                    {
                        _topCenterAdorner.Visibility = Visibility.Collapsed;
                        _centerLeftAdorner.Visibility = Visibility.Collapsed;
                        _centerRightAdorner.Visibility = Visibility.Collapsed;
                        _bottomCenterAdorner.Visibility = Visibility.Collapsed;
                    }
                }
            }
            _isSelected = false;
        }

        private void SetMutiUnselect()
        {
            _isSelected = false;
            if (null != _contentAdorner
                && null != _topLeftAdorner
                && null != _topRightAdorner
                && null != _bottomLeftAdorner
                && null != _bottomLeftAdorner
                && null != _bottomRightAdorner
                && null != _topLeftAdorner
                && null != _topCenterAdorner
                && null != _centerLeftAdorner
                && null != _centerRightAdorner
                && null != _bottomCenterAdorner
                && null != _toolTipButton)
            {
                _contentAdorner.Opacity = 0;
                _topLeftAdorner.Visibility = Visibility.Collapsed;
                _topRightAdorner.Visibility = Visibility.Collapsed;
                _bottomLeftAdorner.Visibility = Visibility.Collapsed;
                _bottomRightAdorner.Visibility = Visibility.Collapsed;
                _toolTipButton.Visibility = Visibility.Collapsed;
                if (!IsLockScale)
                {
                    _topCenterAdorner.Visibility = Visibility.Collapsed;
                    _centerLeftAdorner.Visibility = Visibility.Collapsed;
                    _centerRightAdorner.Visibility = Visibility.Collapsed;
                    _bottomCenterAdorner.Visibility = Visibility.Collapsed;
                }
            }
            IsSelected = false;
        }

        public void Dispose()
        {
            if (_associatedElement is MonitorControl)
            {
                var monitorControl = _associatedElement as MonitorControl;
                if (null != monitorControl.ToolTipControl)
                {
                    monitorControl.ToolTipControl.AdornerLayer._backgroundAdorner.Visibility = Visibility.Collapsed;
                    monitorControl.ToolTipControl.AdornerLayer._parent.Children.Remove(monitorControl.ToolTipControl);
                    monitorControl.ToolTipControl.AdornerLayer._parent.Children.Remove(monitorControl.ToolTipControl.AdornerLayer);
                }
            }
            //_backgroundAdorner.SetValue(CustomCursor.CustomProperty, false);
            if (_associatedElement is ButtonBase)
            {
                var button = _associatedElement as ButtonBase;
                button.IsTabStop = _associatedIsTapStop;
            }
            if (null != _contentAdorner)
            {
                _contentAdorner.PreviewMouseLeftButtonDown -= BackgroundAdorner_MouseLeftButtonDown;
                _contentAdorner.PreviewMouseLeftButtonUp -= BackgroundAdorner_MouseLeftButtonUp;
            }
            //_associatedElement.SizeChanged -= _associatedElement_SizeChanged;
            if (null != _parent)
            {
                _parent.Children.Remove(this);
            }
            GC.SuppressFinalize(this);
        }

        public override void OnApplyTemplate()
        {
            _backgroundAdorner = base.GetTemplateChild("BackgroundAdorner") as Rectangle;
            //_backgroundAdorner.IsHitTestVisible = AllowMove;
            OnEditabledChanged(false, Editabled);
            //_backgroundAdorner.SetValue(CustomCursor.CustomProperty, true);

            _contentAdorner = base.GetTemplateChild("ContentAdorner") as Rectangle;
            _contentAdorner.Opacity = 0;
            _contentAdorner.SetValue(MinHeightProperty, _associatedElement.MinHeight);
            _contentAdorner.SetValue(MinWidthProperty, _associatedElement.MinWidth);
            //_contentAdorner.IsHitTestVisible = AllowMove;

            _topLeftAdorner = base.GetTemplateChild("TopLeftAdorner") as FrameworkElement;
            _topLeftAdorner.PreviewMouseLeftButtonDown += TopLeftAdorner_MouseLeftButtonDown;
            _topLeftAdorner.PreviewMouseLeftButtonUp += TopLeftAdorner_MouseLeftButtonUp;
            _topLeftAdorner.Visibility = Visibility.Collapsed;

            _topCenterAdorner = base.GetTemplateChild("TopCenterAdorner") as FrameworkElement;
            _topCenterAdorner.PreviewMouseLeftButtonDown += TopCenterAdorner_MouseLeftButtonDown;
            _topCenterAdorner.PreviewMouseLeftButtonUp += TopCenterAdorner_MouseLeftButtonUp;
            _topCenterAdorner.Visibility = Visibility.Collapsed;

            _topRightAdorner = base.GetTemplateChild("TopRightAdorner") as FrameworkElement;
            _topRightAdorner.PreviewMouseLeftButtonDown += TopRightAdorner_MouseLeftButtonDown;
            _topRightAdorner.PreviewMouseLeftButtonUp += TopRightAdorner_MouseLeftButtonUp;
            _topRightAdorner.Visibility = Visibility.Collapsed;

            _centerLeftAdorner = base. GetTemplateChild("CenterLeftAdorner") as FrameworkElement;
            _centerLeftAdorner.PreviewMouseLeftButtonDown += CenterLeftAdorner_MouseLeftButtonDown;
            _centerLeftAdorner.PreviewMouseLeftButtonUp += CenterLeftAdorner_MouseLeftButtonUp;
            _centerLeftAdorner.Visibility = Visibility.Collapsed;

            _centerRightAdorner = base.GetTemplateChild("CenterRightAdorner") as FrameworkElement;
            _centerRightAdorner.PreviewMouseLeftButtonDown += CenterRightAdorner_MouseLeftButtonDown;
            _centerRightAdorner.PreviewMouseLeftButtonUp += CenterRightAdorner_MouseLeftButtonUp;
            _centerRightAdorner.Visibility = Visibility.Collapsed;

            _bottomLeftAdorner = base.GetTemplateChild("BottomLeftAdorner") as FrameworkElement;
            _bottomLeftAdorner.PreviewMouseLeftButtonDown += BottomLeftAdorner_MouseLeftButtonDown;
            _bottomLeftAdorner.PreviewMouseLeftButtonUp += BottomLeftAdorner_MouseLeftButtonUp;
            _bottomLeftAdorner.Visibility = Visibility.Collapsed;

            _bottomCenterAdorner = base.GetTemplateChild("BottomCenterAdorner") as FrameworkElement;
            _bottomCenterAdorner.PreviewMouseLeftButtonDown += BottomCenterAdorner_MouseLeftButtonDown;
            _bottomCenterAdorner.PreviewMouseLeftButtonUp += BottomCenterAdorner_MouseLeftButtonUp;
            _bottomCenterAdorner.Visibility = Visibility.Collapsed;

            _bottomRightAdorner = base.GetTemplateChild("BottomRightAdorner") as FrameworkElement;
            _bottomRightAdorner.PreviewMouseLeftButtonDown += BottomRightAdorner_MouseLeftButtonDown;
            _bottomRightAdorner.PreviewMouseLeftButtonUp += BottomRightAdorner_MouseLeftButtonUp;
            _bottomRightAdorner.Visibility = Visibility.Collapsed;

            _toolTipButton = base.GetTemplateChild("ToolTipButton") as Button;
            _toolTipButton.Click += new RoutedEventHandler(ToolTipButton_Click);
            _toolTipButton.Visibility = IsSelected && AllToolTip ? Visibility.Visible : Visibility.Collapsed;

            //if (!AllowMove)
            //{
            //    _contentAdorner.Fill = null;
            //    _backgroundAdorner.Fill = new SolidColorBrush(Colors.Transparent);
            //}
        }

        private void ToolTipButton_Click(object sender, EventArgs e)
        {
            ToggleToolTip();
        }

        public void ToggleToolTip()
        {
            var target = _associatedElement as MonitorControl;
            if (null != target)
            {
                var toolTipControl = target.ToolTipControl;
                if (null == toolTipControl)
                {
                    Debug.Assert(null != target.ScreenElement, "MonitorControl 的 ScreenElement 属性不能为null.");
                    var parentID = target.ScreenElement.ElementID;
                    //LoadScreen._DataContext.Load<t_Element>(LoadScreen._DataContext.GetT_ElementsByScreenIDQuery(screenID), LoadToolTipCallback, null);
                    //LoadToolTip(LoadScreen._DataContext.t_Elements.Where(el => el.ParentID == parentID && el.ElementType == "ToolTip").ToList());
                    LoadToolTip(new ElementDA().SelectBy(parentID, "ToolTip"));
                }
                else if (toolTipControl.Visibility == Visibility.Collapsed)
                {
                    if (null != CurrenttoolTipControl)
                    {
                        CurrenttoolTipControl.IsOpen = false;
                        Adorner.RemoveMutiSelected(CurrenttoolTipControl.AdornerLayer);
                    }
                    CurrenttoolTipControl = toolTipControl;

                    ToolTipLayoutUpdate();
                    toolTipControl.IsOpen = true;
                    var children = toolTipControl.ToolTipCanvas.Children.OfType<MonitorControl>().ToList();
                    foreach (var child in children)
                    {
                        var monitor = child as MonitorControl;
                        if (null != monitor)
                        {
                            monitor.ParentControl = toolTipControl;
                            monitor.AllowToolTip = false;
                            monitor.DesignMode();
                            monitor.ClearValue(Canvas.ZIndexProperty);
                            if (null != monitor.AdornerLayer)
                            {
                                monitor.AdornerLayer.AllToolTip = false;
                            }
                        }
                    }
                }
                else
                {
                    toolTipControl.IsOpen = false;
                    CurrenttoolTipControl = null;
                }
            }
        }

        private static t_Control _t_toolTipControl = null;

        /// <summary>
        /// 加载ToolTip子元素
        /// </summary>
        /// <param name="result"></param>
        private void LoadToolTip(List<t_Element> elements)
        {
            if (elements.Count() > 0)
            {
                LoadToolTipProperty(elements);
            }
            else
            {
                //LoadScreen._DataContext.Load(LoadScreen._DataContext.GetT_ControlByTypeQuery(-1), LoadControlsByTypeCallback, null);
                _t_toolTipControl = new ControlDA().SelectBy(-1).FirstOrDefault();
                CreateToolTip(_t_toolTipControl);
            }
        }

        //private void LoadControlsByTypeCallback(LoadOperation<t_Control> result)
        //{
        //    if (!result.HasError)
        //    {
        //        _t_toolTipControl = result.Entities.FirstOrDefault();
        //        CreateToolTip(_t_toolTipControl);
        //    }
        //}

        private void CreateToolTip(t_Control t_control)
        {
            var target = _associatedElement as MonitorControl;
            if (null != target)
            {
                if (t_control != null)
                {
                    target.IsToolTipLoaded = true;
                    var toolTipControl = new ToolTipControl(target);
                    toolTipControl.SetValue(Canvas.ZIndexProperty, 10000);
                    var listElementProperties = new List<t_ElementProperty>();
                    var toolTipControlElement = MonitorControl.OnInitControl(t_control);
                    toolTipControlElement.Transparent = 100;
                    toolTipControlElement.ControlID = -9999;
                    toolTipControlElement.ElementType = "ToolTip";
                    toolTipControlElement.ElementName = t_control.ControlName;
                    toolTipControl.ScreenElement = toolTipControlElement;
                    toolTipControlElement.Width = 300;
                    toolTipControlElement.Height = 200;
                    var elementProperties = new ControlPropertyDA().SelectByControlId(t_control.ControlID);
                    foreach (t_ControlProperty property in elementProperties)
                    {
                        t_ElementProperty tt = new t_ElementProperty();
                        tt.Caption = property.Caption;
                        tt.ElementID = toolTipControlElement.ElementID;
                        tt.PropertyNo = property.PropertyNo;
                        tt.PropertyValue = property.DefaultValue;
                        tt.PropertyName = property.PropertyName;
                        listElementProperties.Add(tt);
                    }
                    toolTipControl.ScreenElement = toolTipControlElement;
                    toolTipControl.ListElementProp = listElementProperties;
                    toolTipControl.ElementState = ElementSate.New;
                    toolTipControl.SetPropertyValue();
                    toolTipControl.SetCommonPropertyValue();
                    _parent.Children.Add(toolTipControl);
                    toolTipControl.DesignMode();
                    toolTipControl.SetPosition();
                    target.ToolTipControl = toolTipControl;
                    if (null != CurrenttoolTipControl)
                    {
                        CurrenttoolTipControl.IsOpen = false;
                        Adorner.RemoveMutiSelected(CurrenttoolTipControl.AdornerLayer);
                    }
                    CurrenttoolTipControl = toolTipControl;
                    toolTipControl.IsOpen = true;
                }
            }
        }

        /// <summary>
        /// 加载ToolTip子元素属性
        /// </summary>
        /// <param name="result"></param>
        private void LoadToolTipProperty(List<t_Element> elements)
        {
            var target = _associatedElement as MonitorControl;
            if (null != target)
            {
                target.IsToolTipLoaded = true;
                var toolTipControlElement = elements.FirstOrDefault(t => t.ControlID == -9999);
                if (null != toolTipControlElement)
                {
                    var toolTipControl = new ToolTipControl(target);
                    toolTipControl.Width = toolTipControlElement.Width.HasValue ? toolTipControlElement.Width.Value : 300d;
                    toolTipControl.Height = toolTipControlElement.Height.HasValue ? toolTipControlElement.Height.Value : 200d;
                    toolTipControl.SetValue(Canvas.ZIndexProperty, 10000);
                    toolTipControl.ScreenElement = toolTipControlElement;
                    toolTipControl.ListElementProp = new ElementPropertyDA().SelectBy(toolTipControlElement.ElementID);
                    toolTipControl.ElementState = ElementSate.Save;
                    toolTipControl.SetPropertyValue();
                    toolTipControl.SetCommonPropertyValue();
                    _parent.Children.Add(toolTipControl);
                    toolTipControl.DesignMode();
                    toolTipControl.SetPosition();
                    target.ToolTipControl = toolTipControl;
                    if (null != CurrenttoolTipControl)
                    {
                        CurrenttoolTipControl.IsOpen = false;
                        Adorner.RemoveMutiSelected(CurrenttoolTipControl.AdornerLayer);
                    }
                    CurrenttoolTipControl = toolTipControl;
                    toolTipControl.IsOpen = true;
                    var childElements = elements.Where(t => t.ControlID != -9999);
                    foreach (var childElement in childElements)
                    {
                        var poperties = new ElementPropertyDA().SelectBy(childElement.ElementID);
                        var monitor = target.OnLoadElement(toolTipControl.ToolTipCanvas, childElement, ElementSate.Save, poperties);
                        if (null != monitor)
                        {
                            monitor.ParentControl = toolTipControl;
                            monitor.DesignMode();
                            monitor.AllowToolTip = false;
                            monitor.ClearValue(Canvas.ZIndexProperty);
                            if (null != monitor.AdornerLayer)
                            {
                                monitor.AdornerLayer.AllToolTip = false;
                            }
                        }
                    }
                }
            }
        }

        private void ToolTipLayoutUpdate()
        {
            var target = _associatedElement as MonitorControl;
            if (null != target && null != target.ToolTipControl)
            {
                target.ToolTipControl.SetPosition();
            }
        }

        private void PopupLayoutUpdated(object sender, EventArgs e)
        {
            this.LayoutUpdated -= PopupLayoutUpdated;
            Layout();
        }

        private void Layout()
        {
            try
            {
                var root = (_associatedElement as MonitorControl).GetRootControl();
                //var beginPoint = _associatedElement.TransformToVisual(_parent).Transform(new Point(0.0, 0.0));
                var beginX = Canvas.GetLeft(_associatedElement);
                var beginY = Canvas.GetTop(_associatedElement);
                var endPoint = _associatedElement.TransformToVisual(_parent).Transform(new Point(root.ActualWidth, root.ActualHeight));
                this._contentAdorner.SetValue(FrameworkElement.WidthProperty, endPoint.X - beginX);
                this._contentAdorner.SetValue(FrameworkElement.HeightProperty, endPoint.Y - beginY);
                var margin = (Thickness)_contentAdorner.GetValue(FrameworkElement.MarginProperty);
                _offsetLeft = margin.Left;
                _offsetTop = margin.Top;
                var left = (double)_associatedElement.GetValue(Canvas.LeftProperty);
                var top = (double)_associatedElement.GetValue(Canvas.TopProperty);
                this.SetValue(Canvas.LeftProperty, left - _offsetLeft);
                this.SetValue(Canvas.TopProperty, top - _offsetTop);
            }
            catch
            {

            }
        }

        //protected override void OnKeyUp(KeyEventArgs e)
        //{
        //    if (e.Key == Key.Delete)
        //    {
        //        _selectedAdorners.ForEach(a =>
        //            {
        //                a._backgroundAdorner.Visibility = Visibility.Collapsed;
        //                a._parent.Children.Remove(a._associatedElement);
        //                a._parent.Children.Remove(a);
        //                this.Dispose();
        //            });
        //        _selectedAdorners.Clear();
        //    }            
        //    base.OnKeyUp(e);
        //}

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    ModifierKeys keys = ModifierKeys.Control;
        //    if (keys== ModifierKeys.Control && e.Key == Key.C)
        //    {
        //        LoadScreen.CopyArray = _selectedAdorners
        //            .Where(a => null != a._associatedElement && a._associatedElement is MonitorControl)
        //            .Select(a => a._associatedElement as MonitorControl);
        //    }
        //    base.OnKeyDown(e);
        //}

        #endregion

        #region Conent Mouse Method

        private void BackgroundAdorner_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _SetSelect();
            var source = sender as FrameworkElement;
            source.CaptureMouse();
            source.MouseMove -= BackgroundAdorner_MouseMove;
            source.MouseMove += BackgroundAdorner_MouseMove;
            _initialPoint = e.GetPosition(_parent);
            _originPoints = _selectedAdorners.Select(a => new Point((double)a.GetValue(Canvas.LeftProperty) + _offsetLeft, (double)a.GetValue(Canvas.TopProperty) + _offsetTop));
            e.Handled = true;
        }

        private void BackgroundAdorner_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var source = sender as FrameworkElement;
            source.ReleaseMouseCapture();
            source.MouseMove -= BackgroundAdorner_MouseMove;
            OnSelected();
            //e.Handled = true;
        }

        private void BackgroundAdorner_MouseMove(object sender, MouseEventArgs e)
        {
            var mousePoint = e.GetPosition(_parent);
            var i = 0;
            var offsetX = mousePoint.X - _initialPoint.X;
            var offsetY = mousePoint.Y - _initialPoint.Y;
            _selectedAdorners.ForEach(a =>
            {
                var point = _originPoints.ElementAt(i);
                a._associatedElement.SetValue(Canvas.LeftProperty, point.X + offsetX);
                a._associatedElement.SetValue(Canvas.TopProperty, point.Y + offsetY);
                a.SetValue(Canvas.LeftProperty, point.X + offsetX - _offsetLeft);
                a.SetValue(Canvas.TopProperty, point.Y + offsetY - _offsetTop);
                i++;
            });
            _initialPoint = mousePoint;
            //e.Handled = true;
        }

        #endregion

        #region Top Left Mouse Method

        private void TopLeftAdorner_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _topLeftAdorner.CaptureMouse();
            _topLeftAdorner.MouseMove -= TopLeftAdorner_MouseMove;
            _topLeftAdorner.MouseMove += TopLeftAdorner_MouseMove;

            _initialPoint = e.GetPosition(_parent);
            _initialTop = (double)this.GetValue(Canvas.TopProperty);
            _initialLeft = (double)this.GetValue(Canvas.LeftProperty);
            e.Handled = true;
        }

        private void TopLeftAdorner_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _topLeftAdorner.ReleaseMouseCapture();
            _topLeftAdorner.MouseMove -= TopLeftAdorner_MouseMove;

            SynchroLayout();
        }

        private void TopLeftAdorner_MouseMove(object sender, MouseEventArgs e)
        {
            var root = (_associatedElement as MonitorControl).GetRootControl();
            var mousePoint = e.GetPosition(_parent);
            var offset = mousePoint.X - _initialPoint.X + mousePoint.Y - _initialPoint.Y;
            var widthRatio = root.ActualWidth / (root.ActualWidth + root.ActualHeight);
            var heightRatio = 1 - widthRatio;
            var offsetX = widthRatio * offset;
            var offsetY = heightRatio * offset;
            var targetWidth = root.ActualWidth - offsetX;
            var targetHeight = root.ActualHeight - offsetY;
            var targetLeft = _initialLeft + offsetX;
            var targetTop = _initialTop + offsetY;
            if (targetWidth < MIN_SIZE || targetHeight < MIN_SIZE)
            {
                targetWidth = MIN_SIZE;
                targetHeight = MIN_SIZE;
                targetLeft = _initialLeft + root.ActualWidth - MIN_SIZE;
                targetTop = _initialTop + root.ActualHeight - MIN_SIZE;
            }

            this._contentAdorner.SetValue(FrameworkElement.WidthProperty, targetWidth);
            this._contentAdorner.SetValue(FrameworkElement.HeightProperty, targetHeight);
            this.SetValue(Canvas.LeftProperty, targetLeft);
            this.SetValue(Canvas.TopProperty, targetTop);
        }

        #endregion

        #region Top Center Mouse Method

        private void TopCenterAdorner_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _topCenterAdorner.CaptureMouse();
            _topCenterAdorner.MouseMove -= TopCenterAdorner_MouseMove;
            _topCenterAdorner.MouseMove += TopCenterAdorner_MouseMove;

            _initialPoint = e.GetPosition(_parent);
            _initialTop = (double)this.GetValue(Canvas.TopProperty);
        }

        private void TopCenterAdorner_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _topCenterAdorner.ReleaseMouseCapture();
            _topCenterAdorner.MouseMove -= TopCenterAdorner_MouseMove;

            SynchroLayout();
        }

        private void TopCenterAdorner_MouseMove(object sender, MouseEventArgs e)
        {
            var root = (_associatedElement as MonitorControl).GetRootControl();
            var mousePoint = e.GetPosition(_parent);
            var offsetY = mousePoint.Y - _initialPoint.Y;
            var targetHeight = root.ActualHeight - offsetY;
            var targetTop = _initialTop + offsetY;
            if (targetHeight < MIN_SIZE)
            {
                targetHeight = MIN_SIZE;
                targetTop = _initialTop + root.ActualHeight - MIN_SIZE;
            }
            else if (targetHeight < MIN_SIZE || targetHeight < root.MinHeight
                || targetHeight > root.MaxHeight)
            {
                return;
            }
            this._contentAdorner.SetValue(FrameworkElement.HeightProperty, targetHeight);
            this.SetValue(Canvas.TopProperty, targetTop);
        }

        #endregion

        #region Top Right Mouse Method

        private void TopRightAdorner_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _topRightAdorner.CaptureMouse();
            _topRightAdorner.MouseMove -= TopRightAdorner_MouseMove;
            _topRightAdorner.MouseMove += TopRightAdorner_MouseMove;

            _initialPoint = e.GetPosition(_parent);
            _initialTop = (double)this.GetValue(Canvas.TopProperty);
            e.Handled = true;
        }

        private void TopRightAdorner_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _topRightAdorner.ReleaseMouseCapture();
            _topRightAdorner.MouseMove -= TopRightAdorner_MouseMove;

            SynchroLayout();
        }

        private void TopRightAdorner_MouseMove(object sender, MouseEventArgs e)
        {
            var root = (_associatedElement as MonitorControl).GetRootControl();
            var mousePoint = e.GetPosition(_parent);
            var offsetX = mousePoint.X - _initialPoint.X;
            var offsetY = mousePoint.Y - _initialPoint.Y;
            var widthRatio = root.ActualWidth / (root.ActualWidth + root.ActualHeight);
            var heightRatio = 1 - widthRatio;
            var offset = offsetX - offsetY;
            var targetWidth = root.ActualWidth + offset * widthRatio;
            var targetHeight = root.ActualHeight + offset * heightRatio;
            var targetTop = _initialTop - offset * heightRatio;
            if (targetWidth < MIN_SIZE || targetHeight < MIN_SIZE)
            {
                targetWidth = MIN_SIZE;
                targetHeight = MIN_SIZE;
                targetTop = _initialTop + _associatedElement.ActualHeight - MIN_SIZE;
            }
            this._contentAdorner.SetValue(FrameworkElement.WidthProperty, targetWidth);
            this._contentAdorner.SetValue(FrameworkElement.HeightProperty, targetHeight);
            this.SetValue(Canvas.TopProperty, targetTop);
        }

        #endregion

        #region Center Left Mouse Method

        private void CenterLeftAdorner_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _centerLeftAdorner.CaptureMouse();
            _centerLeftAdorner.MouseMove -= CenterLeftAdorner_MouseMove;
            _centerLeftAdorner.MouseMove += CenterLeftAdorner_MouseMove;

            _initialPoint = e.GetPosition(_parent);
            _initialLeft = (double)this.GetValue(Canvas.LeftProperty);
            e.Handled = true;
        }

        private void CenterLeftAdorner_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _centerLeftAdorner.ReleaseMouseCapture();
            _centerLeftAdorner.MouseMove -= CenterLeftAdorner_MouseMove;

            SynchroLayout();
        }

        private void CenterLeftAdorner_MouseMove(object sender, MouseEventArgs e)
        {
            var root = (_associatedElement as MonitorControl).GetRootControl();
            var mousePoint = e.GetPosition(_parent);
            var offsetX = mousePoint.X - _initialPoint.X;
            var targetWidth = root.ActualWidth - offsetX;
            var targetLeft = _initialLeft + offsetX;
            if (targetWidth < MIN_SIZE)
            {
                targetWidth = MIN_SIZE;
                targetLeft = _initialLeft + root.ActualWidth - MIN_SIZE;
            }
            else if (targetWidth < MIN_SIZE || targetWidth < root.MinWidth
                || targetWidth > root.MaxWidth)
            {
                return;
            }
            this._contentAdorner.SetValue(FrameworkElement.WidthProperty, targetWidth);
            this.SetValue(Canvas.LeftProperty, targetLeft);
        }

        #endregion

        #region Center Right Mouse Method

        private void CenterRightAdorner_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _centerRightAdorner.CaptureMouse();
            _centerRightAdorner.MouseMove -= CenterRightAdorner_MouseMove;
            _centerRightAdorner.MouseMove += CenterRightAdorner_MouseMove;

            _initialPoint = e.GetPosition(_parent);
            _initialLeft = (double)this.GetValue(Canvas.LeftProperty);
            e.Handled = true;
        }

        private void CenterRightAdorner_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _centerRightAdorner.ReleaseMouseCapture();
            _centerRightAdorner.MouseMove -= CenterRightAdorner_MouseMove;

            SynchroLayout();
        }

        private void CenterRightAdorner_MouseMove(object sender, MouseEventArgs e)
        {
            var root = (_associatedElement as MonitorControl).GetRootControl();
            var mousePoint = e.GetPosition(_parent);
            var offsetX = mousePoint.X - _initialPoint.X;
            var targetWidth = root.ActualWidth + offsetX;
            if (targetWidth < MIN_SIZE)
            {
                targetWidth = MIN_SIZE;
            }
            else if (targetWidth < MIN_SIZE || targetWidth < root.MinWidth
                || targetWidth > root.MaxWidth)
            {
                return;
            }
            this._contentAdorner.SetValue(FrameworkElement.WidthProperty, targetWidth);
        }

        #endregion

        #region Bottom Left Mouse Method

        private void BottomLeftAdorner_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _bottomLeftAdorner.CaptureMouse();
            _bottomLeftAdorner.MouseMove -= BottomLeftAdorner_MouseMove;
            _bottomLeftAdorner.MouseMove += BottomLeftAdorner_MouseMove;

            _initialPoint = e.GetPosition(_parent);
            _initialLeft = (double)this.GetValue(Canvas.LeftProperty);
            e.Handled = true;
        }

        private void BottomLeftAdorner_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _bottomLeftAdorner.ReleaseMouseCapture();
            _bottomLeftAdorner.MouseMove -= BottomLeftAdorner_MouseMove;

            SynchroLayout();
        }

        private void BottomLeftAdorner_MouseMove(object sender, MouseEventArgs e)
        {
            var root = (_associatedElement as MonitorControl).GetRootControl();
            var mousePoint = e.GetPosition(_parent);
            var offsetX = mousePoint.X - _initialPoint.X;
            var offsetY = mousePoint.Y - _initialPoint.Y;
            var widthRatio = root.ActualWidth / (root.ActualWidth + root.ActualHeight);
            var heightRatio = 1 - widthRatio;
            var offset = offsetY - offsetX;
            var targetWidth = root.ActualWidth + offset * widthRatio;
            var targetHeight = root.ActualHeight + offset * heightRatio;
            var targetLeft = _initialLeft - offset * widthRatio;
            if (targetWidth < MIN_SIZE || targetHeight < MIN_SIZE)
            {
                targetWidth = MIN_SIZE;
                targetHeight = MIN_SIZE;
                targetLeft = _initialLeft + root.ActualWidth - MIN_SIZE;
            }

            this._contentAdorner.SetValue(FrameworkElement.WidthProperty, targetWidth);
            this._contentAdorner.SetValue(FrameworkElement.HeightProperty, targetHeight);
            this.SetValue(Canvas.LeftProperty, targetLeft);
        }

        #endregion

        #region Bottom Center Mouse Method

        private void BottomCenterAdorner_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _bottomCenterAdorner.CaptureMouse();
            _bottomCenterAdorner.MouseMove -= BottomCenterAdorner_MouseMove;
            _bottomCenterAdorner.MouseMove += BottomCenterAdorner_MouseMove;

            _initialPoint = e.GetPosition(_parent);
            e.Handled = true;
        }

        private void BottomCenterAdorner_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _bottomCenterAdorner.ReleaseMouseCapture();
            _bottomCenterAdorner.MouseMove -= BottomCenterAdorner_MouseMove;

            SynchroLayout();
        }

        private void BottomCenterAdorner_MouseMove(object sender, MouseEventArgs e)
        {
            var root = (_associatedElement as MonitorControl).GetRootControl();
            var mousePoint = e.GetPosition(_parent);
            var offsetY = mousePoint.Y - _initialPoint.Y;
            var targetHeight = root.ActualHeight + offsetY;
            if (targetHeight < MIN_SIZE)
            {
                targetHeight = MIN_SIZE;
            }
            else if (targetHeight < MIN_SIZE || targetHeight < root.MinHeight
                || targetHeight > root.MaxHeight)
            {
                return;
            }
            this._contentAdorner.SetValue(FrameworkElement.HeightProperty, targetHeight);
        }

        #endregion

        #region Bottom Right Mouse Method

        private void BottomRightAdorner_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _bottomRightAdorner.CaptureMouse();
            _bottomRightAdorner.MouseMove -= BottomRightAdorner_MouseMove;
            _bottomRightAdorner.MouseMove += BottomRightAdorner_MouseMove;

            _initialPoint = e.GetPosition(_parent);
            _initialTop = (double)this.GetValue(Canvas.TopProperty);
            _initialLeft = (double)this.GetValue(Canvas.LeftProperty);
            e.Handled = true;
        }

        private void BottomRightAdorner_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _bottomRightAdorner.ReleaseMouseCapture();
            _bottomRightAdorner.MouseMove -= BottomRightAdorner_MouseMove;

            SynchroLayout();
        }

        private void BottomRightAdorner_MouseMove(object sender, MouseEventArgs e)
        {
            var root = (_associatedElement as MonitorControl).GetRootControl();
            var mousePoint = e.GetPosition(_parent);
            var offset = mousePoint.X - _initialPoint.X + mousePoint.Y - _initialPoint.Y;
            var widthRatio = root.ActualWidth / (root.ActualWidth + root.ActualHeight);
            var heightRatio = 1 - widthRatio;
            var offsetX = widthRatio * offset;
            var offsetY = heightRatio * offset;
            var targetWidth = root.ActualWidth + offsetX;
            var targetHeight = root.ActualHeight + offsetY;
            if (targetWidth < MIN_SIZE || targetHeight < MIN_SIZE)
            {
                targetWidth = MIN_SIZE;
                targetHeight = MIN_SIZE;
            }

            this._contentAdorner.SetValue(FrameworkElement.WidthProperty, targetWidth);
            this._contentAdorner.SetValue(FrameworkElement.HeightProperty, targetHeight);
        }

        #endregion

        public void OnSelected()
        {
            if (null != Selected)
            {
                Selected(this, EventArgs.Empty);
                ToolTipLayoutUpdate();
            }
        }

        private void SynchroLayout()
        {
            this._associatedElement.SetValue(FrameworkElement.WidthProperty, this._contentAdorner.ActualWidth);
            this._associatedElement.SetValue(FrameworkElement.HeightProperty, this._contentAdorner.ActualHeight);
            this._associatedElement.SetValue(Canvas.LeftProperty, (double)this.GetValue(Canvas.LeftProperty) + _offsetLeft);
            this._associatedElement.SetValue(Canvas.TopProperty, (double)this.GetValue(Canvas.TopProperty) + _offsetTop);

            OnSelected();
        }

        public void SynchroHost()
        {
            if (null != _contentAdorner)
            {
                _contentAdorner.SetValue(FrameworkElement.WidthProperty, _associatedElement.ActualWidth);
                _contentAdorner.SetValue(FrameworkElement.HeightProperty, _associatedElement.ActualHeight);
                this.SetValue(Canvas.LeftProperty, (double)_associatedElement.GetValue(Canvas.LeftProperty) - _offsetLeft);
                this.SetValue(Canvas.TopProperty, (double)_associatedElement.GetValue(Canvas.TopProperty) - _offsetTop);
            }
        }

        public void SynchroHost(double x, double y)
        {
            this.SetValue(Canvas.LeftProperty, x - _offsetLeft);
            this.SetValue(Canvas.TopProperty, y - _offsetTop);
        }

        public static void CancelSelected()
        {
            var array = new Adorner[_selectedAdorners.Count];
            _selectedAdorners.CopyTo(array);
            foreach (var a in array)
            {
                a.SetMutiUnselect();
                if (null != a.Unselected)
                {
                    a.Unselected(a, EventArgs.Empty);
                }
                _selectedAdorners.Remove(a);
            }
            array = null;
        }

        public static void AddMutiSelected(Adorner adorner)
        {
            if(!_selectedAdorners.Contains(adorner))
            {
                _selectedAdorners.Add(adorner);
                adorner.SetMutiSelect();
            }
        }

        public static void RemoveMutiSelected(Adorner adorner)
        {
            if (_selectedAdorners.Contains(adorner))
            {
                _selectedAdorners.Remove(adorner);
                adorner.SetMutiUnselect();
            }
        }

        public static void DeleteAll()
        {
             _selectedAdorners.ForEach(a =>
                    {
                        a._backgroundAdorner.Visibility = Visibility.Collapsed;
                        a._parent.Children.Remove(a._associatedElement);
                        a._parent.Children.Remove(a);
                        a.Dispose();
                    });
             _selectedAdorners.Clear();
        }

        public static void CopyAll()
        {
            CopyArray = _selectedAdorners
                    .Where(a => null != a._associatedElement && a._associatedElement is MonitorControl)
                    .Select(a => {
                        var monitorControl = a._associatedElement as MonitorControl;
                        var obj = new ScreenElementObj();
                        obj.ElementClone(monitorControl, (int)monitorControl.ActualWidth, (int)monitorControl.ActualHeight);
                        return obj;
                    }).ToList();
        }

        public static void SelectAll()
        {
            foreach (var monitorControl in MonitorControl.OnGetMainCanvas().Children.OfType<MonitorControl>())
            {
                AddMutiSelected(monitorControl.AdornerLayer);
            }
        }

        public static void MoveLeft(double offset)
        {
            Move(Canvas.LeftProperty, -offset);
        }

        public static void MoveRight(double offset)
        {
            Move(Canvas.LeftProperty, offset);
        }

        public static void MoveUp(double offset)
        {
            Move(Canvas.TopProperty, -offset);
        }

        public static void MoveDown(double offset)
        {
            Move(Canvas.TopProperty, offset);
        }

        private static void Move(DependencyProperty property, double offset)
        {
            _selectedAdorners.ForEach(a =>
            {
                var origin = (double)a.GetValue(property);
                a.SetValue(property, origin + offset);
                origin = (double)a._associatedElement.GetValue(property);
                a._associatedElement.SetValue(property, origin + offset);
            });
        }
    }
}
