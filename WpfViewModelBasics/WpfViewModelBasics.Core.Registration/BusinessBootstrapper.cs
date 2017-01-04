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
        public static void InitializeBusinessWithAutoac(this ContainerBuilder container)
        {
            container.InjectMiscWithAutofac();
            container.InjectMediatorWithAutofac();
        }
    }
}
