namespace WpfViewModelBasics.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedRelationFriendAndFriendEmails : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.FriendEmails", new[] { "FriendId" });
            AlterColumn("dbo.FriendEmails", "FriendId", c => c.Int());
            CreateIndex("dbo.FriendEmails", "FriendId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.FriendEmails", new[] { "FriendId" });
            AlterColumn("dbo.FriendEmails", "FriendId", c => c.Int(nullable: false));
            CreateIndex("dbo.FriendEmails", "FriendId");
        }
    }
}
