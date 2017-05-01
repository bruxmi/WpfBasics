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
    public class ValidationSimpleProperty
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
        public void ShouldReturnValidationErrorIfFirstNameIsEmpty()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.IsFalse(wrapper.HasErrors);

            wrapper.FirstName = "";
            Assert.IsTrue(wrapper.HasErrors);

            var errors = wrapper.GetErrors(nameof(wrapper.FirstName)).Cast<CustomErrorResult>();
            Assert.AreEqual(1, errors.Count());
            Assert.AreEqual("Firstname is required", errors.First().ErrorMessage);

            wrapper.FirstName = "Julia";
            Assert.IsFalse(wrapper.HasErrors);
        }

        [TestMethod]
        public void ShouldRaiseErrorsChangedEventWhenFirstNameIsSetToEmpty() 
        {
            var wrapper = new FriendWrapper(_friend);
            var fired = false;
            wrapper.ErrorsChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(wrapper.FirstName))
                {
                    fired = true;
                }
            };

            wrapper.FirstName = "";
            Assert.IsTrue(fired);

            fired = false;
            wrapper.FirstName = "Julia";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldSetIsValid()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.IsTrue(wrapper.IsValid);

            wrapper.FirstName = "";
            Assert.IsFalse(wrapper.IsValid);

            wrapper.FirstName = "Julia";
            Assert.IsTrue(wrapper.IsValid);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsValid()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.IsTrue(wrapper.IsValid);

            var fired = false;
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(wrapper.IsValid))
                {
                    fired = true;
                }
            };

            wrapper.FirstName = "";
            Assert.IsTrue(fired);
            fired = false;
            wrapper.FirstName = "Julia";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldSetErrorsAndIsValidAfterInitialization()
        {
            this._friend.FirstName = "";
            var wrapper = new FriendWrapper(_friend);
            Assert.IsFalse(wrapper.IsValid);
            Assert.IsTrue(wrapper.HasErrors);

            var errors = wrapper.GetErrors(nameof(wrapper.FirstName)).Cast<CustomErrorResult>();
            Assert.AreEqual(1, errors.Count());
            Assert.AreEqual("Firstname is required", errors.First().ErrorMessage);
        }

        [TestMethod]
        public void ShouldRefreshErrorsAndIsValidWhenRejectChanges()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.IsTrue(wrapper.IsValid);
            Assert.IsFalse(wrapper.HasErrors);

            wrapper.FirstName = "";

            Assert.IsFalse(wrapper.IsValid);
            Assert.IsTrue(wrapper.HasErrors);

            wrapper.RejectChanges();

            Assert.IsTrue(wrapper.IsValid);
            Assert.IsFalse(wrapper.HasErrors);
        }
    }
}
