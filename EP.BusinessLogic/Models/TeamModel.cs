using EP.EntityData.Helpers;
using System.Collections.Generic;

namespace EP.BusinessLogic.Models
{
    public class CreateTeamModel
    {
        public string Title { get; set; }
        public DisciplineEnum Discipline { get; set; }
        public int OwnerId { get; set; }
        public int Id { get; set; }
    }

    public class TeamsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DisciplineEnum Discipline { get; set; }
        public string LogoUrl { get; set; }
    }

    public class TeamViewModel: TeamsViewModel
    {
        public bool CanAssign { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public int CreatedById { get; set; }
        public string DisciplineName { get; set; }
    }

    public class DiciplineViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TeamsSummary
    {
        public List<TeamsViewModel> Teams = new List<TeamsViewModel>();
        public List<TeamsViewModel> UserTeams = new List<TeamsViewModel>();
    }

    public class EditTeamModel
    {
        public int Id { get; set; }
        public string Title { get; set; }  
        public DiciplineViewModel Discipline { get; set; }
    }

    public class TeamVisitingViewModel
    {
        public string Year { get; set; }
        public int TournamentCount { get; set; }
        public int VisitingCount { get; set; }
    }
}
