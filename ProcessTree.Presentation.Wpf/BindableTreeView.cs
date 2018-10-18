using System.Windows;
using System.Windows.Controls;

namespace ProcessTree.Presentation.Wpf
{
    internal sealed class BindableTreeView : TreeView
    {
        public static readonly DependencyProperty BindableSelectedItemProperty = DependencyProperty.Register(
                "BindableSelectedItem", 
                typeof(object), 
                typeof(BindableTreeView), 
                new PropertyMetadata(null)
        );

        public BindableTreeView()
        {
            SelectedItemChanged += (sender, e) =>
             {
                 BindableSelectedItem = SelectedItem;
             };
        }

        public object BindableSelectedItem
        {
            get { return (object)GetValue(BindableSelectedItemProperty); }
            set { SetValue(BindableSelectedItemProperty, value); }
        }
    }
}