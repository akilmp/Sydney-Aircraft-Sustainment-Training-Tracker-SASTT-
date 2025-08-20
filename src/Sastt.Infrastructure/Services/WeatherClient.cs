using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Sastt.Application.Weather;

namespace Sastt.Infrastructure.Services;

public class WeatherClient : IWeatherClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public WeatherClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _apiKey = config["WEATHER__APIKEY"] ?? string.Empty;
    }

    public async Task<WeatherSnapshot> GetWeatherAsync(string baseCode, CancellationToken cancellationToken = default)
    {
        var url = $"https://api.open-meteo.com/v1/forecast?latitude=0&longitude=0&current_weather=true&apikey={_apiKey}";
        var result = await _httpClient.GetFromJsonAsync<OpenMeteoResponse>(url, cancellationToken);
        var temp = result?.CurrentWeather?.Temperature ?? 0m;
        var cond = result?.CurrentWeather?.WeatherCode?.ToString() ?? "Unknown";
        return new WeatherSnapshot(baseCode, temp, cond, DateTime.UtcNow);
    }

    private sealed class OpenMeteoResponse
    {
        public CurrentWeatherInfo? CurrentWeather { get; set; }
    }

    private sealed class CurrentWeatherInfo
    {
        public decimal Temperature { get; set; }
        public int WeatherCode { get; set; }
    }
}
