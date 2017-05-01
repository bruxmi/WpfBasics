using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfViewModelBasics.UI.Wrapper;
using WpfViewModelBasics.ViewModelMapping.ViewModel;

namespace WpfViewModelBasics.UnitTests.Wrapper
{
    [TestClass]
    public class ValidateComplexProperty
    {
        private FriendVm _friend;

        [TestInitialize]
        public void Initialize()
        {
            _friend = new FriendVm
            {
                FirstName = "Thomas",
                Address = new AddressVm { City = "Berlin" },
                Emails = new List<FriendEmailVm>()
            };
        }

        [TestMethod]
        public void ShouldSetIsValidOfRoot()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.IsTrue(wrapper.IsValid);

            wrapper.Address.City = "";
            Assert.IsFalse(wrapper.IsValid);

            wrapper.Address.City = "Potsdam";
            Assert.IsTrue(wrapper.IsValid);

        }

        [TestMethod]
        public void ShouldSetIsValidOfRootAfterInitialization()
        {
            _friend.Address.City = "";
            var wrapper = new FriendWrapper(_friend);
            Assert.IsFalse(wrapper.IsValid);
            wrapper.Address.City = "Potsdam";
            Assert.IsTrue(wrapper.IsValid);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsValidOfRoot()
        {
            var fired = false;
            var wrapper = new FriendWrapper(_friend);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(wrapper.IsValid))
                {
                    fired = true;
                }
            };

            wrapper.Address.City = "";
            Assert.IsTrue(fired);
            fired = false;
            wrapper.Address.City = "Potsdam";
            Assert.IsTrue(fired);
        }
    }
}
