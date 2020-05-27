namespace EP.EntityData.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AssignPlayer : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Player", new[] { "UserId" });
            CreateTable(
                "dbo.AssignPlayer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Player", "MainDiscipline", c => c.Int(nullable: false));
            AddColumn("dbo.Player", "Photo", c => c.String());
            AlterColumn("dbo.Player", "UserId", c => c.Int());
            CreateIndex("dbo.Player", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Player", new[] { "UserId" });
            AlterColumn("dbo.Player", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Player", "Photo");
            DropColumn("dbo.Player", "MainDiscipline");
            DropTable("dbo.AssignPlayer");
            CreateIndex("dbo.Player", "UserId");
        }
    }
}
