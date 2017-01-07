﻿namespace WpfViewModelBasics.UI.ViewModel
{
    using Core.Requests.Requests.BusinessRequest.Friend.Command;
    using System.Collections.Generic;
    using MediatR;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Prism.Events;
    using Core.Entities;
    using Command;
    using Events;
    using Interfaces;
    using Base;
    using Wrapper;
    using ViewModelMapping.MappingServices;
    using ViewModelMapping.ViewModel;
    using Core.Requests.Requests.BusinessRequest.Address;
    using Core.Requests.Requests.BusinessRequest.Friend.Query;
    using Core.Requests.Requests.BusinessRequest.FriendEmail.Command;

    public class FriendEditViewModel : ViewModelBase, IFriendEditViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IAutoMapperService _mapper;
        private readonly IMediator _mediator;
        private FriendEmailWrapper _selectedEmail;

        public FriendEditViewModel(IEventAggregator eventAggregator,
            IAutoMapperService mapper, 
            IMediator mediator)
        {
            _eventAggregator = eventAggregator;
            _mapper = mapper;
            _mediator = mediator;
            DeleteFriendCommand = new AsyncDelegateCommand(async a => await OnDeleteFriendExecute(a));
            SaveFriendCommand = new AsyncDelegateCommand(async a => await OnSaveExecute(a));
            RemoveFriendEmailCommand = new DelegateCommand(OnRemoveFriendEmail);
            RejectChangesCommand = new DelegateCommand(OnRejectChangesExecute);
            AddFriendEmailCommand = new DelegateCommand(OnAddFriendEmailExecute);
        }

        public async Task Load(int? friendId = null)
        {
            var friendEntity = friendId != null ?
                await this._mediator.SendAsync(new GetFriendRequest {FriendId = friendId.Value}) :
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

        public FriendEmailWrapper SelectedFriendEmail
        {
            get { return GetValue<FriendEmailWrapper>(); }
            set { SetValue(value); }
        }

        public ICommand DeleteFriendCommand { get; set; }
        public ICommand SaveFriendCommand { get; set; }
        public ICommand RejectChangesCommand { get; set; }
        public ICommand AddFriendEmailCommand { get; set; }
        public ICommand RemoveFriendEmailCommand { get; set; }

        private async Task OnDeleteFriendExecute(object obj)
        {
            var friendEntity = this._mapper.MapTo<FriendVm, Friend>(Friend.Model);
            await this._mediator.SendAsync(new DeleteFriendRequest { Friend = friendEntity });
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
            addedEmails.ForEach(a => a.FriendId = Friend.Id);
            addedEmails = await this._mediator.SendAsync(new AddFriendEmailListRequest {FriendEmails = addedEmails});
            for (var i = 0; i < Friend.Emails.AddedItems.Count; i++)
            {
                Friend.Emails.AddedItems[i].Id = addedEmails[i].Id;
            }
            var modifiedEmails = this._mapper.MapTo<IEnumerable<FriendEmailVm>, IEnumerable<FriendEmail>>(Friend.Emails.ModifiedItems.Select(wrapper => wrapper.Model)).ToList();
            await this._mediator.SendAsync(new UpdateFriendEmailListRequest { FriendEmails = modifiedEmails });
        }

        private async Task AddOrUpdateAddress()
        {
            var addressEntity = this._mapper.MapTo<AddressVm, Address>(Friend.Address.Model);
            if (addressEntity.Id == 0)
            {
                addressEntity = await this._mediator.SendAsync(new AddAddressRequest { Address = addressEntity });
            }
            else
            {
                await this._mediator.SendAsync(new UpdateAddressRequest { Address = addressEntity });
            }
            Friend.Address.Id = addressEntity.Id;
        }
        private async Task AddOrUpdateFriend()
        {
            var friendEntity = this._mapper.MapTo<FriendVm, Friend>(Friend.Model);
            friendEntity.Emails = null;
            if (friendEntity.Id == 0)
            {
                friendEntity = await this._mediator.SendAsync(new AddFriendRequest {Friend = friendEntity});
            }
            else
            {
                await this._mediator.SendAsync(new UpdateFriendRequest { Friend = friendEntity });
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

        private void OnRemoveFriendEmail(object obj)
        {
            if (SelectedFriendEmail != null)
            {
                Friend.Emails.Remove(SelectedFriendEmail);
            }
        }
    }
}
