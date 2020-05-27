using EP.BusinessLogic.Models;
using EP.EntityData.Context;
using EP.EntityData.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace EP.BusinessLogic.Services
{
    public class MatchService : BaseService<Match>, IMatchService
    {
        public MatchService(IDataContext dataContext) : base(dataContext)
        {

        }

        public List<Match> GetMatches(MatchFilters filters)
        {
            if (filters.Season == 0 && filters.Tournament == 0 && filters.Team == 0)
            {
                return Dbset.ToList();
            }

            var query = Dbset.AsQueryable();

            if (filters.Season !=  0)
            {
                var dates = SeasonHelper.GetSeasonDates(filters.Season);

                query = query.Where(w => w.Tournament.TournamentDate > dates[0] && w.Tournament.TournamentDate < dates[1]);
            }

            if (filters.Tournament != 0)
                query = query.Where(w => w.TournamentId == filters.Tournament);

            if (filters.Team != 0)
                query = query.Where(w => w.FirstTeam.TeamId == filters.Team || w.SecondTeam.TeamId == filters.Team);

            return query.ToList();
        }

        public FilterItems GetFilterItems()
        {
            return new FilterItems
            {
                Seasons = SeasonHelper.GetSeasons(),
                Teams = DataContext.Teams.Select(s => new Default { Id = s.Id, Name = s.Name }).ToList(),
                Tournaments = DataContext.Tournaments.Select(s => new Default { Id = s.Id, Name = s.Title }).ToList()
            };
        }
    }

    public interface IMatchService : IService<Match>
    {
        FilterItems GetFilterItems();
        List<Match> GetMatches(MatchFilters filters);
    }
}
