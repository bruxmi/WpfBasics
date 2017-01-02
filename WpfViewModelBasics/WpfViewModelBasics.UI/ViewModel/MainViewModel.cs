using System;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Events;
using WpfViewModelBasics.UI.Events;
using WpfViewModelBasics.UI.Interfaces;
using WpfViewModelBasics.UI.ViewModel.Base;


namespace WpfViewModelBasics.UI.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly Func<IFriendEditViewModel> _friendEditViewModelCreator;

        public MainViewModel(IEventAggregator eventAggregator,
            IFriendNavigationViewModel navigationViewModel, 
            Func<IFriendEditViewModel> friendEditViewModelCreator) 
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenFriendEditViewEvent>().Subscribe(OnOpenFriendTab);
            _friendEditViewModelCreator = friendEditViewModelCreator;
            this.NavigationViewModel = navigationViewModel;
            this.FriendEditViewModels = new ObservableCollection<IFriendEditViewModel>();
        }

        public ObservableCollection<IFriendEditViewModel> FriendEditViewModels { get; private set; }

        public IFriendEditViewModel SelectedFriendEditViewModel
        {
            get { return GetValue<IFriendEditViewModel>(); }
            set { SetValue(value); }
        }

        public IFriendNavigationViewModel NavigationViewModel { get; private set; }

        private void OnOpenFriendTab(int friendId)
        {
            var friendEditVm = FriendEditViewModels.SingleOrDefault(vm => vm.Friend.Id == friendId);
            if (friendEditVm == null)
            {
                friendEditVm = this._friendEditViewModelCreator();
                FriendEditViewModels.Add(friendEditVm);
                friendEditVm.Load(friendId);
            }
            SelectedFriendEditViewModel = friendEditVm;
        }

        public void Load()
        {
            NavigationViewModel.Load();
        }
    }
}
