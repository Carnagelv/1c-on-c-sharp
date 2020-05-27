using EP.EntityData.Context;
using System.Collections.Generic;

namespace EP.BusinessLogic.Services
{
    public class MovieService : BaseService<Movie>, IMovieService
    {
        public MovieService(IDataContext dataContext) : base(dataContext)
        {
        }

        public List<Movie> GetMovieBySeason(List<int> seasons, bool firstEnter)
        {
            return seasons.Count > 0 ? GetMany(g => seasons.Contains(g.Season)) : firstEnter ? GetAll() :  new List<Movie>();
        }
    }

    public interface IMovieService : IService<Movie>
    {
        List<Movie> GetMovieBySeason(List<int> seasons, bool firstEnter);
    }
}
