namespace WpfViewModelBasics.Context
{
    using System.Data.Entity;
    using Core.Entities;

    public class FriendStorageContext : DbContext
    {
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
                .WithOptional(email => email.Friend)
                .WillCascadeOnDelete();
        }
    }
}