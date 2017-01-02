using System;
using System.Collections.Generic;

namespace WpfViewModelBasics.ViewModelMapping.ViewModel
{
    public class FriendVm
    {
        public FriendVm()
        {
            this.Emails = new List<FriendEmailVm>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }

        public bool IsDeveloper { get; set; }

        public AddressVm Address { get; set; }

        public List<FriendEmailVm> Emails { get; set; }
    }
}
