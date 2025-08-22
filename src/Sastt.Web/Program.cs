using Microsoft.EntityFrameworkCore;
using Sastt.Application;
using Sastt.Application.Weather;
using Sastt.Application.Weather.Queries;
using Sastt.Application;

using Sastt.Infrastructure.Services;
using Serilog;
using Serilog.Formatting.Json;
using CorrelationId;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .Enrich.FromLogContext()
        .Enrich.WithCorrelationId()
        .WriteTo.Console(new JsonFormatter());
});


builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<IWeatherClient, WeatherClient>();
builder.Services.AddScoped<GetWeatherSnapshotQuery>();
builder.Services.AddScoped<IAuditLogger, AuditLogger>();
builder.Services.AddCorrelationId();

var app = builder.Build();

app.UseCorrelationId();
app.UseSerilogRequestLogging();


app.MapRazorPages();

app.Run();
