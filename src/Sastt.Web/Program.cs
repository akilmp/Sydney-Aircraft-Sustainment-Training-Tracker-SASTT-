using CorrelationId;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sastt.Application;
using Sastt.Application.Weather;
using Sastt.Application.Weather.Queries;
using Sastt.Domain.Identity;
using Sastt.Infrastructure.Identity;
using Sastt.Infrastructure.Persistence;
using Sastt.Infrastructure.SeedData;
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
builder.Services.AddCorrelationId();

builder.Services.AddDbContext<SasttDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (args.Contains("--seed"))
{
    var baseIndex = Array.IndexOf(args, "--base");
    var baseCode = baseIndex >= 0 && args.Length > baseIndex + 1 ? args[baseIndex + 1] : null;
    if (string.IsNullOrWhiteSpace(baseCode))
    {
        Console.Error.WriteLine("Base is required. Use --base YSSY|YWLM.");
        return;
    }

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<SasttDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
    await initializer.SeedAsync();
    await DataSeeder.SeedAsync(context, userManager, baseCode);
    return;
}

app.UseCorrelationId();
app.UseSerilogRequestLogging();

app.MapRazorPages();

app.Run();

public partial class Program { }

