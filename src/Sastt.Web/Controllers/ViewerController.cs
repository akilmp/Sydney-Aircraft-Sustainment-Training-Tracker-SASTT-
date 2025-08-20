using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sastt.Domain.Identity;

namespace Sastt.Web.Controllers
{
    [Authorize(Roles = SasttRoles.Viewer)]
    [Route("viewer")]
    public class ViewerController : Controller
    {
        [HttpGet("")]
        public IActionResult Index() => Content("Viewer area");
    }
}
