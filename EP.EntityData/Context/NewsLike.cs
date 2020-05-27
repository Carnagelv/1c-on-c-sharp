namespace EP.EntityData.Context
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("NewsLike")]
    public partial class NewsLike
    {
        public int Id { get; set; }

        public int NewsId { get; set; }
        public virtual News News { get; set; }      

        public int LikedById { get; set; }
        public virtual UserProfile LikeBy { get; set; }              

        public DateTime CreateDate { get; set; }
    }
}
