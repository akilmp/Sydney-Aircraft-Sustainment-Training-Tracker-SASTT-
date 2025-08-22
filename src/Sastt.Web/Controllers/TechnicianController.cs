using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sastt.Domain.Identity;
using Sastt.Application;
using System.Threading.Tasks;

namespace Sastt.Web.Controllers
{
    [Authorize(Roles = SasttRoles.Technician)]
    [Route("technician")]
    public class TechnicianController : AuditableController
    {
        public TechnicianController(IAuditLogger auditLogger) : base(auditLogger) { }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            await AuditAsync("Accessed technician area");
            return Content("Technician area");
        }
    }
}
