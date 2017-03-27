using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WpfViewModelBasics.UI.Interfaces;
using WpfViewModelBasics.UI.ViewModel.Base;

namespace WpfViewModelBasics.UI.ViewModel
{

    public class Item : ViewModelBase
    {
        public Item()
        {
            Items = new ObservableCollection<Item>();
        }

        public ObservableCollection<Item> Items
        {
            get { return GetValue<ObservableCollection<Item>>(); }
            set { SetValue(value); }
        }

        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }

    public class Group : Item
    {

    }

    public class Entry : Item
    {

    }

    public class FriendTreeViewModel : ViewModelBase, IFriendTreeViewModel
    {


        public FriendTreeViewModel()
        {
        }

        public ObservableCollection<Group> Items
        {
            get
            {
                var result = new ObservableCollection<Group>();
                for (int i = 0; i < 5; i++)
                {
                    result[i].Name = $"Group {i + 1}";
                    result[i].Items.Add(
                    new Group()
                    {
                        Name = $"Entry {i + 1}"
                    });
                }

                return result;
            }
        }
    }
}
