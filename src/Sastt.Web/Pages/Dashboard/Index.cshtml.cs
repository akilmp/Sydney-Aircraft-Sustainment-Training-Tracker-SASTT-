using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

using Sastt.Application.Weather;
using Sastt.Application.Weather.Queries;
using Sastt.Domain.Identity;


namespace Sastt.Web.Pages.Dashboard;

[Authorize(Roles = $"{SasttRoles.Admin},{SasttRoles.Planner},{SasttRoles.Technician},{SasttRoles.TrainingOfficer},{SasttRoles.Auditor},{SasttRoles.Viewer}")]
public class IndexModel : PageModel
{
    private readonly GetWeatherSnapshotQuery _query;
    private readonly string _defaultBase;

    public WeatherSnapshot? Snapshot { get; private set; }
    public int AircraftCount { get; private set; }
    public int WorkOrderCount { get; private set; }
    public int TaskCount { get; private set; }
    public int DefectCount { get; private set; }
    public int PilotCount { get; private set; }
    public int TrainingSessionCount { get; private set; }

    public IndexModel(GetWeatherSnapshotQuery query, IConfiguration config)
    {
        _query = query;
        _defaultBase = config["APP__DEFAULTBASE"] ?? "YSSY";

    }

    public async Task OnGetAsync()
    {
        Snapshot = await _query.ExecuteAsync(_defaultBase);

    }
}
