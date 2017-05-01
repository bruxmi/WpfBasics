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
    public class ValidationCollectionProperty
    {
        private FriendVm _friend;

        [TestInitialize]
        public void Initialize()
        {
            _friend = new FriendVm
            {
                FirstName = "Thomas",
                Address = new AddressVm { City = "Berlin" },
                Emails = new List<FriendEmailVm>
                {
                    new FriendEmailVm { Email = "test@test.de" },
                    new FriendEmailVm { Email = "test2@test.de" }
                }
            };
        }

        [TestMethod]
        public void ShouldSetIsValidOfRoot()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.IsTrue(wrapper.IsValid);

            wrapper.Emails.First().Email = "";
            Assert.IsFalse(wrapper.IsValid);

            wrapper.Emails.First().Email = "test@test.de";
            Assert.IsTrue(wrapper.IsValid);
            Assert.IsFalse(wrapper.HasErrors);
        }

        [TestMethod]
        public void ShouldSetIsValidOfRootWhenInitializing()
        {
            _friend.Emails.First().Email = "";
            var wrapper = new FriendWrapper(_friend);
            Assert.IsFalse(wrapper.IsValid);
            Assert.IsFalse(wrapper.HasErrors);
            Assert.IsTrue(wrapper.Emails.First().HasErrors);
        }

        [TestMethod]
        public void ShouldSetIsValidOfRootWhenRemovingInvalidItem()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.IsTrue(wrapper.IsValid);

            wrapper.Emails.First().Email = "";
            Assert.IsFalse(wrapper.IsValid);

            wrapper.Emails.Remove(wrapper.Emails.First());
            Assert.IsTrue(wrapper.IsValid);
        }

        [TestMethod]
        public void ShouldSetIsValidOfRootWhenAddingInvalidItem()
        {
            var emailToAdd = new FriendEmailWrapper(new FriendEmailVm());
            var wrapper = new FriendWrapper(_friend);
            Assert.IsTrue(wrapper.IsValid); ;
            wrapper.Emails.Add(emailToAdd);
            Assert.IsFalse(wrapper.IsValid);
            emailToAdd.Email = "test@test.com";
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

            wrapper.Emails.First().Email = "";
            Assert.IsTrue(fired);

            fired = false;
            wrapper.Emails.First().Email = "test@test.de";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsValidOfRootWhenRemovingInvalidItem()
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

            wrapper.Emails.First().Email = "";
            Assert.IsTrue(fired);

            fired = false;
            wrapper.Emails.Remove(wrapper.Emails.First());
            Assert.IsTrue(fired);
        }


        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsValidOfRootWhenAddingInvalidItem()
        {
            var fired = false;
            var wrapper = new FriendWrapper(_friend);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "IsValid")
                {
                    fired = true;
                }
            };

            var emailToAdd = new FriendEmailWrapper(new FriendEmailVm());
            wrapper.Emails.Add(emailToAdd);
            Assert.IsTrue(fired);

            fired = false;
            emailToAdd.Email = "test@test.de";
            Assert.IsTrue(fired);
        }

    }
}
