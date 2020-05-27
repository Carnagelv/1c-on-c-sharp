using EP.EntityData.Context;
using System;
using System.Data.Entity;
using System.Linq;

namespace EP.BusinessLogic.Services
{
    public interface INewsCommentaryService : IService<NewsCommentary>
    {
        NewsCommentary AddCommentary(string text, int newsId, int userId);
        void DeleteComment(int id, int userId);
    }

    public class NewsCommentaryService : BaseService<NewsCommentary>, INewsCommentaryService
    {
        public NewsCommentaryService(IDataContext dataContext) : base(dataContext)
        {
        }

        public NewsCommentary AddCommentary(string text, int newsId, int userId)
        {
            var comment = new NewsCommentary();

            if (DataContext.News.Any(w => w.Id == newsId))
            {
                comment.NewsId = newsId;
                comment.Text = text;
                comment.CreateById = userId;
                comment.CreateDate = DateTime.Now;
                comment.ModifiedDate = DateTime.Now;

                Add(comment);
            }

            return Dbset.Include("CreateBy").FirstOrDefault(f => f.Id == comment.Id);
        }

        public void DeleteComment(int id, int userId)
        {
            var entity = Get(g => g.Id == id && g.CreateById == userId);

            if (entity != null)
                    Delete(entity);
        }
    }
}
