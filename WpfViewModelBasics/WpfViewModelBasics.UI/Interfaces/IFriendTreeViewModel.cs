using System.Collections.Generic;
using System.Collections.ObjectModel;
using WpfViewModelBasics.UI.ViewModel;

namespace WpfViewModelBasics.UI.Interfaces
{
    public interface IFriendTreeViewModel
    {
        ObservableCollection<Item> Items { get; set; }
    }
}