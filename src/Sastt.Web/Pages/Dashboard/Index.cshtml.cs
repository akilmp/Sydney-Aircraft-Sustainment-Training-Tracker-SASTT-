using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sastt.Application.Weather;
using Sastt.Application.Weather.Queries;
using Sastt.Domain.Identity;

namespace Sastt.Web.Pages.Dashboard;

[Authorize(Roles = $"{SasttRoles.Admin},{SasttRoles.Planner},{SasttRoles.Technician},{SasttRoles.TrainingOfficer},{SasttRoles.Auditor},{SasttRoles.Viewer}")]
public class IndexModel : PageModel
{
    private readonly GetWeatherSnapshotQuery _query;
    public WeatherSnapshot? Snapshot { get; private set; }

    public IndexModel(GetWeatherSnapshotQuery query)
    {
        _query = query;
    }

    public async Task OnGet()
    {
        Snapshot = await _query.ExecuteAsync("YSSY");
    }
}
