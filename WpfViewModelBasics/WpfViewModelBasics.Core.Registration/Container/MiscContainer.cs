namespace WpfViewModelBasics.Core.Registration.Container
{
    using Autofac;
    using Context;
    using Initializer;
    using Repository.Command;
    using Repository.Query;
    using WpfViewModelBasic.Data;
    using WpfViewModelBasic.Data.Command;
    using WpfViewModelBasic.Data.Query;

    public static class MiscContainer
    {
        public static void InjectMiscWithAutofac(this ContainerBuilder builder)
        {
            builder.RegisterType<FriendStorageContext>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(QueryRepository<>)).As(typeof(IQueryRepository<>));
            builder.RegisterGeneric(typeof(CommandRepository<>)).As(typeof(ICommandRepository<>));
            builder.RegisterGeneric(typeof(RepositoryFriendStorageContextInitializer<>)).As(typeof(IRepositoryInitializer<>));
        }
    }
}