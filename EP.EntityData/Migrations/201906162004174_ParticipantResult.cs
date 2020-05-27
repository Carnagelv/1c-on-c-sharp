namespace EP.EntityData.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ParticipantResult : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParticipantTeam", "Point", c => c.Int(nullable: false));
            AddColumn("dbo.ParticipantTeam", "Place", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParticipantTeam", "Place");
            DropColumn("dbo.ParticipantTeam", "Point");
        }
    }
}
