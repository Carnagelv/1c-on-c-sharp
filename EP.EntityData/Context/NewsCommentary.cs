namespace EP.EntityData.Context
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("NewsCommentary")]
    public partial class NewsCommentary
    {
        public int Id { get; set; }

        public int NewsId { get; set; }
        public virtual News News { get; set; }

        public string Text { get; set; }        

        public int CreateById { get; set; }
        public virtual UserProfile CreateBy { get; set; }              

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
