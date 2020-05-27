using EP.EntityData.Context;
using EP.EntityData.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace EP.BusinessLogic.Services
{
    public interface INewsService : IService<News>
    {
        List<News> GetNewsByDiscipline(NewsDiscipline discipline);
    }

    public class NewsService : BaseService<News>, INewsService
    {
        public NewsService(IDataContext dataContext) : base(dataContext)
        {
        }

        public List<News> GetNewsByDiscipline(NewsDiscipline discipline)
        {
            return discipline == NewsDiscipline.All 
                ? Dbset.Take(Constants.DEFAULT_NEWS_COUNT).ToList() 
                : Dbset.Where(w => w.DisciplineId == discipline).Take(Constants.DEFAULT_NEWS_COUNT).ToList();
        }
    }
}
