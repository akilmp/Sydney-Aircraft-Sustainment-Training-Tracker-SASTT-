using Microsoft.EntityFrameworkCore;
using Sastt.Application;
using Sastt.Application.Weather;
using Sastt.Application.Weather.Queries;
using Sastt.Application;

using Sastt.Infrastructure.Services;
using Serilog;
using Serilog.Formatting.Json;
using CorrelationId;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .Enrich.FromLogContext()
        .Enrich.WithCorrelationId()
        .WriteTo.Console(new JsonFormatter());
});


var oracleConnection = builder.Configuration["ORACLE__CONNECTIONSTRING"] ?? string.Empty;
var defaultBase = builder.Configuration["APP__DEFAULTBASE"] ?? "YSSY";
var weatherApiKey = builder.Configuration["WEATHER__APIKEY"] ?? string.Empty;
var weatherUrl = $"https://api.weatherapi.com/v1/current.json?key={weatherApiKey}&q={defaultBase}&aqi=no";

builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<IWeatherClient, WeatherClient>();
builder.Services.AddScoped<GetWeatherSnapshotQuery>();
builder.Services.AddScoped<IAuditLogger, AuditLogger>();
builder.Services.AddCorrelationId();
builder.Services.AddHealthChecks()
    .AddCheck("oracle-db", async ct =>
    {
        try
        {
            await using var conn = new OracleConnection(oracleConnection);
            await conn.OpenAsync(ct);
            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(ex.Message, ex);
        }
    })
    .AddCheck("weather-api", async (sp, ct) =>
    {
        var client = sp.GetRequiredService<IHttpClientFactory>().CreateClient();
        try
        {
            var resp = await client.GetAsync(weatherUrl, ct);
            return resp.IsSuccessStatusCode
                ? HealthCheckResult.Healthy()
                : HealthCheckResult.Unhealthy($"Status code {(int)resp.StatusCode}");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(ex.Message, ex);
        }
    });

var app = builder.Build();

app.UseCorrelationId();
app.UseSerilogRequestLogging();


app.MapRazorPages();
app.MapHealthChecks("/health");

app.Run();

public partial class Program { }
