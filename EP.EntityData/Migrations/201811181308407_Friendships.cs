namespace EP.EntityData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Friendships : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friend",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WhoID = c.Int(nullable: false),
                        WithID = c.Int(nullable: false),
                        StartFriend = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserProfile", t => t.WhoID)
                .Index(t => t.WhoID);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 50),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        CreateDate = c.DateTime(nullable: false),
                        LastActivityDate = c.DateTime(nullable: false),
                        DateOfBirth = c.DateTime(),
                        Discipline = c.Int(),
                        Position = c.Int(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.RequestToFriend",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WhoID = c.Int(nullable: false),
                        WithID = c.Int(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserProfile", t => t.WhoID)
                .Index(t => t.WhoID);
            
            CreateTable(
                "dbo.webpages_UsersInRoles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.webpages_Roles", t => t.RoleId)
                .ForeignKey("dbo.UserProfile", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.webpages_Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friend", "WhoID", "dbo.UserProfile");
            DropForeignKey("dbo.webpages_UsersInRoles", "UserId", "dbo.UserProfile");
            DropForeignKey("dbo.webpages_UsersInRoles", "RoleId", "dbo.webpages_Roles");
            DropForeignKey("dbo.RequestToFriend", "WhoID", "dbo.UserProfile");
            DropIndex("dbo.webpages_UsersInRoles", new[] { "RoleId" });
            DropIndex("dbo.webpages_UsersInRoles", new[] { "UserId" });
            DropIndex("dbo.RequestToFriend", new[] { "WhoID" });
            DropIndex("dbo.Friend", new[] { "WhoID" });
            DropTable("dbo.webpages_Roles");
            DropTable("dbo.webpages_UsersInRoles");
            DropTable("dbo.RequestToFriend");
            DropTable("dbo.UserProfile");
            DropTable("dbo.Friend");
        }
    }
}
