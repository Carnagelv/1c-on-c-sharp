using System.Collections.Generic;

namespace EP.BusinessLogic.Models
{
    public class NewsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int DisciplineId { get; set; }

        public string ImgUrl { get; set; }       

        public string CreateDate { get; set; }

        public int CommentCount { get; set; }

        public int LikeCount { get; set; }

        public bool IsYouLiked { get; set; }
    }

    public class NewsItemViewModel: NewsViewModel
    {
        public string Text { get; set; }

        public string TextRu { get; set; }

        public List<CommentViewModel> Commentaries = new List<CommentViewModel>();
    }

    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string CreateDate { get; set; }

        public string UserPhoto { get; set; }

        public int CreateById { get; set; }

        public string AuthorName { get; set; }
    }
}
