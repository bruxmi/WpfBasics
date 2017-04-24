using System;
using Autofac;
using Microsoft.Practices.Unity;
using Prism.Events;
using WpfViewModelBasics.UI.Interfaces;
using WpfViewModelBasics.UI.View.Services;
using WpfViewModelBasics.UI.ViewModel;

namespace WpfViewModelBasics.UI.StartUp.Container
{
    public static class ViewModelContainer
    {
        public static void InjectViewModelsWithAutofac(this ContainerBuilder container)
        {
            container.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            container.RegisterType<FriendEditViewModel>().As<IFriendEditViewModel>();
            container.RegisterType<FriendTreeViewModel>().As<IFriendTreeViewModel>();
            container.RegisterType<FriendNavigationViewModel>().As<IFriendNavigationViewModel>();
            container.RegisterType<MainViewModel>().AsSelf();
        }
    }
}
