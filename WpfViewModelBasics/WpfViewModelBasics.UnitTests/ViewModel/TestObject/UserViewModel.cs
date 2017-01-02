using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfViewModelBasics.UI.ViewModel.Base;

namespace WpfViewModelBasics.UnitTests.ViewModel.TestObject
{
    public class UserViewModel : ViewModelBase
    {
        public string DisplayName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public ObservableCollection<EmailViewModel> EmailViewModelList
        {
            get { return GetValue<ObservableCollection<EmailViewModel>>(); }
            set { SetValue(value); }
        }

        public EmailViewModel SingleEmailViewModel
        {
            get { return GetValue<EmailViewModel>(); }
            set { SetValue(value); }
        }
    }
}
