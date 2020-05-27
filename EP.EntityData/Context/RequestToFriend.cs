﻿namespace EP.EntityData.Context
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RequestToFriend")]
    public partial class RequestToFriend
    {
        public int ID { get; set; }

        public int WhoID { get; set; }
        public virtual UserProfile Who { get; set; }

        public int WithID { get; set; }
        public virtual UserProfile With { get; set; }

        public DateTime RequestDate { get; set; }
    }
}
