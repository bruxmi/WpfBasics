namespace WpfViewModelBasics.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedFriendGroupId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Friends", "FriendGroupId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Friends", "FriendGroupId", c => c.Int(nullable: false));
        }
    }
}
