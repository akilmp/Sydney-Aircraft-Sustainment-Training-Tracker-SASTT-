using Microsoft.EntityFrameworkCore;
using Sastt.Application.Reports;
using Sastt.Application.Weather;
using Sastt.Application.Weather.Queries;
using Sastt.Infrastructure.Persistence;
using Sastt.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<IWeatherClient, WeatherClient>();
builder.Services.AddScoped<GetWeatherSnapshotQuery>();
builder.Services.AddScoped<WorkOrderReportService>();
builder.Services.AddScoped<TrainingReportService>();
builder.Services.AddDbContext<SasttDbContext>(options =>
    options.UseInMemoryDatabase("Sastt"));

var app = builder.Build();

app.MapControllers();
app.MapRazorPages();

app.Run();
