using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using WpfViewModelBasics.UI.Interfaces;
using WpfViewModelBasics.UI.View.Services;

namespace WpfViewModelBasics.UI.StartUp.Container
{
    public static class ViewServiceContainer
    {
        public static void InjectViewServices(this IUnityContainer container, Func<LifetimeManager> serviceLifetime)
        {
            container.RegisterType<IMessageDialogService, MessageDialogService>(serviceLifetime());
        }
    }
}
