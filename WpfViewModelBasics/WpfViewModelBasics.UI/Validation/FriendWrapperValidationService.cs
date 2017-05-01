using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfViewModelBasics.UI.Wrapper;

namespace WpfViewModelBasics.UI.Validation
{
    public class FriendWrapperValidationService
    {
        public bool Validate(FriendWrapper wrapper, out ICollection<CustomErrorType> validationErrors)
        {
            validationErrors = new List<CustomErrorType>();

            if (string.IsNullOrWhiteSpace(wrapper.FirstName))
            {
                validationErrors.Add(new CustomErrorType("Firstname is required", Severity.WARNING));
            }
            if (wrapper.IsDeveloper && wrapper.Emails.Count == 0)
            {
                validationErrors.Add(new CustomErrorType("A developer must have an email-address", Severity.ERROR));
            }

            return validationErrors.Any(a => a.Severity == Severity.ERROR);
        }
    }
}
