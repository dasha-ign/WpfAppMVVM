using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfAppMVVM.WPF.Stores;
using WpfAppMVVM.WPF.ViewModels;

namespace WpfAppMVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DictionaryStore dictionaryStore = new DictionaryStore();


            MainWindow = new MainWindow()
            {
                DataContext = new DictionaryViewModel(dictionaryStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
