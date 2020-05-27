namespace EP.EntityData.Context
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ActivationCode")]
    public partial class ActivationCode
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Code { get; set; }

        public DateTime SendingDate { get; set; }
    }
}
