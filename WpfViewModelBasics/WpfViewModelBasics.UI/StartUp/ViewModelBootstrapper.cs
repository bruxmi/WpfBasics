using System;
using Autofac;
using Microsoft.Practices.Unity;
using WpfViewModelBasics.UI.StartUp.Container;

namespace WpfViewModelBasics.UI.StartUp
{
    public static class ViewModelBootstrapper
    {
        public static void InitializeViewModels(IUnityContainer container, Func<LifetimeManager> serviceLifetime)
        {
            container.InjectViewModels(serviceLifetime);
            container.InjectMappingServices(serviceLifetime);
            container.InjectViewServices(serviceLifetime);
        }

        public static void InitializeViewModelsWithAutofac(ContainerBuilder container)
        {
            MappingServiceContainer.InjectMappingServicesWithAutofac(container);
            ViewModelContainer.InjectViewModelsWithAutofac(container);
            ViewServiceContainer.InjectViewServicesWithAutofac(container);
        }
    }
}
