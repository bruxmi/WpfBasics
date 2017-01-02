using System.Collections.ObjectModel;
using Prism.Events;
using WpfViewModelBasics.Core.Interfaces.Services.Query;
using WpfViewModelBasics.UI.Interfaces;
using WpfViewModelBasics.UI.ViewModel.Base;
using WpfViewModelBasics.ViewModelMapping.MappingServices;

namespace WpfViewModelBasics.UI.ViewModel
{
    public class FriendNavigationViewModel: ViewModelBase, IFriendNavigationViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IFriendMappingService _friendMappingService;
        private readonly IFriendQueryService _friendQueryService;

        public FriendNavigationViewModel(IEventAggregator eventAggregator, 
            IFriendMappingService friendMappingService, 
            IFriendQueryService friendQueryService)
        {
            _eventAggregator = eventAggregator;
            _friendMappingService = friendMappingService;
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
            var friends = this._friendMappingService.EntityToViewModel(this._friendQueryService.GetAllFriends());
            NavigationItems.Clear();
            foreach (var friend in friends)
            {
                NavigationItems.Add(new FriendNavigationItemViewModel(friend.Id, friend.FirstName + " " + friend.LastName,_eventAggregator));
            }
        }
    }
}
