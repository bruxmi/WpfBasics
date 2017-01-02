using System;
using Microsoft.Practices.Unity;
using Prism.Events;
using WpfViewModelBasics.UI.Interfaces;
using WpfViewModelBasics.UI.ViewModel;

namespace WpfViewModelBasics.UI.StartUp.Container
{
    public static class ViewModelContainer
    {
        public static void InjectViewModels(this IUnityContainer container, Func<LifetimeManager> serviceLifetime)
        {
            container.RegisterInstance<IEventAggregator>(new EventAggregator());
            container.RegisterType<IFriendEditViewModel, FriendEditViewModel>();
            container.RegisterType<IFriendNavigationViewModel, FriendNavigationViewModel>();
            container.RegisterType<MainViewModel>();
        }
    }
}
