using EP.BusinessLogic.Models;
using EP.BusinessLogic.Services;
using System.Collections.Generic;
using System.Linq;

namespace EP.BusinessLogic.Managers
{
    public class MatchManager : IMatchManager
    {
        private readonly IMatchService _matchService;
        private readonly ITournamentManager _tournamentManager;

        public MatchManager(IMatchService matchService, ITournamentManager tournamentManager)
        {
            _matchService = matchService;
            _tournamentManager = tournamentManager;
        }

        public MatchView GetMatchById(int id)
        {
            var matchEntity = _matchService.Get(g => g.Id == id);
            var match = new MatchView();

            if (matchEntity != null)
            {
                var mapper = Mappings.GetMapper();
                match = mapper.Map<MatchView>(matchEntity);
            }

            return match;
        }

        public MatchFilterView GetList(MatchFilters filters)
        {
            var matchesEntity = _matchService.GetMatches(filters);
            var items = new FilterItems();
            var mapper = Mappings.GetMapper();

            if (filters.IncludeItems)
                items = _matchService.GetFilterItems();

            var matches = mapper.Map<List<MatchesView>>(matchesEntity);
            _tournamentManager.CreateScoreView(matches, matchesEntity.SelectMany(s => s.Goals).ToList());

            return new MatchFilterView
            {
                FilterItems = items,
                Matches = matches
            };
        }
    }

    public interface IMatchManager
    {
        MatchView GetMatchById(int id);
        MatchFilterView GetList(MatchFilters filters);
    }
}
