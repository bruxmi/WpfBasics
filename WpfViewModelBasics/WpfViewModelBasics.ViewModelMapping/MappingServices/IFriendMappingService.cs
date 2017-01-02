using System.Collections.Generic;
using WpfViewModelBasics.Core.Entities;
using WpfViewModelBasics.ViewModelMapping.ViewModel;

namespace WpfViewModelBasics.ViewModelMapping.MappingServices
{
    public interface IFriendMappingService
    {
        Friend ViewModelToEntity(FriendVm vm);
        List<Friend> ViewModelToEntity(List<FriendVm> vm);
        FriendVm EntityToViewModel(Friend entity);
        List<FriendVm> EntityToViewModel(List<Friend> entity);
    }
}
