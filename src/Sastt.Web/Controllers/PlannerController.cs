using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sastt.Domain.Identity;
using Sastt.Application;
using System.Threading.Tasks;

namespace Sastt.Web.Controllers
{
    [Authorize(Roles = SasttRoles.Planner)]
    [Route("planner")]
    public class PlannerController : AuditableController
    {
        public PlannerController(IAuditLogger auditLogger) : base(auditLogger) { }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            await AuditAsync("Accessed planner area");
            return Content("Planner area");
        }
    }
}
