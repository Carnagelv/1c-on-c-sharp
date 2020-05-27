using EP.BusinessLogic.Models;
using EP.BusinessLogic.Services;
using EP.EntityData.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EP.BusinessLogic.Managers
{
    public class MovieManager : IMovieManager
    {
        private readonly IMovieService _movieService;

        public MovieManager(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public List<MovieViewModel> GetMovies(List<int> seasons, bool firstEnter)
        {
            if (seasons == null)
                seasons = new List<int>();

            var mapper = Mappings.GetMapper();

            return mapper.Map<List<MovieViewModel>>(_movieService.GetMovieBySeason(seasons, firstEnter).OrderByDescending(o => o.Id));
        }

        public List<MovieSeason> GetMovieSeasons()
        {
            var currentSeason = DateTime.Now.Year;
            var result = new List<MovieSeason>();

            for (var i = Constants.FIRST_SEASON; i < currentSeason + 1; i++)
            {
                result.Add(new MovieSeason
                {
                    Season = i,
                    IsSelected = true
                });
            }

            return result;
        }
    }

    public interface IMovieManager
    {
        List<MovieViewModel> GetMovies(List<int> seasons, bool firstEnter);
        List<MovieSeason> GetMovieSeasons();
    }
}
