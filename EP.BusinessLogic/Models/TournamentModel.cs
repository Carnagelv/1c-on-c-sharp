using EP.EntityData.Helpers;
using System.Collections.Generic;

namespace EP.BusinessLogic.Models
{
    public class TournamentsList
    {
        public List<ActiveTournamentsView> Active { get; set; } = new List<ActiveTournamentsView>();
        public List<TournamentsView> InActive { get; set; } = new List<TournamentsView>();
    }  
    
    public class TournamentsView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public DisciplineEnum Discipline { get; set; }
    }

    public class ActiveTournamentsView: TournamentsView
    {
        public string PosterUrl { get; set; }
        public int TeamCount { get; set; }
        public int MaxTeamCount { get; set; }
        public string Fee { get; set; }
        public bool IsHorizontal { get; set; }
    }

    public class TournamentView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PosterUrl { get; set; }
        public string Discipline { get; set; }
        public string Date { get; set; }
        public string Place { get; set; }
        public int MaxTeamCount { get; set; }
        public int TeamCount { get; set; }
        public List<TournamentParticipant> Participants { get; set; } = new List<TournamentParticipant>();
        public List<MatchesView> Matches { get; set; } = new List<MatchesView>();
        public List<Bombardier> Bombardiers { get; set; } = new List<Bombardier>(); 
        public bool CanTakePartIn { get; set; }
        public string ErrorMessage { get; set; }
        public string Fee { get; set; }
        public bool HorizontalPoster { get; set; }
        public bool HasResult { get; set; }
    }

    public class TournamentParticipant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string JoinDate { get; set; }
        public bool Paid { get; set; }
        public int Point { get; set; }
        public int Place { get; set; }
        public List<Roster> Roster { get; set; } = new List<Roster>();
    }

    public class TakePartInResult
    {
        public bool Success { get; set; }
        public TournamentParticipant Participant { get; set; }
        public string Message { get; set; }
    }
}
