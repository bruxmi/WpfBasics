using System;
using Microsoft.Practices.Unity;
using WpfViewModelBasics.UI.Interfaces;
using WpfViewModelBasics.UI.ViewModel;
using WpfViewModelBasics.ViewModelMapping.MappingServices;

namespace WpfViewModelBasics.UI.StartUp.Container
{
    public static class MappingServiceContainer
    {
        public static void InjectMappingServices(this IUnityContainer container, Func<LifetimeManager> serviceLifetime)
        {
            container.RegisterType<IFriendMappingService, FriendMappingService>(serviceLifetime());
            container.RegisterType<IAddressMappingService, AddressMappingService>(serviceLifetime());
            container.RegisterType<IFriendEmailMappingService, FriendEmailMappingService>(serviceLifetime());
        }
    }
}
