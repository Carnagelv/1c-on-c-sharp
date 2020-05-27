namespace EP.EntityData.Context
{
    using EP.EntityData.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tournament")]
    public partial class Tournament
    {
        public Tournament()
        {
            ParticipantsTeams = new List<ParticipantTeam>();
            ParticipantPlayers = new List<ParticipantPlayer>();
            Matches = new List<Match>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string PosterUrl { get; set; }

        public DisciplineEnum DisciplineId { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public int MaxTeamCount { get; set; }

        public string Place { get; set; }

        public DateTime TournamentDate { get; set; }

        public double Fee { get; set; }

        public bool HorizontalPoster { get; set; }

        public virtual ICollection<ParticipantTeam> ParticipantsTeams { get; set; }
        public virtual ICollection<ParticipantPlayer> ParticipantPlayers { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
    }
}
