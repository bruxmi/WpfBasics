using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Practices.Unity;
using WpfViewModelBasics.Core.Registration.Container;

namespace WpfViewModelBasics.Core.Registration
{
    public static class BusinessBootstrapper
    {
        public static void InitializeBusiness(IUnityContainer container, Func<LifetimeManager> serviceLifetime)
        {
            container.InjectMisc(serviceLifetime);
            container.InjectMediator(serviceLifetime);
        }

        public static void InitializeBusinessWithAutoac(ContainerBuilder container)
        {
            MiscContainer.InjectMiscWithAutofac(container);
            MediatorContainer.InjectMediatorWithAutofac(container);
        }
    }
}
