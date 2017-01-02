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
    public class ChangeTrackingCollectionTests
    {
        private List<FriendEmailWrapper> _emails;

        [TestInitialize]
        public void Initialize()
        {
            _emails = new List<FriendEmailWrapper>
            {
                new FriendEmailWrapper(new FriendEmailVm {Email = "thomas@thomas.com"}),
                new FriendEmailWrapper(new FriendEmailVm {Email = "julia@juhu-design.com"}),
            };
        }

        [TestMethod]
        public void ShouldTrackAddedItems()
        {
            var emailToAdd = new FriendEmailWrapper(new FriendEmailVm());

            var collection = new ChangeTrackingCollection<FriendEmailWrapper>(_emails);
            Assert.AreEqual(2, collection.Count);
            Assert.IsFalse(collection.IsChanged);

            collection.Add(emailToAdd);
            Assert.AreEqual(3, collection.Count);
            Assert.AreEqual(1, collection.AddedItems.Count);
            Assert.AreEqual(0, collection.RemovedItems.Count);
            Assert.AreEqual(0, collection.ModifiedItems.Count);
            Assert.AreEqual(emailToAdd, collection.AddedItems.First());
            Assert.IsTrue(collection.IsChanged);

            collection.Remove(emailToAdd);
            Assert.AreEqual(2, collection.Count);
            Assert.AreEqual(0, collection.AddedItems.Count);
            Assert.AreEqual(0, collection.RemovedItems.Count);
            Assert.AreEqual(0, collection.ModifiedItems.Count);
            Assert.IsFalse(collection.IsChanged);
        }

        [TestMethod]
        public void ShouldTrackRemovedItems()
        {
            var emailToRemove = _emails.First();
            var collection = new ChangeTrackingCollection<FriendEmailWrapper>(_emails);
            Assert.AreEqual(2, collection.Count);
            Assert.IsFalse(collection.IsChanged);

            collection.Remove(emailToRemove);
            Assert.AreEqual(1, collection.Count);
            Assert.AreEqual(0, collection.AddedItems.Count);
            Assert.AreEqual(1, collection.RemovedItems.Count);
            Assert.AreEqual(0, collection.ModifiedItems.Count);
            Assert.AreEqual(emailToRemove, collection.RemovedItems.First());
            Assert.IsTrue(collection.IsChanged);

            collection.Add(emailToRemove);
            Assert.AreEqual(2, collection.Count);
            Assert.AreEqual(0, collection.AddedItems.Count);
            Assert.AreEqual(0, collection.RemovedItems.Count);
            Assert.AreEqual(0, collection.ModifiedItems.Count);
            Assert.IsFalse(collection.IsChanged);
        }

        [TestMethod]
        public void ShouldTrackModifiedItem()
        {
            var emailToModify = _emails.First();
            var collection = new ChangeTrackingCollection<FriendEmailWrapper>(_emails);
            Assert.AreEqual(2, collection.Count);
            Assert.IsFalse(collection.IsChanged);

            emailToModify.Email = "modified@thomas.com";
            Assert.AreEqual(0, collection.AddedItems.Count);
            Assert.AreEqual(1, collection.ModifiedItems.Count);
            Assert.AreEqual(0, collection.RemovedItems.Count);
            Assert.IsTrue(collection.IsChanged);

            emailToModify.Email = "thomas@thomas.com";
            Assert.AreEqual(0, collection.AddedItems.Count);
            Assert.AreEqual(0, collection.ModifiedItems.Count);
            Assert.AreEqual(0, collection.RemovedItems.Count);
            Assert.IsFalse(collection.IsChanged);
        }

        [TestMethod]
        public void ShouldNotTrackAddedItemAsModified()
        {
            var emailToAdd = new FriendEmailWrapper(new FriendEmailVm());

            var collection = new ChangeTrackingCollection<FriendEmailWrapper>(_emails);
            collection.Add(emailToAdd);
            emailToAdd.Email = "modified@thomas.com";
            Assert.IsTrue(emailToAdd.IsChanged);
            Assert.AreEqual(3, collection.Count);
            Assert.AreEqual(1, collection.AddedItems.Count);
            Assert.AreEqual(0, collection.RemovedItems.Count);
            Assert.AreEqual(0, collection.ModifiedItems.Count);
            Assert.IsTrue(collection.IsChanged);
        }

        [TestMethod]
        public void ShouldNotTrackRemovedItemAsModified()
        {
            var emailToModifyAndRemove = _emails.First();

            var collection = new ChangeTrackingCollection<FriendEmailWrapper>(_emails);
            emailToModifyAndRemove.Email = "modified@thomas.com";
            Assert.AreEqual(2, collection.Count);
            Assert.AreEqual(0, collection.AddedItems.Count);
            Assert.AreEqual(0, collection.RemovedItems.Count);
            Assert.AreEqual(1, collection.ModifiedItems.Count);
            Assert.AreEqual(emailToModifyAndRemove, collection.ModifiedItems.First());
            Assert.IsTrue(collection.IsChanged);

            collection.Remove(emailToModifyAndRemove);
            Assert.AreEqual(1, collection.Count);
            Assert.AreEqual(0, collection.AddedItems.Count);
            Assert.AreEqual(1, collection.RemovedItems.Count);
            Assert.AreEqual(0, collection.ModifiedItems.Count);
            Assert.AreEqual(emailToModifyAndRemove, collection.RemovedItems.First());
            Assert.IsTrue(collection.IsChanged);
        }

        [TestMethod]
        public void ShouldAcceptChanges()
        {
            var emailToModify = _emails.First();
            var emailToRemove = _emails.Skip(1).First();
            var emailToAdd = new FriendEmailWrapper(new FriendEmailVm {Email = "anotherOne@thomas.com" });

            var collection = new ChangeTrackingCollection<FriendEmailWrapper>(_emails);

            collection.Add(emailToAdd);
            collection.Remove(emailToRemove);
            emailToModify.Email = "modified@thomas.com";
            Assert.AreEqual("thomas@thomas.com", emailToModify.EmailOriginalValue);

            Assert.AreEqual(2, collection.Count);
            Assert.AreEqual(1, collection.AddedItems.Count);
            Assert.AreEqual(1, collection.ModifiedItems.Count);
            Assert.AreEqual(1, collection.RemovedItems.Count);

            collection.AcceptChanges();

            Assert.AreEqual(2, collection.Count);
            Assert.IsTrue(collection.Contains(emailToModify));
            Assert.IsTrue(collection.Contains(emailToAdd));

            Assert.AreEqual(0, collection.AddedItems.Count);
            Assert.AreEqual(0, collection.ModifiedItems.Count);
            Assert.AreEqual(0, collection.RemovedItems.Count);

            Assert.IsFalse(emailToModify.IsChanged);
            Assert.AreEqual("modified@thomas.com", emailToModify.Email);
            Assert.AreEqual("modified@thomas.com", emailToModify.EmailOriginalValue);

            Assert.IsFalse(collection.IsChanged);
        }

        [TestMethod]
        public void ShouldRejectChanges()
        {
            var emailToModify = _emails.First();
            var emailToRemove = _emails.Skip(1).First();
            var emailToAdd = new FriendEmailWrapper(new FriendEmailVm {Email = "anotherOne@thomas.com" });

            var collection = new ChangeTrackingCollection<FriendEmailWrapper>(_emails);

            collection.Add(emailToAdd);
            collection.Remove(emailToRemove);
            emailToModify.Email = "modified@thomas.com";
            Assert.AreEqual("thomas@thomas.com", emailToModify.EmailOriginalValue);

            Assert.AreEqual(2, collection.Count);
            Assert.AreEqual(1, collection.AddedItems.Count);
            Assert.AreEqual(1, collection.ModifiedItems.Count);
            Assert.AreEqual(1, collection.RemovedItems.Count);

            collection.RejectChanges();

            Assert.AreEqual(2, collection.Count);
            Assert.IsTrue(collection.Contains(emailToModify));
            Assert.IsTrue(collection.Contains(emailToRemove));

            Assert.AreEqual(0, collection.AddedItems.Count);
            Assert.AreEqual(0, collection.ModifiedItems.Count);
            Assert.AreEqual(0, collection.RemovedItems.Count);

            Assert.IsFalse(emailToModify.IsChanged);
            Assert.AreEqual("thomas@thomas.com", emailToModify.Email);
            Assert.AreEqual("thomas@thomas.com", emailToModify.EmailOriginalValue);

            Assert.IsFalse(collection.IsChanged);
        }


        [TestMethod]
        public void ShouldRejectChangesWithModifiedAndRemovedItem()
        {
            var email = _emails.First();

            var collection = new ChangeTrackingCollection<FriendEmailWrapper>(_emails);

            email.Email = "modified@thomas.com";
            collection.Remove(email);
            Assert.AreEqual("thomas@thomas.com", email.EmailOriginalValue);

            Assert.AreEqual(1, collection.Count);
            Assert.AreEqual(0, collection.AddedItems.Count);
            Assert.AreEqual(0, collection.ModifiedItems.Count);
            Assert.AreEqual(1, collection.RemovedItems.Count);

            collection.RejectChanges();

            Assert.AreEqual(2, collection.Count);
            Assert.IsTrue(collection.Contains(email));

            Assert.AreEqual(0, collection.AddedItems.Count);
            Assert.AreEqual(0, collection.ModifiedItems.Count);
            Assert.AreEqual(0, collection.RemovedItems.Count);

            Assert.IsFalse(email.IsChanged);
            Assert.AreEqual("thomas@thomas.com", email.Email);
            Assert.AreEqual("thomas@thomas.com", email.EmailOriginalValue);

            Assert.IsFalse(collection.IsChanged);
        }
    }
}