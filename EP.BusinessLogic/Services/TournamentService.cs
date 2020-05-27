using EP.BusinessLogic.Models;
using EP.EntityData.Context;
using EP.EntityData.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EP.BusinessLogic.Services
{
    public class TournamentService : BaseService<Tournament>, ITournamentService
    {
        public TournamentService(IDataContext dataContext) : base(dataContext)
        {
        }

        public List<Tournament> GetTournaments()
        {
            return Dbset.OrderByDescending(o => o.CreateDate).ToList();
        }

        public Tournament GetTournament(int Id)
        {
            return Dbset.FirstOrDefault(f => f.Id == Id);
        }

        public TakePartInResult TakePartIn(int userId, int id)
        {
            var tournament = Get(g => g.Id == id);
            var result = new TakePartInResult
            {
                Success = false,
                Message = "Komandu nedrīkst pievienot"
            };

            if (tournament != null)
            {
                if (tournament.IsActive && tournament.MaxTeamCount > tournament.ParticipantsTeams.Count)
                {
                    var team = DataContext.Teams.FirstOrDefault(f => f.DisciplineId == tournament.DisciplineId && f.CreateById == userId);

                    if (team != null)
                    {
                        if (!tournament.ParticipantsTeams.Any(a => a.TeamId == team.Id))
                        {
                            var participant = new ParticipantTeam
                            {
                                JoinDate = DateTime.Now,
                                TeamId = team.Id,
                                TournamentId = tournament.Id
                            };

                            DataContext.ParticipantTeams.Add(participant);
                            DataContext.SaveChanges();

                            result.Message = "Pieteikums nosūtīts";
                            result.Participant = new TournamentParticipant
                            {
                                JoinDate = participant.JoinDate.ToShortDateString(),
                                LogoUrl = participant.Team.LogoUrl,
                                Name = participant.Team.Name,
                                Id = participant.Team.Id,
                                Paid = participant.Paid
                            };
                            result.Success = true;
                        }
                        else
                            result.Message = "Jūsu komanda jau ir pieteikta!";
                    }
                    else
                        result.Message = "Jums nav komandu šajā disciplīnā!";
                }
            }

            return result;
        }

        public Result CanTakePartIn(DisciplineEnum disciplineId, List<ParticipantTeam> participantsTeams, int userId)
        {
            var userTeam = DataContext.Teams.FirstOrDefault(f => f.DisciplineId == disciplineId && f.CreateById == userId);
            var result = new Result()
            {
                IsSuccess = true,
                ErrorMessage = string.Empty
            };

            if (userTeam != null)
            {
                if (participantsTeams.Any(a => a.TeamId == userTeam.Id))
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Jūsu komanda jau ir pieteikta!";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Jums nav komandu šajā disciplīnā!";
            }

            return result;
        }

        public List<DateTime> GetTournamentsDates()
        {
            return Dbset.Select(s => s.TournamentDate).ToList();
        }
    }

    public interface ITournamentService : IService<Tournament>
    {
        List<Tournament> GetTournaments();
        Tournament GetTournament(int Id);
        TakePartInResult TakePartIn(int userId, int id);
        Result CanTakePartIn(DisciplineEnum disciplineId, List<ParticipantTeam> participantsTeams, int userId);
        List<DateTime> GetTournamentsDates();
    }
}
