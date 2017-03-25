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
using Autofac;
using Autofac.Core;
using Autofac.Features.Variance;
using WpfViewModelBasics.Business.FriendEmail.Command;
using WpfViewModelBasics.Core.Interfaces.Services.Command;
using WpfViewModelBasics.Business.Friend.Command;
using WpfViewModelBasics.Business.Address;
using WpfViewModelBasics.Business.Friend.Query;
using WpfViewModelBasics.Core.Interfaces.Services.Query;

namespace WpfViewModelBasics.Core.Registration.Container
{
    public static class BusinessContainer
    {
        public static void InjectBusinessServices(this ContainerBuilder builder)
        {
            builder.RegisterType<FriendEmailCommandService>().As<IFriendEmailCommandService>();
            builder.RegisterType<FriendCommandService>().As<IFriendCommandService>();
            builder.RegisterType<FriendQueryService>().As<IFriendQueryService>();
            builder.RegisterType<AddressCommandService>().As<IAddressCommandService>();
        }
    }
}
