using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sastt.Domain.Identity;
using Sastt.Application;
using System.Threading.Tasks;

namespace Sastt.Web.Controllers
{
    [Authorize(Roles = SasttRoles.Admin)]
    [Route("admin")]
    public class AdminController : AuditableController
    {
        public AdminController(IAuditLogger auditLogger) : base(auditLogger) { }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            await AuditAsync("Accessed admin area");
            return Content("Admin area");
        }
    }
}
