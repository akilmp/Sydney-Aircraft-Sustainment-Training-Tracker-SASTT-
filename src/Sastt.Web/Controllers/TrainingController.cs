using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sastt.Domain.Identity;
using Sastt.Application;
using System.Threading.Tasks;

namespace Sastt.Web.Controllers
{
    [Authorize(Roles = SasttRoles.TrainingOfficer)]
    [Route("training")]
    public class TrainingController : AuditableController
    {
        public TrainingController(IAuditLogger auditLogger) : base(auditLogger) { }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            await AuditAsync("Accessed training officer area");
            return Content("Training officer area");
        }
    }
}
