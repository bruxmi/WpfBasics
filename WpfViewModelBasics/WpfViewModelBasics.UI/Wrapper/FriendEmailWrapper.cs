using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfViewModelBasics.Core.Entities;
using WpfViewModelBasics.ViewModelMapping.ViewModel;

namespace WpfViewModelBasics.UI.Wrapper
{
    public class FriendEmailWrapper : ModelWrapper<FriendEmailVm>
    {

        public FriendEmailWrapper(FriendEmailVm model) : base(model)
        {

        }
        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int IdOriginalValue => GetOriginalValue<int>(nameof(Id));

        public bool IdIsChanged => GetIsChanged(nameof(Id));

        public string Email
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string EmailOriginalValue => GetOriginalValue<string>(nameof(Email));

        public bool EmailIsChanged => GetIsChanged(nameof(Email));


        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                yield return new CustomErrorResult("Email is required", new[] { nameof(this.Email) }, CustomErrorResult.ErrorLevel.Error);
            }
            if (!new EmailAddressAttribute().IsValid(Email))
            {
                yield return new CustomErrorResult("Email is not a valid email address", new[] { nameof(this.Email) }, CustomErrorResult.ErrorLevel.Error);
            }
        }
    }
}
