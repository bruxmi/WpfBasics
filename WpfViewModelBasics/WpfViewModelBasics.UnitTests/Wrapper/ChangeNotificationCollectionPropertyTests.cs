using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfViewModelBasics.Core.Entities;
using WpfViewModelBasics.UI.Wrapper;
using WpfViewModelBasics.ViewModelMapping.ViewModel;

namespace WpfViewModelBasics.UnitTests.Wrapper
{
    [TestClass]
    public class ChangeNotificationCollectionPropertyTests
    {
        private FriendVm _friend;
        private FriendEmailVm _friendEmail;

        [TestInitialize]
        public void Initialize()
        {
            _friendEmail = new FriendEmailVm
            {
                Email = "thomas@thomas.com",
            };
            _friend = new FriendVm
            {
                FirstName = "Thomas",
                Address = new AddressVm(),
                Emails = new List<FriendEmailVm>
                {
                    new FriendEmailVm { Email = "julia@juhu-design.com" },
                    _friendEmail
                }
            };
        }

        [TestMethod]
        public void ShouldInitializeEmailsProperty()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.IsNotNull(wrapper.Emails);
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }

        [TestMethod]
        public void ShouldBeInSyncAfterAddingEmail()
        {
            _friend.Emails.Remove(_friendEmail);
            var wrapper = new FriendWrapper(_friend);
            wrapper.Emails.Add(new FriendEmailWrapper(_friendEmail));
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }

        [TestMethod]
        public void ShouldBeInSyncAfterRemovingEmail()
        {
            var wrapper = new FriendWrapper(_friend);
            var emailToRemove = wrapper.Emails.Single(e => e.Model == _friendEmail);
            wrapper.Emails.Remove(emailToRemove);
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }

        [TestMethod]
        public void ShouldBeInSyncAfterClearingEmails()
        {
            var wrapper = new FriendWrapper(_friend);
            wrapper.Emails.Clear();
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }

        private void CheckIfModelEmailsCollectionIsInSync(FriendWrapper wrapper)
        {
            Assert.AreEqual(_friend.Emails.Count, wrapper.Emails.Count);
            Assert.IsTrue(_friend.Emails.All(e => wrapper.Emails.Any(emailWrapper => emailWrapper.Model == e)));
        }
    }
}
