namespace EP.EntityData.Context
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AssignPlayer")]
    public partial class AssignPlayer
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int PlayerId { get; set; }

        public DateTime RequestDate { get; set; }
    }
}
