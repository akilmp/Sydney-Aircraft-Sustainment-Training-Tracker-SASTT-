using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sastt.Domain.Identity;

namespace Sastt.Web.Controllers
{
    [Authorize(Roles = SasttRoles.Admin)]
    [Route("admin")]
    public class AdminController : Controller
    {
        [HttpGet("")]
        public IActionResult Index() => Content("Admin area");
    }
}
