namespace WpfViewModelBasics.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedRelationOfFriendAndFriendEmail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FriendEmails", "FriendId", "dbo.Friends");
            DropIndex("dbo.FriendEmails", new[] { "FriendId" });
            AlterColumn("dbo.FriendEmails", "FriendId", c => c.Int(nullable: false));
            CreateIndex("dbo.FriendEmails", "FriendId");
            AddForeignKey("dbo.FriendEmails", "FriendId", "dbo.Friends", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendEmails", "FriendId", "dbo.Friends");
            DropIndex("dbo.FriendEmails", new[] { "FriendId" });
            AlterColumn("dbo.FriendEmails", "FriendId", c => c.Int());
            CreateIndex("dbo.FriendEmails", "FriendId");
            AddForeignKey("dbo.FriendEmails", "FriendId", "dbo.Friends", "Id");
        }
    }
}
