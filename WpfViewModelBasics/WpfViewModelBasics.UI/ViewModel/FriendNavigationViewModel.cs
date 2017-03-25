namespace WpfViewModelBasics.UI.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Base;
    using Core.Entities;
    using Events;
    using Interfaces;
    using Prism.Events;
    using ViewModelMapping.MappingServices;
    using ViewModelMapping.ViewModel;
    using WpfViewModelBasics.Core.Interfaces.Services.Query;

    public class FriendNavigationViewModel : ViewModelBase, IFriendNavigationViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IAutoMapperService _mappingService;
        private readonly IFriendQueryService _friendQueryService;

        public FriendNavigationViewModel(IEventAggregator eventAggregator,
            IAutoMapperService mappingService,
            IFriendQueryService friendQueryService)
        {
            this._eventAggregator = eventAggregator;
            this._mappingService = mappingService;
            this._friendQueryService = friendQueryService;
            NavigationItems = new ObservableCollection<FriendNavigationItemViewModel>();
            _eventAggregator.GetEvent<SaveFriendEditViewEvent>().Subscribe(OnFriendSaved);
            _eventAggregator.GetEvent<DeleteFriendEvent>().Subscribe(a => RemoveNavigationItem(a));
        }

        public ObservableCollection<FriendNavigationItemViewModel> NavigationItems
        {
            get { return GetValue<ObservableCollection<FriendNavigationItemViewModel>>(); }
            private set { SetValue(value); }
        }


        public async Task Load()
        {
            var friendEntities = await this._friendQueryService.GetAllFriend();
            var friends = _mappingService.MapTo<IEnumerable<Friend>, IEnumerable<FriendVm>>(friendEntities);
            NavigationItems.Clear();
            foreach (var friend in friends)
            {
                NavigationItems.Add(new FriendNavigationItemViewModel(friend.Id, friend.FirstName + " " + friend.LastName, _eventAggregator));
            }
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
            if (friendId == null)
            {
                return;
            }
            var navigationItemToDelete = NavigationItems.SingleOrDefault(s => s.FriendId == friendId.Value);
            NavigationItems.Remove(navigationItemToDelete);
        }
    }
}