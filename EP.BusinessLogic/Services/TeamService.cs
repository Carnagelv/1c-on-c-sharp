using EP.BusinessLogic.Models;
using EP.EntityData.Context;
using EP.EntityData.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EP.BusinessLogic.Services
{
    public class TeamService : BaseService<Team>, ITeamService
    {
        public TeamService(IDataContext dataContext) : base(dataContext)
        {
        }

        private List<TeamsViewModel> GetTeamsView(IQueryable<Team> teams)
        {
            return teams.Select(s => new TeamsViewModel
            {
                Id = s.Id,
                Discipline = s.DisciplineId,
                LogoUrl = s.LogoUrl,
                Name = s.Name
            }).ToList();
        }

        public TeamsViewModel CreateTeam(Team teamData)
        {
            if (!Dbset.Any(a => (a.Name.ToLower() == teamData.Name.ToLower() && a.DisciplineId == teamData.DisciplineId)
                    && (a.DisciplineId == teamData.DisciplineId && a.CreateById == teamData.CreateById)))
            {
                teamData.LogoUrl = "/Content/img/no-logo-team.png";
                Add(teamData);
            }

            return new TeamsViewModel
            {
                Discipline = teamData.DisciplineId,
                Id = teamData.Id,
                LogoUrl = teamData.LogoUrl,
                Name = teamData.Name
            };
        }

        public List<int> GetBusyDisciplinesByUserId(int userId)
        {
            return Dbset.Where(w => w.CreateById == userId).Select(s => (int)s.DisciplineId).ToList();
        }

        public TeamsSummary GetTeams(int userId)
        {
            var teams = Dbset.AsQueryable();

            return new TeamsSummary
            {
                Teams = GetTeamsView(teams),
                UserTeams = GetTeamsView(teams.Where(w => w.CreateById == userId))
            };
        }

        public EditTeamModel GetUserTeam(int id, int userId)
        {
            var team = Get(f => f.Id == id && f.CreateById == userId);

            if (team != null)
            {
                return new EditTeamModel
                {
                    Id = team.Id,
                    Title = team.Name,
                    Discipline = new DiciplineViewModel
                    {
                        Id = (int)team.DisciplineId,
                        Name = team.DisciplineId.Wordify()
                    }
                };
            }

            return new EditTeamModel();
        }

        public bool CheckTitle(string title, DisciplineEnum discipline, int? id)
        {
            return id.HasValue
                ? Dbset.Any(a => a.Name.ToLower() == title.ToLower() && a.DisciplineId == discipline && a.Id != id)
                : Dbset.Any(a => a.Name.ToLower() == title.ToLower() && a.DisciplineId == discipline);
        }

        public void EditTeam(int id, int userId, string title)
        {
            var team = Get(g => g.Id == id && g.CreateById == userId);

            if (team != null)
            {
                team.Name = title;
                Update(team);
            }
        }

        public bool IsAdminTeam(int teamId, int userId)
        {
            return Dbset.Any(a => a.Id == teamId && a.CreateById == 1) && !DataContext.AssignTeams.Any(a => a.UserId == userId);
        }

        public void AssignTeam(int userId, int teamId)
        {
            DataContext.AssignTeams.Add(new AssignTeam
            {
                RequestDate = DateTime.Now,
                TeamId = teamId,
                UserId = userId
            });

            DataContext.SaveChanges();
        }

        public bool CanAssignTeam(int userId, int id)
        {
            return !DataContext.AssignTeams.Any(a => a.UserId == userId) && Dbset.Any(a => a.CreateById == 1 && a.Id == id);
        }

        public List<DateTime> GetTeamVisiting(int id)
        {
            return Dbset.SelectMany(s => s.ParticipantsTeams.Where(w => w.TeamId == id).Select(t => t.Tournament.TournamentDate)).ToList();
        }
    }

    public interface ITeamService : IService<Team>
    {
        TeamsViewModel CreateTeam(Team teamData);       
        List<int> GetBusyDisciplinesByUserId(int userId);
        TeamsSummary GetTeams(int userId);
        EditTeamModel GetUserTeam(int id, int userId);
        bool CheckTitle(string title, DisciplineEnum discipline, int? id);
        void EditTeam(int id, int userId, string title);
        bool IsAdminTeam(int teamId, int userId);
        void AssignTeam(int userId, int teamId);
        bool CanAssignTeam(int userId, int id);
        List<DateTime> GetTeamVisiting(int id);
    }
}
