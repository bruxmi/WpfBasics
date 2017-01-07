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
using WpfViewModelBasics.Business.Friend.Command;

namespace WpfViewModelBasics.Core.Registration.Container
{
    public static class MediatorContainer
    {
        public static void InjectMediatorWithAutofac(this ContainerBuilder builder)
        {

            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(AddFriendCommandServiceHandler).GetTypeInfo().Assembly).AsImplementedInterfaces();


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
        }
    }
}
