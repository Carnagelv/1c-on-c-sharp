namespace EP.EntityData.Context
{
    using EP.EntityData.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Player")]
    public partial class Player
    {
        public Player()
        {
            ParticipantPlayers = new List<ParticipantPlayer>();
        }

        public int Id { get; set; }

        public int? UserId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Year { get; set; }

        public DateTime CreatedDate { get; set; }

        public DisciplineEnum MainDiscipline { get; set; }

        public string Photo { get; set; }

        public virtual ICollection<ParticipantPlayer> ParticipantPlayers { get; set; }
    }
}
