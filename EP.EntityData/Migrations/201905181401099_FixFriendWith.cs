namespace EP.EntityData.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class FixFriendWith : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Friend", "WithID");
            AddForeignKey("dbo.Friend", "WithID", "dbo.UserProfile", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friend", "WithID", "dbo.UserProfile");
            DropIndex("dbo.Friend", new[] { "WithID" });
        }
    }
}
