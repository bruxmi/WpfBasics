using System;
using Microsoft.Practices.Unity;
using WpfViewModelBasic.Data;
using WpfViewModelBasic.Data.Command;
using WpfViewModelBasic.Data.Query;
using WpfViewModelBasics.Core.Initializer;
using WpfViewModelBasics.Core.Interfaces.Services.Query;
using WpfViewModelBasics.Core.Repository.Command;
using WpfViewModelBasics.Core.Repository.Query;
using WpfViewModelBasics.Business.Query;

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
        }
    }
}
