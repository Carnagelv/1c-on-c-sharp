namespace EP.EntityData.Context
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AssignTeam")]
    public partial class AssignTeam
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TeamId { get; set; }

        public DateTime RequestDate { get; set; }
    }
}
