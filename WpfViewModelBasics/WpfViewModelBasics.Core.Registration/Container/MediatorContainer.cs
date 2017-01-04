using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Practices.Unity;
using WpfViewModelBasics.Business.Friend;
using WpfViewModelBasics.Core.Entities;
using WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.Friend;
using Autofac;
using Autofac.Core;
using Autofac.Features.Variance;

namespace WpfViewModelBasics.Core.Registration.Container
{
    public static class MediatorContainer
    {
        public static void InjectMediator(this IUnityContainer container, Func<LifetimeManager> serviceLifetime)
        {
            container.RegisterType<IMediator, Mediator>(serviceLifetime());

        }

        public static void InjectMediatorWithAutofac(ContainerBuilder builder)
        {

            builder.RegisterSource(new ContravariantRegistrationSource());

            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly)
                .AsImplementedInterfaces();

            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });

            ////register all notification handlers
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //    .As(type => type.GetInterfaces()
            //        .Where(interfacetype => interfacetype.IsClosedTypeOf(typeof(IAsyncNotificationHandler<>))));


            ////register all handlers
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //   .As(type => type.GetInterfaces()
            //       .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IAsyncRequestHandler<,>)))
            //       .Select(interfaceType => new KeyedService("asyncRequestHandler", interfaceType)));


            ////register pipeline decorators
            //builder.RegisterGenericDecorator(typeof(AddFriendCommandServiceHandler), typeof(IAsyncRequestHandler<,>), "asyncRequestHandler");

        }
    }
}
