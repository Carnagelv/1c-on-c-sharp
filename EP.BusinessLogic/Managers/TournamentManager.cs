using EP.BusinessLogic.Models;
using EP.BusinessLogic.Services;
using EP.EntityData.Context;
using EP.EntityData.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace EP.BusinessLogic.Managers
{
    public class TournamentManager : ITournamentManager
    {
        private readonly ITournamentService _tournamentService;

        public TournamentManager(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        private static void CollectBombardiers(TournamentView tournament, List<Goal> goals)
        {
            foreach (var goal in goals.GroupBy(g => g.ParticipantId))
            {
                var item = goal.FirstOrDefault(w => !w.IsAutoGoal);

                if (item != null)
                {
                    tournament.Bombardiers.Add(new Bombardier
                    {
                        Id = item.ParticipantPlayer.PlayerId,
                        FullName = $"{item.ParticipantPlayer.Player.FirstName[0]}. {item.ParticipantPlayer.Player.LastName}",
                        TeamLogo = item.ParticipantPlayer.Team.LogoUrl,
                        Goals = goal.Where(w => !w.IsAutoGoal).Sum(s => s.Count)
                    });
                }
            }
        }

        private void FillTeamsRoster(TournamentView tournament, ICollection<ParticipantPlayer> participantPlayers)
        {
            foreach (var team in tournament.Participants)
            {
                team.Roster.AddRange(participantPlayers.Where(w => w.TeamId == team.Id).OrderByDescending(o => o.Player.LastName).Select(s => new Roster
                {
                    FullName = $"{s.Player.FirstName} {s.Player.LastName}"
                }));
            }
        }

        public void CreateScoreView(List<MatchesView> matches, List<Goal> goals)
        {
            foreach (var match in matches)
            {
                var goalsInMatch = goals.Where(w => w.MatchId == match.Id).ToList();

                var firstTeamGoals = goalsInMatch.Where(w => w.ParticipantPlayer.TeamId == match.FirstTeamId && !w.IsAutoGoal).ToList();
                firstTeamGoals.AddRange(goalsInMatch.Where(w => w.ParticipantPlayer.TeamId == match.SecondTeamId && w.IsAutoGoal).ToList());

                var secondTeamGoals = goalsInMatch.Where(w => w.ParticipantPlayer.TeamId == match.SecondTeamId && !w.IsAutoGoal).ToList();
                secondTeamGoals.AddRange(goalsInMatch.Where(w => w.ParticipantPlayer.TeamId == match.FirstTeamId && w.IsAutoGoal).ToList());

                match.FirstTeamScore = firstTeamGoals.Sum(s => s.Count);
                match.SecondTeamScore = secondTeamGoals.Sum(s => s.Count);
            }            
        }

        public TournamentsList GetTournaments()
        {
            var tournaments = _tournamentService.GetTournaments();
            var mapper = Mappings.GetMapper();

            return new TournamentsList
            {
                Active = mapper.Map<List<ActiveTournamentsView>>(tournaments.Where(w => w.IsActive).ToList()),
                InActive = mapper.Map<List<TournamentsView>>(tournaments.Where(w => !w.IsActive).ToList())
            };
        }

        public TournamentView GetTournamentById(int id, int userId)
        {
            var entity = _tournamentService.Get(g => g.Id == id);

            if (entity != null)
            {
                var mapper = Mappings.GetMapper();

                var goals = entity.Matches.SelectMany(s => s.Goals).ToList();
                var tournament = mapper.Map<TournamentView>(entity);

                if (entity.IsActive)
                {
                    var result = _tournamentService.CanTakePartIn(entity.DisciplineId, entity.ParticipantsTeams.ToList(), userId);

                    tournament.CanTakePartIn = result.IsSuccess;
                    tournament.ErrorMessage = result.ErrorMessage;
                }
                else
                {
                    tournament.CanTakePartIn = false;
                    tournament.ErrorMessage = string.Empty;
                }

                if (tournament.HasResult)
                {
                    CreateScoreView(tournament.Matches, goals);
                    CollectBombardiers(tournament, goals);
                }

                FillTeamsRoster(tournament, entity.ParticipantPlayers);

                tournament.Discipline = DisciplineHelper.GetDisciplineLatvianName(entity.DisciplineId);

                return tournament;
            }

            return new TournamentView();
        }       

        public TakePartInResult TakePartIn(int userId, int id)
        {
            return _tournamentService.TakePartIn(userId, id);
        }
    }

    public interface ITournamentManager
    {
        TournamentsList GetTournaments();
        TournamentView GetTournamentById(int id, int userId);
        TakePartInResult TakePartIn(int userId, int id);
        void CreateScoreView(List<MatchesView> matches, List<Goal> goals);
    }
}
