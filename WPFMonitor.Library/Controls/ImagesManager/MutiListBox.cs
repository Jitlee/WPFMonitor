using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections;

namespace WPFMonitor.Library.Controls.ImagesManager
{
    public static class SelectedItems
    {
        private static readonly DependencyProperty SelectedItemsBehaviorProperty =
            DependencyProperty.RegisterAttached(
                "SelectedItemsBehavior",
                typeof(SelectedItemsBehavior),
                typeof(ListBox),
                null);

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.RegisterAttached(
                "Items",
                typeof(IList),
                typeof(SelectedItems),
                new PropertyMetadata(null, ItemsPropertyChanged));

        public static void SetItems(ListBox listBox, IList list) { listBox.SetValue(ItemsProperty, list); }
        public static IList GetItems(ListBox listBox) { return listBox.GetValue(ItemsProperty) as IList; }

        private static void ItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = d as ListBox;
            if (target != null)
            {
                GetOrCreateBehavior(target, e.NewValue as IList);
            }
        }

        private static SelectedItemsBehavior GetOrCreateBehavior(ListBox target, IList list)
        {
            var behavior = target.GetValue(SelectedItemsBehaviorProperty) as SelectedItemsBehavior;
            if (behavior == null)
            {
                behavior = new SelectedItemsBehavior(target, list);
                target.SetValue(SelectedItemsBehaviorProperty, behavior);
            }

            return behavior;
        }
    }

    public class SelectedItemsBehavior
    {
        private readonly ListBox _listBox;
        private readonly IList _boundList;

        public SelectedItemsBehavior(ListBox listBox, IList boundList)
        {
            _boundList = boundList;
            _listBox = listBox;
            _listBox.SelectionChanged += OnSelectionChanged;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _boundList.Clear();

            foreach (var item in _listBox.SelectedItems)
            {
                _boundList.Add(item);
            }
        }
    } 

}
