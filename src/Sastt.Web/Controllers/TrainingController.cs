using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sastt.Domain.Identity;

namespace Sastt.Web.Controllers
{
    [Authorize(Roles = SasttRoles.TrainingOfficer)]
    [Route("training")]
    public class TrainingController : Controller
    {
        [HttpGet("")]
        public IActionResult Index() => Content("Training officer area");
    }
}
