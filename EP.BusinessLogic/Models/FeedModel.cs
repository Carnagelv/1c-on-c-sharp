using EP.EntityData.Helpers;
using System.Collections.Generic;

namespace EP.BusinessLogic.Models
{
    public class FeedModel
    {
        public List<FeedNewsModel> News { get; set; } = new List<FeedNewsModel>();
        public List<FeedLikeModel> Likes { get; set; } = new List<FeedLikeModel>();
        public List<FeedLikeModel> Comments { get; set; } = new List<FeedLikeModel>();
        public List<FeedLikeModel> Users { get; set; } = new List<FeedLikeModel>();
    }

    public class FeedNewsModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public NewsDiscipline Category { get; set; }
        public string Date { get; set; }
    }

    public class FeedLikeModel
    {
        public int Id { get; set; }
        public string Who { get; set; }
        public string Action { get; set; }        
        public string Target { get; set; }
    }
}
