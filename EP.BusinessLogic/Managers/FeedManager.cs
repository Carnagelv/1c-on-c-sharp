using EP.BusinessLogic.Models;
using EP.BusinessLogic.Services;

namespace EP.BusinessLogic.Managers
{
    public class FeedManager : IFeedManager
    {
        private readonly IFeedService _feedService;

        public FeedManager(IFeedService feedService)
        {
            _feedService = feedService;
        }

        public FeedModel GetFeeds(int userId)
        {
            return new FeedModel
            {
                News = _feedService.GetFiveLastNews(),
                Likes = _feedService.GetFiveLastLikes(userId),
                Comments = _feedService.GetFiveLastComments(userId),
                Users = _feedService.GetFiveLastFriendShips(userId)
            };
        }
    }

    public interface IFeedManager
    {
        FeedModel GetFeeds(int userId);
    }
}
