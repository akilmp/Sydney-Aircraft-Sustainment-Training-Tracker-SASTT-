using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Sastt.Application.Weather.Queries;

public class GetWeatherSnapshotQuery
{
    private readonly IWeatherClient _client;
    private readonly IMemoryCache _cache;
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(15);

    public GetWeatherSnapshotQuery(IWeatherClient client, IMemoryCache cache)
    {
        _client = client;
        _cache = cache;
    }

    public async Task<WeatherSnapshot> ExecuteAsync(string baseCode, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"weather:{baseCode}";
        if (_cache.TryGetValue(cacheKey, out WeatherSnapshot snapshot))
        {
            return snapshot;
        }

        snapshot = await _client.GetWeatherAsync(baseCode, cancellationToken);
        _cache.Set(cacheKey, snapshot, CacheDuration);
        return snapshot;
    }
}
