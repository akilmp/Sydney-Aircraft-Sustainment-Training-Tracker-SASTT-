using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sastt.Application.Weather;
using Sastt.Application.Weather.Queries;
using Sastt.Domain.Identity;


namespace Sastt.Web.Pages.Dashboard;

[Authorize(Roles = $"{SasttRoles.Admin},{SasttRoles.Planner},{SasttRoles.Technician},{SasttRoles.TrainingOfficer},{SasttRoles.Auditor},{SasttRoles.Viewer}")]
public class IndexModel : PageModel
{
    private readonly GetWeatherSnapshotQuery _query;
    private readonly SasttDbContext _context;
    public WeatherSnapshot? Snapshot { get; private set; }
    public int AircraftCount { get; private set; }
    public int WorkOrderCount { get; private set; }
    public int TaskCount { get; private set; }
    public int DefectCount { get; private set; }
    public int PilotCount { get; private set; }
    public int TrainingSessionCount { get; private set; }

    public IndexModel(GetWeatherSnapshotQuery query, SasttDbContext context)
    {
        _query = query;
        _context = context;
    }

    public async Task OnGetAsync()
    {
        Snapshot = await _query.ExecuteAsync("YSSY");
        AircraftCount = await _context.Aircraft.CountAsync();
        WorkOrderCount = await _context.WorkOrders.CountAsync();
        TaskCount = await _context.WorkOrderTasks.CountAsync();
        DefectCount = await _context.Defects.CountAsync();
        PilotCount = await _context.Pilots.CountAsync();
        TrainingSessionCount = await _context.TrainingSessions.CountAsync();
    }
}
