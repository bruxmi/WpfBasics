using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfViewModelBasics.Core.Entities;
using WpfViewModelBasics.ViewModelMapping.ViewModel;

namespace WpfViewModelBasics.ViewModelMapping.MappingServices
{
    public class AddressMappingService : IAddressMappingService
    {
        public Address ViewModelToEntity(AddressVm vm)
        {
            return new Address
            {
                City = vm.City,
                Id = vm.Id,
                Street = vm.Street,
                StreetNumber = vm.StreetNumber,
            };
        }

        public AddressVm EntityToViewModel(Address entity)
        {
            return new AddressVm
            {
                City = entity.City,
                Id = entity.Id,
                StreetNumber = entity.StreetNumber,
                Street = entity.Street
            };
        }
    }
}
