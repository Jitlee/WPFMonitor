using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Effects;

namespace MonitorSystem.Controls
{
    public partial class FloatPanel : ContentControl
    {
        #region 变量

        private Point _originPoint;
        private double _originX;
        private double _originY;
        private double _originWidth;
        private double _originHeight;
        private readonly Storyboard _closeStoryBoard;
        private readonly Storyboard _showStoryBoard;

        #endregion

        #region 属性

        #region Title

        public static readonly DependencyProperty TitleProperty =
           DependencyProperty.Register("Title", typeof(string), typeof(FloatPanel), new PropertyMetadata("FloatPanel", TitlePropertyChanged));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        private static void TitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var elment = d as FloatPanel;
            if (null != elment)
            {
                elment.TitleOpened_Changed((string)e.OldValue, (string)e.NewValue);
            }
        }

        private void TitleOpened_Changed(string oldValue, string newValue)
        {
            this.TitleTextBlock.Text = newValue;
        }

        #endregion

        #region IsOpened

        public static readonly DependencyProperty IsOpenedProperty =
            DependencyProperty.Register("IsOpened", typeof(bool), typeof(FloatPanel), new PropertyMetadata(false, IsOpenedPropertyChanged));

        public bool IsOpened
        {
            get { return (bool)GetValue(IsOpenedProperty); }
            set { SetValue(IsOpenedProperty, value); }
        }

        private static void IsOpenedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var elment = d as FloatPanel;
            if (null != elment)
            {
                elment.IsOpened_Changed((bool)e.OldValue, (bool)e.NewValue);
            }
        }

        private void IsOpened_Changed(bool oldValue, bool newValue)
        {
            if (newValue)
            {
                _showStoryBoard.Begin();
            }
            else
            {
                _closeStoryBoard.Begin();
            }
        }

        #endregion

        #region Height

        public static new readonly DependencyProperty HeightProperty =
            DependencyProperty.Register("Height", typeof(double), typeof(FloatPanel), new PropertyMetadata(300d, HeightPropertyChanged));

        public new double Height
        {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        private static void HeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var elment = d as FloatPanel;
            if (null != elment)
            {
                elment.Height_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void Height_Changed(double oldValue, double newValue)
        {
            if (newValue < 0d)
            {
                newValue = 0d;
            }
            LayoutRoot.SetValue(UserControl.HeightProperty, newValue);
        }

        #endregion

        #region Width

        public static new readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(double), typeof(FloatPanel), new PropertyMetadata(300d, WidthPropertyChanged));

        public new double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        private static void WidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var elment = d as FloatPanel;
            if (null != elment)
            {
                elment.Width_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void Width_Changed(double oldValue, double newValue)
        {
            if (newValue < 0d)
            {
                newValue = 0d;
            }
            LayoutRoot.SetValue(UserControl.WidthProperty, newValue);
        }

        #endregion

        #region Left

        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register("Left", typeof(double), typeof(FloatPanel), new PropertyMetadata(0d, LeftPropertyChanged));

        public double Left
        {
            get { return (double)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        private static void LeftPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var elment = d as FloatPanel;
            if (null != elment)
            {
                elment.Left_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void Left_Changed(double oldValue, double newValue)
        {
            //var beginPoint = this.TransformToVisual(null).Transform(new Point(0.0, 0.0));
            Popup.HorizontalOffset = newValue;
        }

        #endregion

        #region Top

        public static readonly DependencyProperty TopProperty =
           DependencyProperty.Register("Top", typeof(double), typeof(FloatPanel), new PropertyMetadata(0d, TopPropertyChanged));

        public double Top
        {
            get { return (double)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

        private static void TopPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var elment = d as FloatPanel;
            if (null != elment)
            {
                elment.Top_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void Top_Changed(double oldValue, double newValue)
        {
            //var beginPoint = this.TransformToVisual(null).Transform(new Point(0.0, 0.0));
            Popup.VerticalOffset = newValue;
        }

        #endregion

        #region Child

        public static readonly DependencyProperty ChildProperty =
            DependencyProperty.Register("Child", typeof(UIElement), typeof(FloatPanel), new PropertyMetadata(null, ChildPropertyChanged));

        public UIElement Child
        {
            get { return (UIElement)GetValue(ChildProperty); }
            set { SetValue(ChildProperty, value); }
        }

        private static void ChildPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as FloatPanel).ContentBorder.Child = e.NewValue as UIElement;
        }

        #endregion

        #endregion

        #region 构造方法

        public FloatPanel()
        {
            InitializeComponent();
            _showStoryBoard = Resources["ShowStoryboard"] as Storyboard;
            _closeStoryBoard = Resources["CloseStoryboard"] as Storyboard;
            if (!IsOpened)
            {
                Close();
            }
        }

        #endregion

        #region 事件
        
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion

        #region 公共方法

        public void Show()
        {
            SetValue(IsOpenedProperty, true);
            IsOpened_Changed(false, true);
        }

        public void Close()
        {
            SetValue(IsOpenedProperty, false);
            IsOpened_Changed(true, false);
        }

        #endregion

        #region 中

        private void BackgroundRectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BackgroundRectangle.CaptureMouse();

            _originPoint = e.GetPosition(null);
            _originX = this.Left;
            _originY = this.Top;
            BackgroundRectangle.MouseMove -= BackgroundRectangle_MouseMove;
            BackgroundRectangle.MouseMove += BackgroundRectangle_MouseMove;
            BackgroundRectangle.MouseLeftButtonUp -= BackgroundRectangle_MouseLeftButtonUp;
            BackgroundRectangle.MouseLeftButtonUp += BackgroundRectangle_MouseLeftButtonUp;
        }

        private void BackgroundRectangle_MouseMove(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition(null);
            var offsetX = point.X - _originPoint.X;
            var offsetY = point.Y - _originPoint.Y;
            this.Left = _originX + offsetX;
            this.Top = _originY + offsetY;
        }

        private void BackgroundRectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BackgroundRectangle.ReleaseMouseCapture();
            BackgroundRectangle.MouseMove -= BackgroundRectangle_MouseMove;
            BackgroundRectangle.MouseLeftButtonUp -= BackgroundRectangle_MouseLeftButtonUp;
        }

        #endregion

        #region 左上

        private void LeftTopRectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LeftTopRectangle.CaptureMouse();

            _originPoint = e.GetPosition(null);
            _originX = this.Left;
            _originY = this.Top;
            _originWidth = this.Width;
            _originHeight = this.Height;
            LeftTopRectangle.MouseMove -= LeftTopRectangle_MouseMove;
            LeftTopRectangle.MouseMove += LeftTopRectangle_MouseMove;
            LeftTopRectangle.MouseLeftButtonUp -= LeftTopRectangle_MouseLeftButtonUp;
            LeftTopRectangle.MouseLeftButtonUp += LeftTopRectangle_MouseLeftButtonUp;

            e.Handled = true;
        }

        private void LeftTopRectangle_MouseMove(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition(null);
            var offsetX = point.X - _originPoint.X;
            var offsetY = point.Y - _originPoint.Y;
            //this.Left = _originX + offsetX;
            //this.Top = _originY + offsetY;
            //this.Width = _originWidth - offsetX;
            //this.Height = _originHeight - offsetY;

            var width = _originWidth - offsetX;
            if (width >= this.LayoutRoot.MinWidth)
            {
                this.Width = _originWidth - offsetX;
                this.Left = _originX + offsetX;
            }
            else
            {
                this.Width = this.LayoutRoot.MinWidth;
                this.Left = _originX + offsetX + width - this.LayoutRoot.MinWidth;
            }

            var height = _originHeight - offsetY;
            if (height >= this.LayoutRoot.MinHeight)
            {
                this.Height = _originHeight - offsetY;
                this.Top = _originY + offsetY;
            }
            else
            {
                this.Height = this.LayoutRoot.MinHeight;
                this.Top = _originY + offsetY + height - this.LayoutRoot.MinHeight;
            }
        }

        private void LeftTopRectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LeftTopRectangle.ReleaseMouseCapture();
            LeftTopRectangle.MouseMove -= LeftTopRectangle_MouseMove;
            LeftTopRectangle.MouseLeftButtonUp -= LeftTopRectangle_MouseLeftButtonUp;
        }

        #endregion

        #region 上

        private void TopRectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TopRectangle.CaptureMouse();

            _originPoint = e.GetPosition(null);
            //_originX = this.Left;
            _originY = this.Top;
            //_originWidth = this.Width;
            _originHeight = this.Height;
            TopRectangle.MouseMove -= TopRectangle_MouseMove;
            TopRectangle.MouseMove += TopRectangle_MouseMove;
            TopRectangle.MouseLeftButtonUp -= TopRectangle_MouseLeftButtonUp;
            TopRectangle.MouseLeftButtonUp += TopRectangle_MouseLeftButtonUp;

            e.Handled = true;
        }

        private void TopRectangle_MouseMove(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition(null);
            var offsetX = point.X - _originPoint.X;
            var offsetY = point.Y - _originPoint.Y;
            //this.Left = _originX + offsetX;
            //this.Top = _originY + offsetY;
            //this.Width = _originWidth - offsetX;
            //this.Height = _originHeight - offsetY;

            var height = _originHeight - offsetY;
            if (height >= this.LayoutRoot.MinHeight)
            {
                this.Height = _originHeight - offsetY;
                this.Top = _originY + offsetY;
            }
            else
            {
                this.Height = this.LayoutRoot.MinHeight;
                this.Top = _originY + offsetY + height - this.LayoutRoot.MinHeight;
            }
        }

        private void TopRectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TopRectangle.ReleaseMouseCapture();
            TopRectangle.MouseMove -= TopRectangle_MouseMove;
            TopRectangle.MouseLeftButtonUp -= TopRectangle_MouseLeftButtonUp;
        }

        #endregion

        #region 右上

        private void RightTopRectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RightTopRectangle.CaptureMouse();

            _originPoint = e.GetPosition(null);
            //_originX = this.Left;
            _originY = this.Top;
            _originWidth = this.Width;
            _originHeight = this.Height;
            RightTopRectangle.MouseMove -= RightTopRectangle_MouseMove;
            RightTopRectangle.MouseMove += RightTopRectangle_MouseMove;
            RightTopRectangle.MouseLeftButtonUp -= RightTopRectangle_MouseLeftButtonUp;
            RightTopRectangle.MouseLeftButtonUp += RightTopRectangle_MouseLeftButtonUp;

            e.Handled = true;
        }

        private void RightTopRectangle_MouseMove(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition(null);
            var offsetX = point.X - _originPoint.X;
            var offsetY = point.Y - _originPoint.Y;
            //this.Left = _originX + offsetX;
            //this.Top = _originY + offsetY;
            this.Width = _originWidth + offsetX;
            //this.Height = _originHeight - offsetY;

            var height = _originHeight - offsetY;
            if (height >= this.LayoutRoot.MinHeight)
            {
                this.Height = _originHeight - offsetY;
                this.Top = _originY + offsetY;
            }
            else
            {
                this.Height = this.LayoutRoot.MinHeight;
                this.Top = _originY + offsetY + height - this.LayoutRoot.MinHeight;
            }
        }

        private void RightTopRectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RightTopRectangle.ReleaseMouseCapture();
            RightTopRectangle.MouseMove -= RightTopRectangle_MouseMove;
            RightTopRectangle.MouseLeftButtonUp -= RightTopRectangle_MouseLeftButtonUp;
        }

        #endregion

        #region 右

        private void RightRectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RightRectangle.CaptureMouse();

            _originPoint = e.GetPosition(null);
            _originWidth = this.Width;
            //_originHeight = this.Height;
            RightRectangle.MouseMove -= RightRectangle_MouseMove;
            RightRectangle.MouseMove += RightRectangle_MouseMove;
            RightRectangle.MouseLeftButtonUp -= RightRectangle_MouseLeftButtonUp;
            RightRectangle.MouseLeftButtonUp += RightRectangle_MouseLeftButtonUp;

            e.Handled = true;
        }

        private void RightRectangle_MouseMove(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition(null);
            var offsetX = point.X - _originPoint.X;
            //var offsetY = point.Y - _originPoint.Y;
            this.Width = _originWidth + offsetX;
            //this.Height = _originHeight + offsetY;
        }

        private void RightRectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RightRectangle.ReleaseMouseCapture();
            RightRectangle.MouseMove -= RightRectangle_MouseMove;
            RightRectangle.MouseLeftButtonUp -= RightRectangle_MouseLeftButtonUp;
        }

        #endregion

        #region 右下

        private void RightBottomRectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RightBottomRectangle.CaptureMouse();

            _originPoint = e.GetPosition(null);
            _originWidth = this.Width;
            _originHeight = this.Height;
            RightBottomRectangle.MouseMove -= RightBottomRectangle_MouseMove;
            RightBottomRectangle.MouseMove += RightBottomRectangle_MouseMove;
            RightBottomRectangle.MouseLeftButtonUp -= RightBottomRectangle_MouseLeftButtonUp;
            RightBottomRectangle.MouseLeftButtonUp += RightBottomRectangle_MouseLeftButtonUp;

            e.Handled = true;
        }

        private void RightBottomRectangle_MouseMove(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition(null);
            var offsetX = point.X - _originPoint.X;
            var offsetY = point.Y - _originPoint.Y;
            this.Width = _originWidth + offsetX;
            this.Height = _originHeight + offsetY;
        }

        private void RightBottomRectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RightBottomRectangle.ReleaseMouseCapture();
            RightBottomRectangle.MouseMove -= RightBottomRectangle_MouseMove;
            RightBottomRectangle.MouseLeftButtonUp -= RightBottomRectangle_MouseLeftButtonUp;
        }

        #endregion

        #region 下

        private void BottomRectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BottomRectangle.CaptureMouse();

            _originPoint = e.GetPosition(null);
            //_originWidth = this.Width;
            _originHeight = this.Height;
            BottomRectangle.MouseMove -= BottomRectangle_MouseMove;
            BottomRectangle.MouseMove += BottomRectangle_MouseMove;
            BottomRectangle.MouseLeftButtonUp -= BottomRectangle_MouseLeftButtonUp;
            BottomRectangle.MouseLeftButtonUp += BottomRectangle_MouseLeftButtonUp;

            e.Handled = true;
        }

        private void BottomRectangle_MouseMove(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition(null);
            //var offsetX = point.X - _originPoint.X;
            var offsetY = point.Y - _originPoint.Y;
            //this.Width = _originWidth + offsetX;
            this.Height = _originHeight + offsetY;
        }

        private void BottomRectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BottomRectangle.ReleaseMouseCapture();
            BottomRectangle.MouseMove -= BottomRectangle_MouseMove;
            BottomRectangle.MouseLeftButtonUp -= BottomRectangle_MouseLeftButtonUp;
        }

        #endregion

        #region 左下

        private void LeftBottomRectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LeftBottomRectangle.CaptureMouse();

            _originPoint = e.GetPosition(null);
            _originX = this.Left;
            //_originY = this.Top;
            _originWidth = this.Width;
            _originHeight = this.Height;
            LeftBottomRectangle.MouseMove -= LeftBottomRectangle_MouseMove;
            LeftBottomRectangle.MouseMove += LeftBottomRectangle_MouseMove;
            LeftBottomRectangle.MouseLeftButtonUp -= LeftBottomRectangle_MouseLeftButtonUp;
            LeftBottomRectangle.MouseLeftButtonUp += LeftBottomRectangle_MouseLeftButtonUp;

            e.Handled = true;
        }

        private void LeftBottomRectangle_MouseMove(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition(null);
            var offsetX = point.X - _originPoint.X;
            var offsetY = point.Y - _originPoint.Y;
            //this.Left = _originX + offsetX;
            //this.Top = _originY + offsetY;
            //this.Width = _originWidth - offsetX;
            this.Height = _originHeight + offsetY;

            var width = _originWidth - offsetX;
            if (width >= this.LayoutRoot.MinWidth)
            {
                this.Width = _originWidth - offsetX;
                this.Left = _originX + offsetX;
            }
            else
            {
                this.Width = this.LayoutRoot.MinWidth;
                this.Left = _originX + offsetX + width - this.LayoutRoot.MinWidth;
            }
        }

        private void LeftBottomRectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LeftBottomRectangle.ReleaseMouseCapture();
            LeftBottomRectangle.MouseMove -= LeftBottomRectangle_MouseMove;
            LeftBottomRectangle.MouseLeftButtonUp -= LeftBottomRectangle_MouseLeftButtonUp;
        }

        #endregion

        #region 左

        private void LeftRectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LeftRectangle.CaptureMouse();

            _originPoint = e.GetPosition(null);
            _originX = this.Left;
            //_originY = this.Top;
            _originWidth = this.Width;
            //_originHeight = this.Height;
            LeftRectangle.MouseMove -= LeftRectangle_MouseMove;
            LeftRectangle.MouseMove += LeftRectangle_MouseMove;
            LeftRectangle.MouseLeftButtonUp -= LeftRectangle_MouseLeftButtonUp;
            LeftRectangle.MouseLeftButtonUp += LeftRectangle_MouseLeftButtonUp;

            e.Handled = true;
        }

        private void LeftRectangle_MouseMove(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition(null);
            var offsetX = point.X - _originPoint.X;
            var offsetY = point.Y - _originPoint.Y;
            //this.Top = _originY + offsetY;

            var width = _originWidth - offsetX;
            if (width >= this.LayoutRoot.MinWidth)
            {
                this.Width = _originWidth - offsetX;
                this.Left = _originX + offsetX;
            }
            else
            {
                this.Width = this.LayoutRoot.MinWidth;
                this.Left = _originX + offsetX + width - this.LayoutRoot.MinWidth;
            }
            //this.Height = _originHeight - offsetY;
        }

        private void LeftRectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LeftRectangle.ReleaseMouseCapture();
            LeftRectangle.MouseMove -= LeftRectangle_MouseMove;
            LeftRectangle.MouseLeftButtonUp -= LeftRectangle_MouseLeftButtonUp;
        }

        #endregion
    }
}
