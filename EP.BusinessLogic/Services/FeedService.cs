using EP.BusinessLogic.Models;
using EP.EntityData.Context;
using EP.EntityData.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace EP.BusinessLogic.Services
{
    public class FeedService : BaseService<News>, IFeedService
    {
        public FeedService(IDataContext dataContext) : base(dataContext)
        {
        }

        public List<FeedNewsModel> GetFiveLastNews()
        {
            var news = DataContext.News.OrderByDescending(o => o.CreateDate).Take(Constants.DEFAULT_FEED_ITEMS_COUNT).AsQueryable();

            var result = new List<FeedNewsModel>();
            foreach (var item in news)
            {
                result.Add(new FeedNewsModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    Category = item.DisciplineId,
                    Date = item.CreateDate.ToShortDateString()
                });
            }

            return result;
        }

        public List<FeedLikeModel> GetFiveLastLikes(int userId)
        {
            var friendsIds = DataContext.Friends.Where(w => w.WhoID == userId).Select(s => s.WithID).ToList();
            var likes = DataContext.NewsLikes.Where(w => friendsIds.Contains(w.LikedById)).Take(Constants.DEFAULT_FEED_ITEMS_COUNT).ToList();

            var result = new List<FeedLikeModel>();
            foreach (var like in likes)
            {
                result.Add(new FeedLikeModel
                {
                    Id = like.NewsId,
                    Who = $"{like.LikeBy.FirstName} {like.LikeBy.LastName}",
                    Action = " nospieda like jaunumam ",
                    Target = $"{ like.News.Title }"
                });
            }

            return result;
        }

        public List<FeedLikeModel> GetFiveLastComments(int userId)
        {
            var friendsIds = DataContext.Friends.Where(w => w.WhoID == userId).Select(s => s.WithID).ToList();
            var comments = DataContext.NewsCommentaries.Where(w => friendsIds.Contains(w.CreateById)).Take(Constants.DEFAULT_FEED_ITEMS_COUNT).ToList();

            var result = new List<FeedLikeModel>();
            foreach (var comnt in comments)
            {
                result.Add(new FeedLikeModel
                {
                    Id = comnt.NewsId,
                    Who = $"{comnt.CreateBy.FirstName} {comnt.CreateBy.LastName}",
                    Action = " komentēja jaunumu ",
                    Target = $"{ comnt.News.Title }"
                });
            }

            return result;
        }

        public List<FeedLikeModel> GetFiveLastFriendShips(int userId)
        {
            var friendsIds = DataContext.Friends.Where(w => w.WhoID == userId).Select(s => s.WithID).ToList();
            var users = DataContext.Friends.Where(w => friendsIds.Contains(w.WithID)).Take(Constants.DEFAULT_FEED_ITEMS_COUNT).ToList();
            var otherUsers = DataContext.Friends.Where(w => friendsIds.Contains(w.WhoID)).Take(Constants.DEFAULT_FEED_ITEMS_COUNT).ToList();

            var result = new List<FeedLikeModel>();
            foreach (var user in users)
            {
                var otherUser = otherUsers.FirstOrDefault(f => f.WhoID == user.WithID);

                result.Add(new FeedLikeModel
                {
                    Id = user.Who.UserId,
                    Who = $"{otherUser.Who.FirstName} {otherUser.Who.LastName}",
                    Action = " tagad ir draugi",
                    Target = $"{ user.Who.FirstName } { user.Who.LastName }"
                });
            }

            return result;
        }
    }

    public interface IFeedService : IService<News>
    {
        List<FeedLikeModel> GetFiveLastComments(int userId);
        List<FeedLikeModel> GetFiveLastFriendShips(int userId);
        List<FeedLikeModel> GetFiveLastLikes(int userId);
        List<FeedNewsModel> GetFiveLastNews();
    }
}
