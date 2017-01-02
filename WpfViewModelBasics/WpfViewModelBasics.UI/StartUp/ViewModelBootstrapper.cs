using System;
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
    }
}
