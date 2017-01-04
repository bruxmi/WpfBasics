using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Microsoft.Practices.Unity;
using WpfViewModelBasics.Core.Registration;
using WpfViewModelBasics.UI.StartUp;
using WpfViewModelBasics.UI.View;
using WpfViewModelBasics.UI.ViewModel;
using WpfViewModelBasics.ViewModelMapping;

namespace WpfViewModelBasics.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainViewModel _mainViewModel;

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var containerbuilder = new ContainerBuilder();
            containerbuilder.InitializeBusinessWithAutoac();
            containerbuilder.InitializeViewModelsWithAutofac();
            var container = containerbuilder.Build();
            _mainViewModel = container.Resolve<MainViewModel>();

            MainWindow = new MainWindow(_mainViewModel);
            MainWindow.Show();
            await _mainViewModel.Load();
        }
    }
}
