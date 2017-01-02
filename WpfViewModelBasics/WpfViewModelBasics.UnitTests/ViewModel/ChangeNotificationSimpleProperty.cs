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
    public class ChangeNotificationSimpleProperty
    {
        private UserViewModel _userViewModel;


        [TestInitialize]
        public void Initialize()
        {
            _userViewModel = new UserViewModel
            {
                SingleEmailViewModel = new EmailViewModel(),
                DisplayName = "Thomas",
                EmailViewModelList = new ObservableCollection<EmailViewModel>()
            };
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventWhenPropertyChanged()
        {
            var fired = false;
            _userViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_userViewModel.DisplayName))
                {
                    fired = true;
                }
            };
            _userViewModel.DisplayName = "Julia";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldNotRaisePropertyCHangedEventIfPropertyIsSetToSameValue()
        {
            var fired = false;
            _userViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_userViewModel.DisplayName))
                {
                    fired = true;
                }
            };
            _userViewModel.DisplayName = "Thomas";
            Assert.IsFalse(fired);
        }
    }
}
