using WpfViewModelBasics.UI.ViewModel.Base;

namespace WpfViewModelBasics.UI.ViewModel
{
    using System.Windows.Input;
    using Prism.Events;
    using WpfViewModelBasics.UI.Command;
    using WpfViewModelBasics.UI.Events;

    public class FriendNavigationItemViewModel: ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;

        public FriendNavigationItemViewModel(int friendId, string displayName, IEventAggregator eventAggregator)
        {
            this.FriendId = friendId;
            this.DisplayName = displayName;
            this._eventAggregator = eventAggregator;
            OpenFriendEditViewCommand = new DelegateCommand(OpenFriendEditViewExecute);
        }

        public ICommand OpenFriendEditViewCommand { get; set; }

        public string DisplayName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        private void OpenFriendEditViewExecute(object obj)
        {
            _eventAggregator.GetEvent<OpenFriendEditViewEvent>().Publish(FriendId);
        }

        public int FriendId { get; private set; }
    }
}
