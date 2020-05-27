namespace EP.EntityData.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Match")]
    public partial class Match
    {
        public Match()
        {
            Goals = new List<Goal>();
            Cards = new List<Card>();
        }

        public int Id { get; set; }

        public int TournamentId { get; set; }
        public virtual Tournament Tournament { get; set; }

        public int FirstTeamId { get; set; }
        public virtual ParticipantTeam FirstTeam { get; set; }

        public int SecondTeamId { get; set; }
        public virtual ParticipantTeam SecondTeam { get; set; }

        public bool WonFirstTeam { get; set; }

        public bool WonSecondTeam { get; set; }

        public bool Draw { get; set; }

        public bool WonOnPenalties { get; set; }

        public string ScoreOnPenalties { get; set; }

        public DateTime MatchDate { get; set; }

        public virtual ICollection<Goal> Goals { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
    }
}
