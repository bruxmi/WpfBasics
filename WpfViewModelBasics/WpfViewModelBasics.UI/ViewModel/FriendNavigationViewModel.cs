
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfViewModelBasics.UI.Command;
using WpfViewModelBasics.UI.Events;

namespace WpfViewModelBasics.UI.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Prism.Events;
    using Core.Entities;
    using Core.Interfaces.Services.Query;
    using Interfaces;
    using Base;
    using ViewModelMapping.MappingServices;
    using ViewModelMapping.ViewModel;
    using System;

    public class FriendNavigationViewModel: ViewModelBase, IFriendNavigationViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IAutoMapperService _mappingService;
        private readonly IFriendQueryService _friendQueryService;

        public FriendNavigationViewModel(IEventAggregator eventAggregator, 
            IAutoMapperService mappingService, 
            IFriendQueryService friendQueryService)
        {
            _eventAggregator = eventAggregator;
            _mappingService = mappingService;
            _friendQueryService = friendQueryService;
            NavigationItems = new ObservableCollection<FriendNavigationItemViewModel>();
            _eventAggregator.GetEvent<SaveFriendEditViewEvent>().Subscribe(OnFriendSaved);
            _eventAggregator.GetEvent<DeleteFriendEvent>().Subscribe(a => RemoveNavigationItem(a));
        }

        private void OnFriendSaved(FriendVm savedFriend)
        {
            var navigationItem = NavigationItems.SingleOrDefault(item => item.FriendId == savedFriend.Id);
            if (navigationItem != null)
            {
                navigationItem.DisplayName = $"{savedFriend.FirstName} {savedFriend.LastName}";
            }
            else
            {
                NavigationItems.Add(new FriendNavigationItemViewModel(savedFriend.Id, savedFriend.FirstName + " " + savedFriend.LastName, _eventAggregator));
            }
        }

        private void RemoveNavigationItem(int? friendId)
        {
            if (friendId != null)
            {
                var navigationItemToDelete = NavigationItems.SingleOrDefault(s => s.FriendId == friendId.Value);
                NavigationItems.Remove(navigationItemToDelete);
            }
        }

        public ObservableCollection<FriendNavigationItemViewModel> NavigationItems
        {
            get { return GetValue<ObservableCollection<FriendNavigationItemViewModel>>(); }
            private set { SetValue(value);}
        }


        public async Task Load()
        {
            var friendEntities = await this._friendQueryService.GetAllFriendsAsync();
            var friends = this._mappingService.MapTo<IEnumerable<Friend>, IEnumerable<FriendVm>>(friendEntities);
            NavigationItems.Clear();
            foreach (var friend in friends)
            {
                NavigationItems.Add(new FriendNavigationItemViewModel(friend.Id, friend.FirstName + " " + friend.LastName,_eventAggregator));
            }
        }
    }
}
