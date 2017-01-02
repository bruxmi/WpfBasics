using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfViewModelBasics.Core.Entities;
using WpfViewModelBasics.ViewModelMapping.ViewModel;

namespace WpfViewModelBasics.ViewModelMapping.MappingServices
{
    public class FriendEmailMappingService : IFriendEmailMappingService
    {
        public FriendEmail ViewModelToEntity(FriendEmailVm vm)
        {
            return new FriendEmail
            {
                Id = vm.Id,
                Email = vm.Email,
                FriendId = vm.FriendId
            };
        }

        public List<FriendEmail> ViewModelToEntity(List<FriendEmailVm> vms)
        {
            var result = vms.Select(ViewModelToEntity).ToList();
            return result;
        }

        public FriendEmailVm EntityToViewModel(FriendEmail entity)
        {
            return new FriendEmailVm
            {
                Id = entity.Id,
                Email = entity.Email,
                FriendId = entity.FriendId
            };
        }

        public List<FriendEmailVm> EntityToViewModel(List<FriendEmail> entities)
        {
            var result = entities.Select(EntityToViewModel).ToList();
            return result;
        }
    }
}
