namespace EP.EntityData.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Messages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorId = c.Int(nullable: false),
                        RecipientId = c.Int(nullable: false),
                        MessageText = c.String(),
                        LastMessage = c.String(),
                        LastSenderId = c.Int(nullable: false),
                        IsNewMessage = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.AuthorId)
                .ForeignKey("dbo.UserProfile", t => t.LastSenderId)
                .ForeignKey("dbo.UserProfile", t => t.RecipientId)
                .Index(t => t.AuthorId)
                .Index(t => t.RecipientId)
                .Index(t => t.LastSenderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Message", "RecipientId", "dbo.UserProfile");
            DropForeignKey("dbo.Message", "LastSenderId", "dbo.UserProfile");
            DropForeignKey("dbo.Message", "AuthorId", "dbo.UserProfile");
            DropIndex("dbo.Message", new[] { "LastSenderId" });
            DropIndex("dbo.Message", new[] { "RecipientId" });
            DropIndex("dbo.Message", new[] { "AuthorId" });
            DropTable("dbo.Message");
        }
    }
}
