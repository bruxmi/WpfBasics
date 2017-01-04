using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Autofac.Features.Variance;
using MediatR;
using Microsoft.Practices.Unity;
using WpfViewModelBasic.Data;
using WpfViewModelBasic.Data.Command;
using WpfViewModelBasic.Data.Query;
using WpfViewModelBasics.Business.Address;
using WpfViewModelBasics.Business.Friend;
using WpfViewModelBasics.Business.FriendEmail;
using WpfViewModelBasics.Context;
using WpfViewModelBasics.Core.Initializer;
using WpfViewModelBasics.Core.Interfaces.Services.Command;
using WpfViewModelBasics.Core.Interfaces.Services.Query;
using WpfViewModelBasics.Core.Repository.Command;
using WpfViewModelBasics.Core.Repository.Query;
using WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.Friend;

namespace WpfViewModelBasics.Core.Registration.Container
{
    public static class MiscContainer
    {
        public static void InjectMisc(this IUnityContainer container, Func<LifetimeManager> serviceLifetime)
        {
            container.RegisterType(typeof(IRepositoryInitializer<>), typeof(RepositoryFriendStorageContextInitializer<>), serviceLifetime());
            container.RegisterType(typeof(IQueryRepository<>), typeof(QueryRepository<>), serviceLifetime());
            container.RegisterType(typeof(ICommandRepository<>), typeof(CommandRepository<>), serviceLifetime());

            container.RegisterType<IFriendQueryService, FriendQueryService>(serviceLifetime());
            container.RegisterType<IFriendCommandService, FriendCommandService>(serviceLifetime());

            container.RegisterType<IFriendEmailQueryService, FriendEmailQueryService>(serviceLifetime());
            container.RegisterType<IFriendEmailCommandService, FriendEmailCommandService>(serviceLifetime());

            container.RegisterType<IAddressCommandService, AddressCommandService>(serviceLifetime());
        }

        public static void InjectMiscWithAutofac(ContainerBuilder builder)
        {
            builder.RegisterType<FriendStorageContext>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(QueryRepository<>)).As(typeof(IQueryRepository<>));
            builder.RegisterGeneric(typeof(CommandRepository<>)).As(typeof(ICommandRepository<>));
            builder.RegisterGeneric(typeof(RepositoryFriendStorageContextInitializer<>)).As(typeof(IRepositoryInitializer<>));

            builder.RegisterType<FriendQueryService>().As<IFriendQueryService>();
            builder.RegisterType<FriendCommandService>().As<IFriendCommandService>();
            builder.RegisterType<FriendEmailQueryService>().As<IFriendEmailQueryService>();
            builder.RegisterType<FriendEmailCommandService>().As<IFriendEmailCommandService>();
            builder.RegisterType<AddressCommandService>().As<IAddressCommandService>();
        }
    }
}
