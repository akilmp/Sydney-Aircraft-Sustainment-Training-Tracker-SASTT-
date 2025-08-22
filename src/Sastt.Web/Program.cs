using Microsoft.EntityFrameworkCore;
using Sastt.Application;
using Sastt.Application.Weather;
using Sastt.Application.Weather.Queries;
using Sastt.Infrastructure.Identity;

using Sastt.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityInfrastructure(builder.Configuration);

builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<IWeatherClient, WeatherClient>();
builder.Services.AddDbContext<SasttDbContext>(options =>
    options.UseInMemoryDatabase("SasttDb"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
    await initializer.SeedAsync();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();


app.Run();
