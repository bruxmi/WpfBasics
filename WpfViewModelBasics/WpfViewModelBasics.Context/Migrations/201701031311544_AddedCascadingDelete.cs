namespace WpfViewModelBasics.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCascadingDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FriendEmails", "FriendId", "dbo.Friends");
            AddColumn("dbo.FriendEmails", "Friend_Id", c => c.Int());
            CreateIndex("dbo.FriendEmails", "Friend_Id");
            AddForeignKey("dbo.FriendEmails", "Friend_Id", "dbo.Friends", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendEmails", "Friend_Id", "dbo.Friends");
            DropIndex("dbo.FriendEmails", new[] { "Friend_Id" });
            DropColumn("dbo.FriendEmails", "Friend_Id");
            AddForeignKey("dbo.FriendEmails", "FriendId", "dbo.Friends", "Id");
        }
    }
}
