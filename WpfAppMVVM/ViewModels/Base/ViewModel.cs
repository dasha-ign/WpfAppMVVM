using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Markup;

namespace WpfAppMVVM.WPF.ViewModels.Base;

public abstract class ViewModel : MarkupExtension, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null!) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

    protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null!)
    {
        if (Equals(field, value)) return false;

        field = value;
        OnPropertyChanged(PropertyName);
        return true;
    }

    public override object ProvideValue(IServiceProvider sp) => this;
}