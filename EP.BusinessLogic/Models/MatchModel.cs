using System.Collections.Generic;

namespace EP.BusinessLogic.Models
{
    public class MatchesView
    {
        public int Id { get; set; }
        public string FirstTeamName { get; set; }
        public string SecondTeamName { get; set; }
        public int FirstTeamId { get; set; }
        public int SecondTeamId { get; set; }
        public string FirstTeamLogo { get; set; }
        public string SecondTeamLogo { get; set; }
        public int FirstTeamScore { get; set; }
        public int SecondTeamScore { get; set; }
        public bool IsWonFirstTeam { get; set; }
        public bool IsDraw { get; set; }
        public string Date { get; set; }
    }

    public class MatchView
    {
        public string TournamentName { get; set; }
        public string Date { get; set; }
    }

    public class FilterItems
    {
        public List<Default> Seasons { get; set; } = new List<Default>();
        public List<Default> Tournaments { get; set; } = new List<Default>();
        public List<Default> Teams { get; set; } = new List<Default>();
    }

    public class MatchFilters
    {
        public int Season { get; set; }
        public int Tournament { get; set; }
        public int Team { get; set; }
        public bool IncludeItems { get; set; }
    }

    public class MatchFilterView
    {
        public List<MatchesView> Matches { get; set; } = new List<MatchesView>();
        public FilterItems FilterItems { get; set; }
    }
}
