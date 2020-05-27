namespace EP.EntityData.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class NewsWithLikesAndCommentaries : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsCommentary",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NewsId = c.Int(nullable: false),
                        Text = c.String(),
                        CreateById = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.CreateById, cascadeDelete: true)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: true)
                .Index(t => t.NewsId)
                .Index(t => t.CreateById);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Text = c.String(),
                        ImgUrl = c.String(),
                        DisciplineId = c.Int(nullable: false),
                        CreateById = c.Int(nullable: false),
                        ModifiedById = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.CreateById)
                .ForeignKey("dbo.UserProfile", t => t.ModifiedById)
                .Index(t => t.CreateById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "dbo.NewsLike",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NewsId = c.Int(nullable: false),
                        LikedById = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.LikedById, cascadeDelete: true)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: true)
                .Index(t => t.NewsId)
                .Index(t => t.LikedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsCommentary", "NewsId", "dbo.News");
            DropForeignKey("dbo.NewsLike", "NewsId", "dbo.News");
            DropForeignKey("dbo.NewsLike", "LikedById", "dbo.UserProfile");
            DropForeignKey("dbo.News", "ModifiedById", "dbo.UserProfile");
            DropForeignKey("dbo.News", "CreateById", "dbo.UserProfile");
            DropForeignKey("dbo.NewsCommentary", "CreateById", "dbo.UserProfile");
            DropIndex("dbo.NewsLike", new[] { "LikedById" });
            DropIndex("dbo.NewsLike", new[] { "NewsId" });
            DropIndex("dbo.News", new[] { "ModifiedById" });
            DropIndex("dbo.News", new[] { "CreateById" });
            DropIndex("dbo.NewsCommentary", new[] { "CreateById" });
            DropIndex("dbo.NewsCommentary", new[] { "NewsId" });
            DropTable("dbo.NewsLike");
            DropTable("dbo.News");
            DropTable("dbo.NewsCommentary");
        }
    }
}
