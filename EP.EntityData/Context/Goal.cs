namespace EP.EntityData.Context
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Goal")]
    public partial class Goal
    {
        public int Id { get; set; }

        public int MatchId { get; set; }
        public virtual Match Match { get; set; }

        public int Count { get; set; }

        public int ParticipantId { get; set; }
        public virtual ParticipantPlayer ParticipantPlayer { get; set; }

        public bool IsOnPenalties { get; set; }

        public bool IsAutoGoal { get; set; }
    }
}
