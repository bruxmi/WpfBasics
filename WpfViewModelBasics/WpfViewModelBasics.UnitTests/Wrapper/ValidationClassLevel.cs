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
    public class ValidationClassLevel
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
                  new FriendEmailVm { Email="test@test.de" },
                  new FriendEmailVm {Email="test2@test.de" }
                }
            };
        }

        [TestMethod]
        public void ShouldHaveErrorsAndNotBeValidWhenIsDeveloperIsTrueAndNoEmailExists()
        {
            var expectedError = "A developer must have an email-address";

            var wrapper = new FriendWrapper(_friend);
            wrapper.Emails.Clear();
            Assert.IsFalse(wrapper.IsDeveloper);
            Assert.IsTrue(wrapper.IsValid);

            wrapper.IsDeveloper = true;
            Assert.IsFalse(wrapper.IsValid);

            var emailsErrors = wrapper.GetErrors(nameof(wrapper.Emails)).Cast<CustomErrorResult>().ToList();
            Assert.AreEqual(1, emailsErrors.Count);
            Assert.AreEqual(expectedError, emailsErrors.Single().ErrorMessage);

            var isDeveloperErrors = wrapper.GetErrors(nameof(wrapper.IsDeveloper)).Cast<CustomErrorResult>().ToList();
            Assert.AreEqual(1, isDeveloperErrors.Count);
            Assert.AreEqual(expectedError, isDeveloperErrors.Single().ErrorMessage);
        }

        [TestMethod]
        public void ShouldBeValidAgainWhenIsDeveloperIsSetBackToFalse()
        {
            var wrapper = new FriendWrapper(_friend);
            wrapper.Emails.Clear();
            Assert.IsFalse(wrapper.IsDeveloper);
            Assert.IsTrue(wrapper.IsValid);

            wrapper.IsDeveloper = true;
            Assert.IsFalse(wrapper.IsValid);


            wrapper.IsDeveloper = false;
            Assert.IsTrue(wrapper.IsValid);

            var emailsErrors = wrapper.GetErrors(nameof(wrapper.Emails)).Cast<string>().ToList();
            Assert.AreEqual(0, emailsErrors.Count);

            var isDeveloperErrors = wrapper.GetErrors(nameof(wrapper.IsDeveloper)).Cast<string>().ToList();
            Assert.AreEqual(0, isDeveloperErrors.Count);
        }

        [TestMethod]
        public void ShouldBeValidAgainWhenEmailIsAdded()
        {
            var wrapper = new FriendWrapper(_friend);
            wrapper.Emails.Clear();
            Assert.IsFalse(wrapper.IsDeveloper);
            Assert.IsTrue(wrapper.IsValid);

            wrapper.IsDeveloper = true;
            Assert.IsFalse(wrapper.IsValid);

            wrapper.Emails.Add(new FriendEmailWrapper(new FriendEmailVm { Email = "test@test.de" }));
            Assert.IsTrue(wrapper.IsValid);

            var emailsErrors = wrapper.GetErrors(nameof(wrapper.Emails)).Cast<string>().ToList();
            Assert.AreEqual(0, emailsErrors.Count);

            var isDeveloperErrors = wrapper.GetErrors(nameof(wrapper.IsDeveloper)).Cast<string>().ToList();
            Assert.AreEqual(0, isDeveloperErrors.Count);
        }

        [TestMethod]
        public void ShouldIntializeWithoutProblems()
        {
            _friend.IsDeveloper = true;
            var wrapper = new FriendWrapper(_friend);
            Assert.IsTrue(wrapper.IsValid);
        }
    }
}
