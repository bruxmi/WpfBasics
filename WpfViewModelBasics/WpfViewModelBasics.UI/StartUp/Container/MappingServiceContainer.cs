
using Autofac;
using WpfViewModelBasics.ViewModelMapping;

namespace WpfViewModelBasics.UI.StartUp.Container
{
    using System;
    using AutoMapper;
    using Microsoft.Practices.Unity;
    using ViewModelMapping.MappingServices;
    public static class MappingServiceContainer
    {
        public static void InjectMappingServicesWithAutofac(this ContainerBuilder container)
        {
            container.RegisterInstance<IMapper>(new Mapper(AutoMapperConfig.Configure()));
            container.RegisterType<AutoMappingService>().As<IAutoMapperService>();
        }
    }
}
