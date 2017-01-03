using System.Collections.Generic;

namespace WpfViewModelBasics.UI.ViewModel
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Prism.Events;
    using Core.Entities;
    using Core.Interfaces.Services.Command;
    using Core.Interfaces.Services.Query;
    using Command;
    using Events;
    using Interfaces;
    using Base;
    using Wrapper;
    using ViewModelMapping.MappingServices;
    using ViewModelMapping.ViewModel;

    public class FriendEditViewModel : ViewModelBase, IFriendEditViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IFriendQueryService _friendQueryService;
        private readonly IFriendCommandService _friendCommandService;
        private readonly IFriendEmailCommandService _friendEmailCommandService;
        private readonly IAddressCommandService _addressCommandService;
        private readonly IAutoMapperService _mapper;

        public FriendEditViewModel(IEventAggregator eventAggregator,
            IFriendQueryService friendQueryService,
            IFriendCommandService friendCommandService, 
            IFriendEmailCommandService friendEmailCommandService, 
            IAddressCommandService addressCommandService, 
            IAutoMapperService mapper)
        {
            _eventAggregator = eventAggregator;
            _friendQueryService = friendQueryService;
            _friendCommandService = friendCommandService;
            _friendEmailCommandService = friendEmailCommandService;
            _addressCommandService = addressCommandService;
            _mapper = mapper;
            DeleteFriendCommand = new AsyncDelegateCommand(async a => await OnDeleteFriendExecute(a));
            SaveFriendCommand = new AsyncDelegateCommand(async a => await OnSaveExecute(a));
            RejectChangesCommand = new DelegateCommand(OnRejectChangesExecute);
        }

        public async Task Load(int? friendId = null)
        {

            var friendEntity = friendId != null ? 
                await this._friendQueryService.GetFriendByIdAsync(friendId.Value) :
                new Friend { Emails = new List<FriendEmail>(), Address = new Address()};
                
            var friend = _mapper.MapTo<Friend, FriendVm>(friendEntity);
            Friend = new FriendWrapper(friend);
        }

        public FriendWrapper Friend
        {
            get { return GetValue<FriendWrapper>(); }
            private set { SetValue(value); }
        }
        public ChangeTrackingCollection<FriendEmailWrapper> Emails { get; set; }
        public ICommand DeleteFriendCommand { get; set; }
        public ICommand SaveFriendCommand { get; set; }
        public ICommand RejectChangesCommand { get; set; }

        private async Task OnDeleteFriendExecute(object obj)
        {
            var friendEntity = this._mapper.MapTo<FriendVm, Friend>(Friend.Model);
            await this._friendCommandService.DeleteFriendAsync(friendEntity);
            _eventAggregator.GetEvent<DeleteFriendEvent>().Publish(friendEntity.Id);
        }

        private async Task OnSaveExecute(object obj)
        {
            var friendEntity = this._mapper.MapTo<FriendVm, Friend>(Friend.Model);
            await AddOrUpdateFriend(friendEntity);
            
            if (Friend.Emails.IsChanged)
            {
                await this._friendEmailCommandService.UpdateEmail(this._mapper.MapTo<FriendEmailVm, FriendEmail>(Friend.Emails.ModifiedItems.First().Model));
            }
            if (Friend.Address.IsChanged)
            {
                var addressEntity = this._mapper.MapTo<AddressVm, Address>(Friend.Address.Model);
                await AddOrUpdateAddress(addressEntity);
            }
            Friend.AcceptChanges();
            _eventAggregator.GetEvent<SaveFriendEditViewEvent>().Publish(Friend.Model);
        }

        private async Task AddOrUpdateAddress(Address addressEntity)
        {
            if (addressEntity.Id == 0)
            {
                await this._addressCommandService.AddAddressAsync(addressEntity);
            }
            else
            {
                await this._addressCommandService.UpdateAddressAsync(addressEntity);
            }
        }

        private async Task AddOrUpdateFriend(Friend friendEntity)
        {
            if (friendEntity.Id == 0)
            {
                await _friendCommandService.AddFriendAsync(friendEntity);
            }
            else
            {
                await this._friendCommandService.UpdateFriendAsync(friendEntity);
            }
        }

        private void OnRejectChangesExecute(object obj)
        {
            Friend.RejectChanges();
        }
    }
}
