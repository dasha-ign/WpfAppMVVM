using System.Windows;
using Microsoft.Xaml.Behaviors;

namespace WpfAppMVVM.WPF.Behaviors;

internal class DragAndDropBehavior : Behavior<UIElement>
{
    protected override void OnAttached()
    {
        AssociatedObject.DragLeave += OnDragLeave;
        AssociatedObject.DragEnter += OnDragEnter;

        base.OnAttached();
    }

    protected override void OnDetaching()
    {
        AssociatedObject.DragLeave -= OnDragLeave;
        AssociatedObject.DragEnter -= OnDragEnter;

        base.OnDetaching();
    }

    private void OnDragLeave(object Sender, DragEventArgs E)
    {

    }

    private void OnDragEnter(object Sender, DragEventArgs E)
    {

    }
}
