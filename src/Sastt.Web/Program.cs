using Sastt.Application.Weather;
using Sastt.Application.Weather.Queries;
using Sastt.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<IWeatherClient, WeatherClient>();
builder.Services.AddScoped<GetWeatherSnapshotQuery>();

var app = builder.Build();

app.MapRazorPages();


app.Run();

public partial class Program { }
