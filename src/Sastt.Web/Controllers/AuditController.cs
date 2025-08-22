using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sastt.Domain.Identity;
using Sastt.Application;
using System.Threading.Tasks;

namespace Sastt.Web.Controllers
{
    [Authorize(Roles = SasttRoles.Auditor)]
    [Route("audit")]
    public class AuditController : AuditableController
    {
        public AuditController(IAuditLogger auditLogger) : base(auditLogger) { }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            await AuditAsync("Accessed audit area");
            return Content("Audit area");
        }
    }
}
