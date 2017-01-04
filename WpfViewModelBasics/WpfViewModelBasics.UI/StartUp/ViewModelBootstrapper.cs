using System;
using Autofac;
using Microsoft.Practices.Unity;
using WpfViewModelBasics.UI.StartUp.Container;

namespace WpfViewModelBasics.UI.StartUp
{
    public static class ViewModelBootstrapper
    {
        public static void InitializeViewModelsWithAutofac(this ContainerBuilder container)
        {
            container.InjectMappingServicesWithAutofac();
            container.InjectViewModelsWithAutofac();
            container.InjectViewServicesWithAutofac();
        }
    }
}
