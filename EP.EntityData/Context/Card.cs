namespace EP.EntityData.Context
{
    using EP.EntityData.Helpers;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Card")]
    public partial class Card
    {
        public int Id { get; set; }

        public CardEnum CardType { get; set; }

        public int MatchId { get; set; }
        public virtual Match Match { get; set; }

        public int ParticipantId { get; set; }
        public virtual ParticipantPlayer ParticipantPlayer { get; set; }
    }
}
