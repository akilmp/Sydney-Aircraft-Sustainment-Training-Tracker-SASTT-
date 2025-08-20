using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sastt.Domain.Identity;

namespace Sastt.Web.Controllers
{
    [Authorize(Roles = SasttRoles.Technician)]
    [Route("technician")]
    public class TechnicianController : Controller
    {
        [HttpGet("")]
        public IActionResult Index() => Content("Technician area");
    }
}
