using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfViewModelBasics.UnitTests.ViewModel.TestObject;

namespace WpfViewModelBasics.UnitTests.ViewModel
{
    [TestClass]
    public class ChangeNotificationCollectionPropertyTests
    {
        private UserViewModel _userViewModel;
        private ObservableCollection<EmailViewModel> _emailsViewModels;


        [TestInitialize]
        public void Initialize()
        {
            _emailsViewModels = new ObservableCollection<EmailViewModel>
            {
                new EmailViewModel {Id = 1, Email = "thomas@thomas.com"},
                new EmailViewModel {Id = 1, Email = "julia@juhu-design.com"}
            };
            _userViewModel = new UserViewModel
            {
                SingleEmailViewModel = new EmailViewModel(),
                DisplayName = "Thomas",
                EmailViewModelList = _emailsViewModels
            };
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventWhenPropertyChanged()
        {
            var fired = false;
            _userViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_userViewModel.EmailViewModelList))
                {
                    fired = true;
                }
            };

            _userViewModel.EmailViewModelList = new ObservableCollection<EmailViewModel>();
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldNotRaisePropertyCHangedEventIfPropertyIsSetToSameValue()
        {
            var fired = false;
            _userViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_userViewModel.EmailViewModelList))
                {
                    fired = true;
                }
            };

            _userViewModel.EmailViewModelList = _emailsViewModels;
            Assert.IsFalse(fired);
        }
    }
}
