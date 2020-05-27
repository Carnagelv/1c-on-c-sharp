namespace EP.EntityData.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class MatchesGoalsCards : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Card",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardType = c.Int(nullable: false),
                        MatchId = c.Int(nullable: false),
                        ParticipantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Match", t => t.MatchId, cascadeDelete: true)
                .ForeignKey("dbo.ParticipantPlayer", t => t.ParticipantId, cascadeDelete: true)
                .Index(t => t.MatchId)
                .Index(t => t.ParticipantId);
            
            CreateTable(
                "dbo.Match",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TournamentId = c.Int(nullable: false),
                        FirstTeamId = c.Int(nullable: false),
                        SecondTeamId = c.Int(nullable: false),
                        WonFirstTeam = c.Boolean(nullable: false),
                        WonSecondTeam = c.Boolean(nullable: false),
                        Draw = c.Boolean(nullable: false),
                        WonOnPenalties = c.Boolean(nullable: false),
                        ScoreOnPenalties = c.String(),
                        MatchDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParticipantTeam", t => t.FirstTeamId)
                .ForeignKey("dbo.ParticipantTeam", t => t.SecondTeamId)
                .ForeignKey("dbo.Tournament", t => t.TournamentId)
                .Index(t => t.TournamentId)
                .Index(t => t.FirstTeamId)
                .Index(t => t.SecondTeamId);
            
            CreateTable(
                "dbo.Goal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MatchId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        ParticipantId = c.Int(nullable: false),
                        IsOnPenalties = c.Boolean(nullable: false),
                        IsAutoGoal = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Match", t => t.MatchId, cascadeDelete: true)
                .ForeignKey("dbo.ParticipantPlayer", t => t.ParticipantId, cascadeDelete: true)
                .Index(t => t.MatchId)
                .Index(t => t.ParticipantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Card", "ParticipantId", "dbo.ParticipantPlayer");
            DropForeignKey("dbo.Card", "MatchId", "dbo.Match");
            DropForeignKey("dbo.Match", "TournamentId", "dbo.Tournament");
            DropForeignKey("dbo.Match", "SecondTeamId", "dbo.ParticipantTeam");
            DropForeignKey("dbo.Match", "FirstTeamId", "dbo.ParticipantTeam");
            DropForeignKey("dbo.Goal", "ParticipantId", "dbo.ParticipantPlayer");
            DropForeignKey("dbo.Goal", "MatchId", "dbo.Match");
            DropIndex("dbo.Goal", new[] { "ParticipantId" });
            DropIndex("dbo.Goal", new[] { "MatchId" });
            DropIndex("dbo.Match", new[] { "SecondTeamId" });
            DropIndex("dbo.Match", new[] { "FirstTeamId" });
            DropIndex("dbo.Match", new[] { "TournamentId" });
            DropIndex("dbo.Card", new[] { "ParticipantId" });
            DropIndex("dbo.Card", new[] { "MatchId" });
            DropTable("dbo.Goal");
            DropTable("dbo.Match");
            DropTable("dbo.Card");
        }
    }
}
