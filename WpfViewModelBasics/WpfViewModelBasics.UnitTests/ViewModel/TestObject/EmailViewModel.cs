using WpfViewModelBasics.UI.ViewModel.Base;

namespace WpfViewModelBasics.UnitTests.ViewModel.TestObject
{
    public class EmailViewModel: ViewModelBase
    {
        public string Email
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
    }
}
