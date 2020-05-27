namespace EP.EntityData.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AssignTheTeam : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignTeam",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AssignTeam");
        }
    }
}
