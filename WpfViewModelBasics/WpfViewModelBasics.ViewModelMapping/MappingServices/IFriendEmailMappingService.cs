using System.Collections.Generic;
using WpfViewModelBasics.Core.Entities;
using WpfViewModelBasics.ViewModelMapping.ViewModel;

namespace WpfViewModelBasics.ViewModelMapping.MappingServices
{
    public interface IFriendEmailMappingService
    {
        FriendEmail ViewModelToEntity(FriendEmailVm vm);

        List<FriendEmail> ViewModelToEntity(List<FriendEmailVm> vms);
        FriendEmailVm EntityToViewModel(FriendEmail entity);
        List<FriendEmailVm> EntityToViewModel(List<FriendEmail> entities);
    }
}
