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
    public class AddressWrapper:ModelWrapper<AddressVm>
    {
        public AddressWrapper(AddressVm model): base(model)
        {
        }

        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value);}
        }

        public string IdOriginalValue => GetOriginalValue<string>(nameof(this.Id));

        public bool IdIsChanged => GetIsChanged(nameof(this.Id));

        public string City
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string CityOriginalValue => GetOriginalValue<string>(nameof(this.City));

        public bool CityIsChanged => GetIsChanged(nameof(this.City));

        public string StreetNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string StreetNumberOriginalValue => GetOriginalValue<string>(nameof(this.StreetNumber));

        public bool StreetNumberIsChanged => GetIsChanged(nameof(this.StreetNumber));

        public string Street
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string StreetOriginalValue => GetOriginalValue<string>(nameof(this.Street));

        public bool StreetIsChanged => GetIsChanged(nameof(this.Street));

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return new CustomErrorResult("City is required", new[] { nameof(City) }, CustomErrorResult.ErrorLevel.Warning);
        }
    }
}
