using System.Windows.Markup;
using WpfAppMVVM.WPF.ViewModels.Base;

namespace WpfAppMVVM.WPF.ViewModels;

[MarkupExtensionReturnType(typeof(MainWindowViewModel))]
public class MainWindowViewModel : ViewModel
{
    #region Title : string - Заголовок

    /// <summary>Заголовок</summary>
    private string _Title = "Главное окно";

    /// <summary>Заголовок</summary>
    public string Title { get => _Title; set => Set(ref _Title, value); }

    #endregion


}
