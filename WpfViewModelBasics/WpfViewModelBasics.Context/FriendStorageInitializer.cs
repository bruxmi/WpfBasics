using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfViewModelBasics.Core.Entities;

namespace WpfViewModelBasics.Context
{
    public class FriendStorageInitializer : DropCreateDatabaseIfModelChanges<FriendStorageContext>
    {
        public override void InitializeDatabase(FriendStorageContext context)
        {
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction
                , string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));
            base.InitializeDatabase(context);
        }

        protected override void Seed(FriendStorageContext context)
        {
            var friends = new List<Friend>
            {
                new Friend
                {
                    FirstName = "Thomas",
                    LastName = "Huber",
                    Address = new Address
                    {
                        City = "Müllheim", Street = "Elmstreet", StreetNumber = "12345"
                    },
                    Birthday = new DateTime(1980, 10, 28),
                    IsDeveloper = true,
                    Emails = new List<FriendEmail> {new FriendEmail {Email = "thomas@thomasclaudiushuber.com"}},
                },
                new Friend
                {
                    FirstName = "Julia",
                    LastName = "Huber",
                    Address = new Address {City = "Müllheim", Street = "Elmstreet", StreetNumber = "12345"},
                    Birthday = new DateTime(1982, 10, 10),
                    Emails = new List<FriendEmail> {new FriendEmail {Email = "julia@juhu-design.com"}},
                },
                new Friend
                {
                    FirstName = "Anna",
                    LastName = "Huber",
                    Address = new Address {City = "Müllheim", Street = "Elmstreet", StreetNumber = "12345"},
                    Birthday = new DateTime(2011, 05, 13),
                    Emails = new List<FriendEmail>(),
                },
                new Friend
                {
                    FirstName = "Sara",
                    LastName = "Huber",
                    Address = new Address {City = "Müllheim", Street = "Elmstreet", StreetNumber = "12345"},
                    Birthday = new DateTime(2013, 02, 25),
                    Emails = new List<FriendEmail>(),
                },
                new Friend
                {
                    FirstName = "Andreas",
                    LastName = "Böhler",
                    Address = new Address {City = "Tiengen", Street = "Hardstreet", StreetNumber = "5"},
                    Birthday = new DateTime(1981, 01, 10),
                    IsDeveloper = true,
                    Emails = new List<FriendEmail> {new FriendEmail {Email = "andreas@strenggeheim.de"}},
                },
                new Friend
                {
                    FirstName = "Urs",
                    LastName = "Meier",
                    Address = new Address {City = "Bern", Street = "Baslerstrasse", StreetNumber = "17"},
                    Birthday = new DateTime(1970, 03, 5),
                    IsDeveloper = true,
                    Emails = new List<FriendEmail> {new FriendEmail {Email = "urs@strenggeheim.ch"}},
                },
                new Friend
                {
                    FirstName = "Chrissi",
                    LastName = "Heuberger",
                    Address = new Address {City = "Hillhome", Street = "Freiburgerstrasse", StreetNumber = "32"},
                    Birthday = new DateTime(1987, 07, 16),
                    Emails = new List<FriendEmail> {new FriendEmail {Email = "chrissi@web.de"}},
                },
                new Friend
                {
                    FirstName = "Erkan",
                    LastName = "Egin",
                    Address = new Address {City = "Neuenburg", Street = "Rheinweg", StreetNumber = "4"},
                    Birthday = new DateTime(1983, 05, 23),
                    Emails = new List<FriendEmail> {new FriendEmail {Email = "erko@web.de"}},
                },
            };
            context.Friends.AddOrUpdate(friends.ToArray());
            base.Seed(context);
        }
    }
}