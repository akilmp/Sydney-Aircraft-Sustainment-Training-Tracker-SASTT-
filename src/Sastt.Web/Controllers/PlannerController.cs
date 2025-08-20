using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sastt.Domain.Identity;

namespace Sastt.Web.Controllers
{
    [Authorize(Roles = SasttRoles.Planner)]
    [Route("planner")]
    public class PlannerController : Controller
    {
        [HttpGet("")]
        public IActionResult Index() => Content("Planner area");
    }
}
