using AutoMapper;
using EP.BusinessLogic.Models;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace EP.Controllers
{
    public class EPController : Controller
    {
        protected static IMapper mapper;

        public EPController()
        {
            if (mapper == null)
            {
                mapper = Mappings.GetMapper();
            }
        }

        protected int GetCurrentUserId()
        {
            return WebSecurity.GetUserId(User?.Identity.Name);
        }

        protected string GetCurrentUserName()
        {
            return User?.Identity.Name ?? string.Empty;
        }
    }
}