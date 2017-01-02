using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfViewModelBasics.Core.Entities;
using WpfViewModelBasics.ViewModelMapping.ViewModel;

namespace WpfViewModelBasics.ViewModelMapping.MappingServices
{
    public class FriendMappingService: IFriendMappingService
    {
        private readonly IAddressMappingService _addressMappingService;
        private readonly IFriendEmailMappingService _emailMappingService;

        public FriendMappingService(IAddressMappingService addressMappingService, IFriendEmailMappingService emailMappingService)
        {
            _addressMappingService = addressMappingService;
            _emailMappingService = emailMappingService;
        }
        
        public Friend ViewModelToEntity(FriendVm vm)
        {
            return new Friend
            {
                Id = vm.Id,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                IsDeveloper = vm.IsDeveloper,
                Birthday = vm.Birthday,
                Address = this._addressMappingService.ViewModelToEntity(vm.Address),
                AddressId = vm.Address.Id
                //Emails = this._emailMappingService.ViewModelToEntity(vm.Emails)
            };
        }

        public List<Friend> ViewModelToEntity(List<FriendVm> vms)
        {
            var result = vms.Select(ViewModelToEntity).ToList();
            return result;
        }

        public FriendVm EntityToViewModel(Friend entity)
        {
            return new FriendVm
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                IsDeveloper = entity.IsDeveloper,
                Birthday = entity.Birthday,
                Address = this._addressMappingService.EntityToViewModel(entity.Address),
                Emails = this._emailMappingService.EntityToViewModel(entity.Emails.ToList())
            };
        }

        public List<FriendVm> EntityToViewModel(List<Friend> entities)
        {
            return entities.Select(EntityToViewModel).ToList();
        }
    }
}
