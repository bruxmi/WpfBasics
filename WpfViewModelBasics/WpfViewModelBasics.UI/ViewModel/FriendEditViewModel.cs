using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Events;
using WpfViewModelBasics.Core.Interfaces.Services.Command;
using WpfViewModelBasics.Core.Interfaces.Services.Query;
using WpfViewModelBasics.UI.Command;
using WpfViewModelBasics.UI.Events;
using WpfViewModelBasics.UI.Interfaces;
using WpfViewModelBasics.UI.ViewModel.Base;
using WpfViewModelBasics.UI.Wrapper;
using WpfViewModelBasics.ViewModelMapping.MappingServices;

namespace WpfViewModelBasics.UI.ViewModel
{
    public class FriendEditViewModel : ViewModelBase, IFriendEditViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IFriendQueryService _friendQueryService;
        private readonly IFriendMappingService _friendMappingService;
        private readonly IFriendCommandService _friendCommandService;
        private readonly IFriendEmailCommandService _friendEmailCommandService;
        private readonly IFriendEmailMappingService _friendEmailMappingService;
        private readonly IAddressCommandService _addressCommandService;
        private readonly IAddressMappingService _addressMappingService;

        public FriendEditViewModel(IEventAggregator eventAggregator,
            IFriendQueryService friendQueryService,
            IFriendMappingService friendMappingService, 
            IFriendCommandService friendCommandService, 
            IFriendEmailCommandService friendEmailCommandService, 
            IFriendEmailMappingService friendEmailMappingService,
            IAddressCommandService addressCommandService, 
            IAddressMappingService addressMappingService)
        {
            _eventAggregator = eventAggregator;
            _friendQueryService = friendQueryService;
            _friendMappingService = friendMappingService;
            _friendCommandService = friendCommandService;
            _friendEmailCommandService = friendEmailCommandService;
            _friendEmailMappingService = friendEmailMappingService;
            _addressCommandService = addressCommandService;
            _addressMappingService = addressMappingService;
            SaveFriendCommand = new AsyncDelegateCommand(async a => await OnSaveExecute(a));
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

        public ChangeTrackingCollection<FriendEmailWrapper> Emails { get; set; }

        public ICommand SaveFriendCommand { get; set; }

        private async Task OnSaveExecute(object obj)
        {
            await this._friendCommandService.UpdateFriend(this._friendMappingService.ViewModelToEntity(Friend.Model));
            if (Friend.Emails.IsChanged)
            {
                await this._friendEmailCommandService.UpdateEmail(this._friendEmailMappingService.ViewModelToEntity(Friend.Emails.ModifiedItems.First().Model));
            }
            if (Friend.Address.IsChanged)
            {
                await this._addressCommandService.UpdateAddress(this._addressMappingService.ViewModelToEntity(Friend.Address.Model));
            }
            Friend.AcceptChanges();
            _eventAggregator.GetEvent<SaveFriendEditViewEvent>().Publish(Friend.Model);
        }
    }
}
