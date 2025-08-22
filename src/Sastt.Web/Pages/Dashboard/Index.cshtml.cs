using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Sastt.Application.Weather;
using Sastt.Application.Weather.Queries;

namespace Sastt.Web.Pages.Dashboard;

public class IndexModel : PageModel
{
    private readonly GetWeatherSnapshotQuery _query;
    private readonly string _defaultBase;
    public WeatherSnapshot? Snapshot { get; private set; }

    public IndexModel(GetWeatherSnapshotQuery query, IConfiguration config)
    {
        _query = query;
        _defaultBase = config["APP__DEFAULTBASE"] ?? "YSSY";
    }

    public async Task OnGet()
    {
        Snapshot = await _query.ExecuteAsync(_defaultBase);
    }
}
