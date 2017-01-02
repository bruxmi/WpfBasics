using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfViewModelBasics.Core.Interfaces.Services.Query;
using WpfViewModelBasics.UI.Interfaces;
using WpfViewModelBasics.UI.ViewModel.Base;
using WpfViewModelBasics.UI.Wrapper;
using WpfViewModelBasics.ViewModelMapping.MappingServices;

namespace WpfViewModelBasics.UI.ViewModel
{
    public class FriendEditViewModel : ViewModelBase, IFriendEditViewModel
    {
        private readonly IFriendQueryService _friendQueryService;
        private readonly IFriendMappingService _friendMappingService;
        private FriendWrapper _friend;

        public FriendEditViewModel(IFriendQueryService friendQueryService,
            IFriendMappingService friendMappingService)
        {
            _friendQueryService = friendQueryService;
            _friendMappingService = friendMappingService;
        }

        public void Load(int friendId)
        {
            var friend = this._friendMappingService.EntityToViewModel(this._friendQueryService.GetFriendById(friendId));
            Friend = new FriendWrapper(friend);
        }

        public FriendWrapper Friend
        {
            get { return GetValue<FriendWrapper>(); }
            private set { SetValue(value); }
        }
    }
}
