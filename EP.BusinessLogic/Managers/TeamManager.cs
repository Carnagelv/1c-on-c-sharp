using EP.BusinessLogic.Models;
using EP.BusinessLogic.Services;
using EP.EntityData.Context;
using EP.EntityData.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EP.BusinessLogic.Managers
{
    public class TeamManager : ITeamManager
    {
        private readonly ITeamService _teamService;
        private readonly ITournamentService _tournamentService;

        public TeamManager(ITeamService teamService, ITournamentService tournamentService)
        {
            _teamService = teamService;
            _tournamentService = tournamentService;
        }

        public TeamsViewModel CreateTeam(Team teamData)
        {
            return !string.IsNullOrEmpty(teamData.Name)
                ? _teamService.CreateTeam(teamData)
                : new TeamsViewModel();
        }

        public Team GetTeam(int id)
        {
            return _teamService.Get(g => g.Id == id);
        }        

        public List<DiciplineViewModel> GetFreeDisciplines(int userId)
        {
            var busyDisciplines = _teamService.GetBusyDisciplinesByUserId(userId);

            var freeDisciplines = new List<DiciplineViewModel>();
            foreach (var discipline in (DisciplineEnum[])Enum.GetValues(typeof(DisciplineEnum)))
            {
                if (!busyDisciplines.Contains((int)discipline))
                {
                    freeDisciplines.Add(new DiciplineViewModel
                    {
                        Id = (int)discipline,
                        Name = discipline.Wordify()
                    });
                }
            }

            return freeDisciplines;
        }

        public TeamsSummary GetTeams(int userId)
        {
            return _teamService.GetTeams(userId);
        }

        public EditTeamModel GetTeamForEdit(int id, int userId)
        {
            return _teamService.GetUserTeam(id, userId);
        }

        public bool CheckTitle(string title, DisciplineEnum discipline, int? id)
        {
            return string.IsNullOrEmpty(title)
                    ? !string.IsNullOrEmpty(title)
                    : _teamService.CheckTitle(title, discipline, id);
        }

        public void EditTeam(int id, int userId, string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                _teamService.EditTeam(id, userId, title);
            }
        }

        public bool AssignTeam(int userId, int teamId)
        {
            if (_teamService.IsAdminTeam(teamId, userId))
            {
                _teamService.AssignTeam(userId, teamId);

                return true;
            }

            return false;
        }

        public bool CanAssignTeam(int userId, int id)
        {
            return _teamService.CanAssignTeam(userId, id);
        }

        public List<TeamVisitingViewModel> GetTeamVisiting(int id)
        {
            var tournaments = _tournamentService.GetTournamentsDates();
            var visiting = _teamService.GetTeamVisiting(id);

            var result = new List<TeamVisitingViewModel>();

            for (var year = Constants.FIRST_SEASON; year < DateTime.Now.AddYears(1).Year; year++)
            {
                result.Add(new TeamVisitingViewModel
                {
                    TournamentCount = tournaments.Count(c => c.Year == year),
                    Year = year.ToString(),
                    VisitingCount = visiting.Count(c => c.Year == year)
                });
            }

            return result;
        }
    }

    public interface ITeamManager
    {
        TeamsViewModel CreateTeam(Team teamData);
        Team GetTeam(int id);        
        List<DiciplineViewModel> GetFreeDisciplines(int userId);
        TeamsSummary GetTeams(int userId);
        EditTeamModel GetTeamForEdit(int id, int userId);
        bool CheckTitle(string title, DisciplineEnum discipline, int? id);
        void EditTeam(int id, int userId, string title);
        bool AssignTeam(int userId, int teamId);
        bool CanAssignTeam(int userId, int id);
        List<TeamVisitingViewModel> GetTeamVisiting(int id);
    }
}
