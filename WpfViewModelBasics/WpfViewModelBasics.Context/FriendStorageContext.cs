
using WpfViewModelBasics.Core.Entities;

namespace WpfViewModelBasics.Context
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class FriendStorageContext : DbContext
    {
        // Your context has been configured to use a 'FriendStorageContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'WpfViewModelBasics.Context.FriendStorageContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'FriendStorageContext' 
        // connection string in the application configuration file.
        public FriendStorageContext()
            : base("name=FriendStorageContext")
        {
            Database.SetInitializer(new FriendStorageInitializer());
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<FriendEmail> FriendEmails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friend>()
                .HasMany(e => e.Emails)
                .WithOptional()
                .WillCascadeOnDelete();
        }
    }
}