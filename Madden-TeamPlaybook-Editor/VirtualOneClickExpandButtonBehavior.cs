﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Madden20PlaybookEditor.User_Controls
{
    public class VirtualOneClickExpandButtonBehavior : DependencyObject
    {
        public static bool GetEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnabledProperty);
        }

        public static void SetEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(EnabledProperty, value);
        }


        public static readonly DependencyProperty EnabledProperty =
            DependencyProperty.RegisterAttached("Enabled", typeof(bool), typeof(VirtualOneClickExpandButtonBehavior),
                new UIPropertyMetadata(false, EnabledPropertyChangedCallback
                    ));

        private static void EnabledPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var treeView = dependencyObject as TreeView;

            if (treeView == null) return;

            treeView.MouseLeftButtonUp += TreeView_MouseUp;
        }

        private static void TreeView_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var treeViewItem = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (treeViewItem != null) treeViewItem.IsExpanded = !treeViewItem.IsExpanded;
        }

        static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
                source = VisualTreeHelper.GetParent(source);

            return source;
        }
    }
}
