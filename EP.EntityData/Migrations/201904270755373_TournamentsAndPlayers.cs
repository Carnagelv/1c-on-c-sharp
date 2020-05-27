namespace EP.EntityData.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class TournamentsAndPlayers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParticipantTeam",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TournamentId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Team", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.Tournament", t => t.TournamentId, cascadeDelete: true)
                .Index(t => t.TournamentId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Tournament",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        PosterUrl = c.String(),
                        DisciplineId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParticipantPlayer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TournamentId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Player", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Team", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.Tournament", t => t.TournamentId, cascadeDelete: true)
                .Index(t => t.TournamentId)
                .Index(t => t.TeamId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Year = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParticipantTeam", "TournamentId", "dbo.Tournament");
            DropForeignKey("dbo.ParticipantPlayer", "TournamentId", "dbo.Tournament");
            DropForeignKey("dbo.ParticipantPlayer", "TeamId", "dbo.Team");
            DropForeignKey("dbo.ParticipantPlayer", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.Player", "UserId", "dbo.UserProfile");
            DropForeignKey("dbo.ParticipantTeam", "TeamId", "dbo.Team");
            DropIndex("dbo.Player", new[] { "UserId" });
            DropIndex("dbo.ParticipantPlayer", new[] { "PlayerId" });
            DropIndex("dbo.ParticipantPlayer", new[] { "TeamId" });
            DropIndex("dbo.ParticipantPlayer", new[] { "TournamentId" });
            DropIndex("dbo.ParticipantTeam", new[] { "TeamId" });
            DropIndex("dbo.ParticipantTeam", new[] { "TournamentId" });
            DropTable("dbo.Player");
            DropTable("dbo.ParticipantPlayer");
            DropTable("dbo.Tournament");
            DropTable("dbo.ParticipantTeam");
        }
    }
}
