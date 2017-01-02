using System;
using Microsoft.Practices.Unity;
using WpfViewModelBasic.Data;
using WpfViewModelBasic.Data.Command;
using WpfViewModelBasic.Data.Query;
using WpfViewModelBasics.Business.Address;
using WpfViewModelBasics.Business.Friend;
using WpfViewModelBasics.Business.FriendEmail;
using WpfViewModelBasics.Core.Initializer;
using WpfViewModelBasics.Core.Interfaces.Services.Command;
using WpfViewModelBasics.Core.Interfaces.Services.Query;
using WpfViewModelBasics.Core.Repository.Command;
using WpfViewModelBasics.Core.Repository.Query;

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
    }
}
