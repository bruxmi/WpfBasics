using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Events;
using WpfViewModelBasics.Core.Entities;
using WpfViewModelBasics.UI.Command;
using WpfViewModelBasics.UI.Enums;
using WpfViewModelBasics.UI.Events;
using WpfViewModelBasics.UI.Interfaces;
using WpfViewModelBasics.UI.ViewModel.Base;


namespace WpfViewModelBasics.UI.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly Func<IFriendEditViewModel> _friendEditViewModelCreator;
        private readonly IMessageDialogService _messageDialogService;

        public MainViewModel(IEventAggregator eventAggregator,
            IFriendNavigationViewModel navigationViewModel, 
            Func<IFriendEditViewModel> friendEditViewModelCreator, 
            IMessageDialogService messageDialogService) 
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenFriendEditViewEvent>().Subscribe(OnOpenFriendTab);

            _friendEditViewModelCreator = friendEditViewModelCreator;
            _messageDialogService = messageDialogService;
            this.NavigationViewModel = navigationViewModel;
            this.FriendEditViewModels = new ObservableCollection<IFriendEditViewModel>();
            CloseFriendTabCommand = new AsyncDelegateCommand(async friend => await CloseFriendEditViewExecute(friend as IFriendEditViewModel));
        }


        public ObservableCollection<IFriendEditViewModel> FriendEditViewModels { get; private set; }

        public IFriendEditViewModel SelectedFriendEditViewModel
        {
            get { return GetValue<IFriendEditViewModel>(); }
            set { SetValue(value); }
        }

        public IFriendEditViewModel FriendToClose
        {
            get { return GetValue<IFriendEditViewModel>(); }
            set { SetValue(value); }
        }

        public IFriendNavigationViewModel NavigationViewModel { get; private set; }


        public ICommand CloseFriendTabCommand { get; set; }

        private async Task CloseFriendEditViewExecute(IFriendEditViewModel friendEditVmToClose)
        {
            if (friendEditVmToClose != null)
            {
                if (friendEditVmToClose.Friend.IsChanged)
                {
                    var result = await this._messageDialogService.ShowYesNoDialog("Close Tab", "Do you want to close the tab?");
                    if (result == MessageDialogResult.No)
                    {
                        return;
                    }
                }
            }
            FriendEditViewModels.Remove(friendEditVmToClose);
        }

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
