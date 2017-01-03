
using WpfViewModelBasics.ViewModelMapping;

namespace WpfViewModelBasics.UI.StartUp.Container
{
    using System;
    using AutoMapper;
    using Microsoft.Practices.Unity;
    using ViewModelMapping.MappingServices;
    public static class MappingServiceContainer
    {
        public static void InjectMappingServices(this IUnityContainer container, Func<LifetimeManager> serviceLifetime)
        {
            container.RegisterInstance<IMapper>(new Mapper(AutoMapperConfig.Configure()));
            container.RegisterType<IAutoMapperService, AutoMappingService>(serviceLifetime());
        }
    }
}
