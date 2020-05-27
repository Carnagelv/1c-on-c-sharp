namespace EP.EntityData.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Teams : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LogoUrl = c.String(),
                        DisciplineId = c.Int(nullable: false),
                        CreateById = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.CreateById, cascadeDelete: true)
                .Index(t => t.CreateById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Team", "CreateById", "dbo.UserProfile");
            DropIndex("dbo.Team", new[] { "CreateById" });
            DropTable("dbo.Team");
        }
    }
}
