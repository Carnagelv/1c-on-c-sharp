namespace EP.EntityData.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class TournamentTeamCountStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParticipantTeam", "Paid", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tournament", "MaxTeamCount", c => c.Int(nullable: false));
            AddColumn("dbo.Tournament", "Place", c => c.String());
            AddColumn("dbo.Tournament", "TournamentDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tournament", "TournamentDate");
            DropColumn("dbo.Tournament", "Place");
            DropColumn("dbo.Tournament", "MaxTeamCount");
            DropColumn("dbo.ParticipantTeam", "Paid");
        }
    }
}
