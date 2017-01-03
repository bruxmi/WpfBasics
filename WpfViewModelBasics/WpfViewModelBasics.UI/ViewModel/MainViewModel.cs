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
using WpfViewModelBasics.ViewModelMapping.ViewModel;


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
            _eventAggregator.GetEvent<OpenFriendEditViewEvent>().Subscribe(async a => await OnOpenFriendTab(a));

            _friendEditViewModelCreator = friendEditViewModelCreator;
            _messageDialogService = messageDialogService;
            this.NavigationViewModel = navigationViewModel;
            this.FriendEditViewModels = new ObservableCollection<IFriendEditViewModel>();
            CloseFriendTabCommand = new AsyncDelegateCommand(async friend => await CloseFriendEditViewExecute(friend as IFriendEditViewModel));
            AddFriendCommand = new AsyncDelegateCommand(OnAddFriendExecute);
            _eventAggregator.GetEvent<DeleteFriendEvent>().Subscribe(a => DeleteFriendEditViewExecute(a));
        }

        private void DeleteFriendEditViewExecute(int? friendId)
        {
            if (friendId != null)
            {
                var friendEditViewModel = FriendEditViewModels.SingleOrDefault(s => s.Friend.Id == friendId.Value);
                FriendEditViewModels.Remove(friendEditViewModel);
            }
        }

        public ObservableCollection<IFriendEditViewModel> FriendEditViewModels { get; }

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

        public IFriendNavigationViewModel NavigationViewModel { get; }

        public ICommand CloseFriendTabCommand { get; set; }

        public ICommand AddFriendCommand { get; set; }

        public bool IsLoading
        {
            get { return GetValue<bool>(); }
            set
            {
                SetValue(value);
                CanShowFriendTab = !value;
            }
        }

        public bool CanShowFriendTab
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }
        
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

        private async Task OnOpenFriendTab(int friendId)
        {
            var friendEditVm = FriendEditViewModels.SingleOrDefault(vm => vm.Friend.Id == friendId);
            if (friendEditVm == null)
            {
                friendEditVm = this._friendEditViewModelCreator();
                FriendEditViewModels.Add(friendEditVm);
                await friendEditVm.Load(friendId);
            }
            SelectedFriendEditViewModel = friendEditVm;
        }

        private async Task OnAddFriendExecute(object obj)
        {
            var friendEditVm = this._friendEditViewModelCreator();
            FriendEditViewModels.Add(friendEditVm);
            await friendEditVm.Load();
            SelectedFriendEditViewModel = friendEditVm;        
        }

        public async Task Load()
        {
            IsLoading = true;
            FriendEditViewModels.Clear();
            await NavigationViewModel.Load();
            IsLoading = false;
        }
    }
}
