namespace EP.EntityData.Context
{
    using EP.EntityData.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Team")]
    public partial class Team
    {
        public Team()
        {
            ParticipantsTeams = new List<ParticipantTeam>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public DisciplineEnum DisciplineId { get; set; }

        public int CreateById { get; set; }
        public virtual UserProfile CreateBy { get; set; }      

        public DateTime CreateDate { get; set; }

        public virtual ICollection<ParticipantTeam> ParticipantsTeams { get; set; }
    }
}
