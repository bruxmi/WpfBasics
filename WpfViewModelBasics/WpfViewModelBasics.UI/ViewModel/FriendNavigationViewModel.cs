
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
        }

        public ObservableCollection<FriendNavigationItemViewModel> NavigationItems
        {
            get { return GetValue<ObservableCollection<FriendNavigationItemViewModel>>(); }
            private set { SetValue(value);}
        }


        public void Load()
        {
            var friendEntities = this._friendQueryService.GetAllFriends();
            var friends = this._mappingService.MapTo<IEnumerable<Friend>, IEnumerable<FriendVm>>(friendEntities);
            NavigationItems.Clear();
            foreach (var friend in friends)
            {
                NavigationItems.Add(new FriendNavigationItemViewModel(friend.Id, friend.FirstName + " " + friend.LastName,_eventAggregator));
            }
        }
    }
}
