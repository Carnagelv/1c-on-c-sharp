using AutoMapper;
using OneC.BusinessLogic.Models;
using System.Web.Mvc;

namespace OneC.Controllers
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
    }
}