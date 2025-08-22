using Microsoft.EntityFrameworkCore;
using Sastt.Application;
using Sastt.Application.Weather;
using Sastt.Infrastructure.Persistence;
using Sastt.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<IWeatherClient, WeatherClient>();
builder.Services.AddDbContext<SasttDbContext>(options =>
    options.UseInMemoryDatabase("SasttDb"));

var app = builder.Build();

app.UseStaticFiles();
app.MapRazorPages();


app.Run();
