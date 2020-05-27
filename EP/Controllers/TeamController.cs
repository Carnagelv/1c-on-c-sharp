using EP.BusinessLogic.Managers;
using EP.BusinessLogic.Models;
using EP.EntityData.Context;
using EP.EntityData.Helpers;
using System;
using System.Web.Mvc;

namespace EP.Controllers
{
    public class TeamController : EPController
    {
        private readonly ITeamManager _teamManager;
        private readonly IUserProfileManager _userProfileManager;
        
        public TeamController(ITeamManager teamManager, IUserProfileManager userProfileManager)
        {
            _teamManager = teamManager;
            _userProfileManager = userProfileManager;
        }

        [Authorize]
        public ActionResult Index(int? Id)
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public JsonResult Get(int Id)
        {
            var team = mapper.Map<TeamViewModel>(_teamManager.GetTeam(Id));
            team.CanAssign = _teamManager.CanAssignTeam(GetCurrentUserId(), Id);

            var visiting = _teamManager.GetTeamVisiting(Id);

            return Json(new { team, visiting }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetTeams()
        {
            var teams = _teamManager.GetTeams(GetCurrentUserId());

            return Json(new { teams = teams.Teams, userTeams = teams.UserTeams }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public JsonResult Create(CreateTeamModel teamData)
        {
            if (!Enum.IsDefined(typeof(DisciplineEnum), teamData.Discipline) || !_userProfileManager.IsActived(GetCurrentUserId()))
            {
                return Json(new { success = false });
            }

            teamData.OwnerId = GetCurrentUserId();

            return Json(new { success = true, team = _teamManager.CreateTeam(mapper.Map<Team>(teamData)) });
        }

        [Authorize]
        [HttpPost]
        public JsonResult Edit(CreateTeamModel teamData)
        {
            _teamManager.EditTeam(teamData.Id, GetCurrentUserId(), teamData.Title);

            return Json(new { edited = true });
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetTeamForEdit(int id)
        {
            return Json(new { team = _teamManager.GetTeamForEdit(id, GetCurrentUserId()) }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult CheckTitle(string title, DisciplineEnum discipline, int? id)
        {
            return Json(new { success = _teamManager.CheckTitle(title, discipline, id) }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetDisciplines()
        {
            return Json(new { disciplines = _teamManager.GetFreeDisciplines(GetCurrentUserId()) }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public JsonResult AssignTeam(int teamId)
        {
            if (!_userProfileManager.IsActived(GetCurrentUserId()))
                return Json(new { success = false });

            return Json(new { success = _teamManager.AssignTeam(GetCurrentUserId(), teamId) });
        }
    }
}