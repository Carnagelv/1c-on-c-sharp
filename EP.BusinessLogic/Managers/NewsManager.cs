using EP.BusinessLogic.Models;
using EP.BusinessLogic.Services;
using EP.EntityData.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace EP.BusinessLogic.Managers
{
    public class NewsManager : INewsManager
    {
        private readonly INewsService _newsService;
        private readonly INewsLikeService _newsLikeService;
        private readonly INewsCommentaryService _newsCommentaryService;

        public NewsManager(INewsService newsService, INewsLikeService newsLikeService, INewsCommentaryService newsCommentaryService)
        {
            _newsService = newsService;
            _newsLikeService = newsLikeService;
            _newsCommentaryService = newsCommentaryService;
        }
      
        public List<NewsViewModel> GetNewsByDiscipline(NewsDiscipline discipline, int userId)
        {
            var news = _newsService.GetNewsByDiscipline(discipline);

            return news.OrderByDescending(o => o.CreateDate).Select(s => new NewsViewModel
            {
                Id = s.Id,
                DisciplineId = (int)s.DisciplineId,
                CreateDate = s.CreateDate.ToShortDateString(),
                ImgUrl = s.ImgUrl,
                CommentCount = s.NewsCommentaries.Count,
                LikeCount = s.NewsLikes.Count,
                IsYouLiked = s.NewsLikes.Any(a => a.LikedById == userId),
                Title = s.Title
            }).ToList();
        }

        public NewsItemViewModel GetNewsById(int id, int userId)
        {
            var entity = _newsService.Get(g => g.Id == id);
            var news = new NewsItemViewModel();

            if (entity != null)
            {
                var mapper = Mappings.GetMapper();

                news = mapper.Map<NewsItemViewModel>(entity);
                news.IsYouLiked = entity.NewsLikes.Any(a => a.LikedById == userId);
            }

            return news;
        }

        public CommentViewModel AddComment(string text, int newsId, int userId)
        {
            var mapper = Mappings.GetMapper();

            return mapper.Map<CommentViewModel>(_newsCommentaryService.AddCommentary(text, newsId, userId));
        }
             
        public void ToogleLikeNews(int id, int userId)
        {
            _newsLikeService.ToogleLikeNews(id, userId);
        }

        public void DeleteComment(int id, int userId)
        {
            _newsCommentaryService.DeleteComment(id, userId);
        }
    }

    public interface INewsManager
    {
        List<NewsViewModel> GetNewsByDiscipline(NewsDiscipline discipline, int userId);
        NewsItemViewModel GetNewsById(int id, int userId);
        CommentViewModel AddComment(string text, int newsId, int userId);
        void ToogleLikeNews(int id, int userId);        
        void DeleteComment(int id, int userId);
    }
}
