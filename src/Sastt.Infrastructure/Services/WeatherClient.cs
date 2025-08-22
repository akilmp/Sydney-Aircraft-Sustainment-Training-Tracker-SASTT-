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
        // WeatherAPI endpoint (https://www.weatherapi.com/)
        var url = $"https://api.weatherapi.com/v1/current.json?key={_apiKey}&q={baseCode}&aqi=no";
        var result = await _httpClient.GetFromJsonAsync<WeatherApiResponse>(url, cancellationToken);

        var temp = result?.Current?.TempC ?? 0m;
        var cond = result?.Current?.Condition?.Text ?? "Unknown";

        return new WeatherSnapshot(baseCode, temp, cond, DateTime.UtcNow);
    }

    private sealed class WeatherApiResponse
    {
        public CurrentWeather? Current { get; set; }
    }

    private sealed class CurrentWeather
    {
        public decimal TempC { get; set; }
        public Condition? Condition { get; set; }
    }

    private sealed class Condition
    {
        public string? Text { get; set; }
    }
}
