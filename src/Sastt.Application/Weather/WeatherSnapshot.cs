namespace Sastt.Application.Weather;

public record WeatherSnapshot(
    string BaseCode,
    decimal TemperatureC,
    string Conditions,
    DateTime RetrievedAt);
