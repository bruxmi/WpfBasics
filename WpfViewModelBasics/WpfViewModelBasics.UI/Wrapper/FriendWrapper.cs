using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfViewModelBasics.Core.Entities;
using WpfViewModelBasics.UI.ViewModel;
using WpfViewModelBasics.UI.Wrapper;
using WpfViewModelBasics.ViewModelMapping.ViewModel;

namespace WpfViewModelBasics.UI.Wrapper
{
    public class FriendWrapper : ModelWrapper<FriendVm>
    {
        public FriendWrapper(FriendVm model) : base(model)
        {
            InitializeComplexProperties(model);
            InitializeCollectionProperties(model);
        }

        private void InitializeCollectionProperties(FriendVm model)
        {
            if (model.Emails == null)
            {
                throw new ArgumentException("Emails cannot be null");
            }
            Emails = new ChangeTrackingCollection<FriendEmailWrapper>(model.Emails.Select(e => new FriendEmailWrapper(e)));
            RegisterCollection(Emails, model.Emails);
        }

        private void InitializeComplexProperties(FriendVm model)
        {
            if (model.Address == null)
            {
                throw new ArgumentException("Address cannot be null");
            }
            Address = new AddressWrapper(model.Address);
            RegisterComplexProperties(Address);
        }

        public string FirstName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string FirstNameOriginalValue => GetOriginalValue<string>(nameof(this.FirstName));

        public bool FirstNameIsChanged => GetIsChanged(nameof(this.FirstName));

        public string LastName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string LastNameOriginalValue => GetOriginalValue<string>(nameof(this.LastName));

        public bool LastNameIsChanged => GetIsChanged(nameof(this.LastName));

        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int FriendGroupId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public DateTime? Birthday
        {
            get { return GetValue<DateTime?>(); }
            set { SetValue(value); }
        }

        public DateTime? BirthdayOriginalValue => GetOriginalValue<DateTime?>(nameof(this.Birthday));

        public bool BirthdayIsChanged => GetIsChanged(nameof(this.Birthday));

        public bool IsDeveloper
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public bool IsDeveloperOriginalValue => GetOriginalValue<bool>(nameof(this.IsDeveloper));

        public bool IsDeveloperIsChanged => GetIsChanged(nameof(this.IsDeveloper));


        public AddressWrapper Address { get; private set; }
        public ChangeTrackingCollection<FriendEmailWrapper> Emails { get; private set; }

    }
}