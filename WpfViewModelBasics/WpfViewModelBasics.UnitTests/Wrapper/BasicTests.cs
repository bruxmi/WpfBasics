using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfViewModelBasics.Core.Entities;
using WpfViewModelBasics.UI.Wrapper;
using WpfViewModelBasics.ViewModelMapping.ViewModel;

namespace WpfViewModelBasics.UnitTests.Wrapper
{
    [TestClass]
    public class BasicTests
    {
        private FriendVm _friend;

        [TestInitialize]
        public void Initialize()
        {
            _friend = new FriendVm
            {
                FirstName = "Thomas",
                Address = new AddressVm(),
                Emails = new List<FriendEmailVm>()
            };
        }

        [TestMethod]
        public void ShouldContainModelInModelProperty()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.AreEqual(_friend, wrapper.Model);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowArgumentNullExceptionIfModelIsNull()
        {
            try
            {
                var wrapper = new FriendWrapper(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("model", e.ParamName);
                throw;
            }
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentExceptionIfAddressIsNull()
        {
            try
            {
                _friend.Address = null;
                var wrapper = new FriendWrapper(_friend);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Address cannot be null", e.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentExceptionIfEmailsCollectionIsNull()
        {
            try
            {
                _friend.Emails = null;
                var wrapper = new FriendWrapper(_friend);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Emails cannot be null", e.Message);
                throw;
            }
        }

        [TestMethod]
        public void ShouldSetValueOfUnderlyingModelProperty()
        {
            var wrapper = new FriendWrapper(_friend);
            wrapper.FirstName = "Julia";
            Assert.AreEqual("Julia", wrapper.Model.FirstName);
        }

        [TestMethod]
        public void ShouldGetValueOfUnderlyingModelProperty()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.AreEqual(_friend.FirstName, wrapper.FirstName);
        }
    }
}
