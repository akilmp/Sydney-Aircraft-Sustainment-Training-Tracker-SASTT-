using System.Threading;
using System.Threading.Tasks;

namespace Sastt.Application.Weather;

public interface IWeatherClient
{
    Task<WeatherSnapshot> GetWeatherAsync(string baseCode, CancellationToken cancellationToken = default);
}
