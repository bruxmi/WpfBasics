using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfViewModelBasics.Core.Entities;
using WpfViewModelBasics.UI.Wrapper;
using WpfViewModelBasics.ViewModelMapping.ViewModel;

namespace WpfViewModelBasics.UnitTests.Wrapper
{
    [TestClass]
    public class ChangeTrackingCollectionProperty
    {
        private FriendVm _friend;

        [TestInitialize]
        public void Initialize()
        {
            _friend = new FriendVm
            {
                FirstName = "Thomas",
                Address = new AddressVm(),
                Emails = new List<FriendEmailVm>
                {
                    new FriendEmailVm {Email = "thomas@thomas.com"},
                    new FriendEmailVm {Email = "julia@juhu-design.com"}
                }
            };
        }

        [TestMethod]
        public void ShouldSetIsChangedOfFriendWrapper()
        {
            var wrapper = new FriendWrapper(_friend);
            var emailToModify = wrapper.Emails.First();
            emailToModify.Email = "modified@thomas.com";

            Assert.IsTrue(wrapper.IsChanged);

            emailToModify.Email = "thomas@thomas.com";
            Assert.IsFalse(wrapper.IsChanged);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsChangedPropertyOfFriendWrapper()
        {
            var fired = false;
            var wrapper = new FriendWrapper(_friend);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(wrapper.IsChanged))
                {
                    fired = true;
                }
            };

            wrapper.Emails.First().Email = "modified@thomas.com";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldAcceptChanges()
        {
            var wrapper = new FriendWrapper(_friend);

            var emailToModify = wrapper.Emails.First();
            emailToModify.Email = "modified@thomas.com";

            Assert.IsTrue(wrapper.IsChanged);

            wrapper.AcceptChanges();

            Assert.IsFalse(wrapper.IsChanged);
            Assert.AreEqual("modified@thomas.com", emailToModify.Email);
            Assert.AreEqual("modified@thomas.com", emailToModify.EmailOriginalValue);
        }

        [TestMethod]
        public void ShouldRejectChanges()
        {
            var wrapper = new FriendWrapper(_friend);

            var emailToModify = wrapper.Emails.First();
            emailToModify.Email = "modified@thomas.com";

            Assert.IsTrue(wrapper.IsChanged);

            wrapper.RejectChanges();

            Assert.IsFalse(wrapper.IsChanged);
            Assert.AreEqual("thomas@thomas.com", emailToModify.Email);
            Assert.AreEqual("thomas@thomas.com", emailToModify.EmailOriginalValue);
        }
    }
}