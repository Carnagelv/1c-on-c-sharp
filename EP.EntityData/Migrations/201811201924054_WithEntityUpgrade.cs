namespace EP.EntityData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WithEntityUpgrade : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.RequestToFriend", "WithID");
            AddForeignKey("dbo.RequestToFriend", "WithID", "dbo.UserProfile", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestToFriend", "WithID", "dbo.UserProfile");
            DropIndex("dbo.RequestToFriend", new[] { "WithID" });
        }
    }
}
