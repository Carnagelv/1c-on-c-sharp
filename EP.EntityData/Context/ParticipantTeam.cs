namespace EP.EntityData.Context
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ParticipantTeam")]
    public partial class ParticipantTeam
    {
        public int Id { get; set; }

        public int TournamentId { get; set; }
        public virtual Tournament Tournament { get; set; }

        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

        public DateTime JoinDate { get; set; }

        public bool Paid { get; set; }

        public int Point { get; set; }

        public int Place { get; set; }
    }
}
