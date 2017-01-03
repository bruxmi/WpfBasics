using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WpfViewModelBasics.Core.Entities;
using WpfViewModelBasics.ViewModelMapping.ViewModel;

namespace WpfViewModelBasics.ViewModelMapping
{
    public static class AutoMapperConfig
    {
        public static IConfigurationProvider Configure()
        {
                var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<AddressVm, Address>();
                cfg.CreateMap<FriendEmailVm, FriendEmail>();
                cfg.CreateMap<FriendVm, Friend>();

                cfg.CreateMap<AddressVm, Address>().ReverseMap();
                cfg.CreateMap<FriendEmailVm, FriendEmail>().ReverseMap();
                cfg.CreateMap<FriendVm, Friend>().ReverseMap();
                });

            return mapper;
        }
    }
}
