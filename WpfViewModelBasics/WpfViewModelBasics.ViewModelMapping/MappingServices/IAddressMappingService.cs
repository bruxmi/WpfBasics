using WpfViewModelBasics.Core.Entities;
using WpfViewModelBasics.ViewModelMapping.ViewModel;

namespace WpfViewModelBasics.ViewModelMapping.MappingServices
{
    public interface IAddressMappingService
    {
        Address ViewModelToEntity(AddressVm vm);
        AddressVm EntityToViewModel(Address entityAddress);
    }
}