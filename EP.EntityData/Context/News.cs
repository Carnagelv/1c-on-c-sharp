namespace EP.EntityData.Context
{
    using EP.EntityData.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("News")]
    public partial class News
    {
        public News()
        {
            NewsCommentaries = new List<NewsCommentary>();
            NewsLikes = new List<NewsLike>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string TextRu { get; set; }

        public string ImgUrl { get; set; }

        public NewsDiscipline DisciplineId { get; set; }

        public int CreateById { get; set; }
        public virtual UserProfile CreateBy { get; set; }

        public int ModifiedById { get; set; }
        public virtual UserProfile ModifiedBy { get; set; }       

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<NewsCommentary> NewsCommentaries { get; set; }
        public virtual ICollection<NewsLike> NewsLikes { get; set; }
    }
}
