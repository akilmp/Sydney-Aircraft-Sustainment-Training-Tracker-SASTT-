using CorrelationId;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sastt.Application;
using Sastt.Application.Weather;
using Sastt.Application.Weather.Queries;
using Sastt.Application;
using Sastt.Application.Reports;

using Sastt.Infrastructure.Services;
using Serilog;
using Serilog.Formatting.Json;

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
builder.Services.AddScoped<IWeeklyReportService, WeeklyReportService>();
builder.Services.AddCorrelationId();

builder.Services.AddDbContext<SasttDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(SasttDbContext).Assembly.FullName)));

builder.Services.AddIdentityInfrastructure(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
    await initializer.SeedAsync();
}

app.UseCorrelationId();
app.UseSerilogRequestLogging();

app.MapRazorPages();
app.MapControllers();

app.Run();

public partial class Program { }

