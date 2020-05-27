using EP.EntityData.Context;
using System;
using System.Linq;

namespace EP.BusinessLogic.Services
{
    public interface INewsLikeService : IService<NewsLike>
    {
        void ToogleLikeNews(int id, int userId);
    }

    public class NewsLikeService : BaseService<NewsLike>, INewsLikeService
    {
        public NewsLikeService(IDataContext dataContext) : base(dataContext)
        {
        }

        public void ToogleLikeNews(int id, int userId)
        {
            if (DataContext.News.Any(a => a.Id == id))
            {
                var entity = Dbset.FirstOrDefault(w => w.NewsId == id && w.LikedById == userId);

                if (entity == null)
                {
                    Add(new NewsLike
                    {
                        CreateDate = DateTime.Now,
                        LikedById = userId,
                        NewsId = id
                    });
                }
                else
                    Delete(entity);
            }
        }
    }
}
