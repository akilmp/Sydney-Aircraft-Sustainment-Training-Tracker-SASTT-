using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sastt.Domain.Identity;

namespace Sastt.Web.Controllers
{
    [Authorize(Roles = SasttRoles.Auditor)]
    [Route("audit")]
    public class AuditController : Controller
    {
        [HttpGet("")]
        public IActionResult Index() => Content("Audit area");
    }
}
