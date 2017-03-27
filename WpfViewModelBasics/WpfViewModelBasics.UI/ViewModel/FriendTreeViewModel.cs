namespace WpfViewModelBasics.UI.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using WpfViewModelBasics.UI.Interfaces;
    using WpfViewModelBasics.UI.ViewModel.Base;

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

            this.Items = this.CreateTree();
        }

        public ObservableCollection<Item> Items
        {
            get { return GetValue<ObservableCollection<Item>>(); }
            set { SetValue(value); }
        }

        private ObservableCollection<Item> CreateTree()
        {
            var items = new ObservableCollection<Item>
            {
                new Group { Name = "Root" }
            };
            foreach (var item in items)
            {
                for (int i = 0; i < 5; i++)
                {
                    item.Items.Add(new Entry() { Name = $"Entry {i + 1}" });
                }
            }
            return items;
        }
    }
}
