using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sastt.Application.Weather;
using Sastt.Application.Weather.Queries;

namespace Sastt.Web.Pages.Dashboard;

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
