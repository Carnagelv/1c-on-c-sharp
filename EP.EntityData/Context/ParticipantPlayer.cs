namespace EP.EntityData.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ParticipantPlayer")]
    public partial class ParticipantPlayer
    {
        public ParticipantPlayer()
        {
            Goals = new List<Goal>();
            Cards = new List<Card>();
        }

        public int Id { get; set; }

        public int TournamentId { get; set; }
        public virtual Tournament Tournament { get; set; }

        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public DateTime JoinDate { get; set; }

        public virtual ICollection<Goal> Goals { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
    }
}
