namespace EP.EntityData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotoAndActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfile", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserProfile", "Photo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfile", "Photo");
            DropColumn("dbo.UserProfile", "IsActive");
        }
    }
}
