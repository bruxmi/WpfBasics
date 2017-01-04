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
            AddFriendEmailCommand = new DelegateCommand(OnAddFriendEmailExecute);
        }

        public async Task Load(int? friendId = null)
        {

            var friendEntity = friendId != null ?
                await this._friendQueryService.GetFriendByIdAsync(friendId.Value) :
                new Friend { Emails = new List<FriendEmail>(), Address = new Address() };

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
        public ICommand AddFriendEmailCommand { get; set; }

        private async Task OnDeleteFriendExecute(object obj)
        {
            var friendEntity = this._mapper.MapTo<FriendVm, Friend>(Friend.Model);
            await this._friendCommandService.DeleteFriendAsync(friendEntity);
            _eventAggregator.GetEvent<DeleteFriendEvent>().Publish(friendEntity.Id);
        }

        private async Task OnSaveExecute(object obj)
        {
            await AddOrUpdateFriend();

            if (Friend.Emails.IsChanged)
            {
                await AddOrUpdateEmails();
            }
            if (Friend.Address.IsChanged)
            {
                await AddOrUpdateAddress();
            }
            Friend.AcceptChanges();
            _eventAggregator.GetEvent<SaveFriendEditViewEvent>().Publish(Friend.Model);
        }

        private async Task AddOrUpdateEmails()
        {
            var addedEmails = this._mapper.MapTo<IEnumerable<FriendEmailVm>, IEnumerable<FriendEmail>>(Friend.Emails.AddedItems.Select(wrapper => wrapper.Model)).ToList();
            addedEmails = await this._friendEmailCommandService.AddEmailListAsync(addedEmails);
            for (var i = 0; i < Friend.Emails.AddedItems.Count; i++)
            {
                Friend.Emails.AddedItems[i].Id = addedEmails[i].Id;
            }
            var modifiedEmails = this._mapper.MapTo<IEnumerable<FriendEmailVm>, IEnumerable<FriendEmail>>(Friend.Emails.ModifiedItems.Select(wrapper => wrapper.Model)).ToList();
            await this._friendEmailCommandService.UpdateEmailListAsync(modifiedEmails);
        }

        private async Task AddOrUpdateAddress()
        {
            var addressEntity = this._mapper.MapTo<AddressVm, Address>(Friend.Address.Model);
            if (addressEntity.Id == 0)
            {
                addressEntity = await this._addressCommandService.AddAddressAsync(addressEntity);
            }
            else
            {
                await this._addressCommandService.UpdateAddressAsync(addressEntity);
            }
            Friend.Address.Id = addressEntity.Id;
        }

        private async Task AddOrUpdateFriend()
        {
            var friendEntity = this._mapper.MapTo<FriendVm, Friend>(Friend.Model);
            if (friendEntity.Id == 0)
            {
                friendEntity = await _friendCommandService.AddFriendAsync(friendEntity);
            }
            else
            {
                await this._friendCommandService.UpdateFriendAsync(friendEntity);
            }
            Friend.Id = friendEntity.Id;
            Friend.Address.Id = friendEntity.Address.Id;
        }

        private void OnRejectChangesExecute(object obj)
        {
            Friend.RejectChanges();
        }

        private void OnAddFriendEmailExecute(object obj)
        {
            Friend.Emails.Add(new FriendEmailWrapper(new FriendEmailVm { FriendId = Friend.Id }));
        }
    }
}
