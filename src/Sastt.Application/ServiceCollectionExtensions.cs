using Microsoft.Extensions.DependencyInjection;
using Sastt.Application.Weather.Queries;

namespace Sastt.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<GetWeatherSnapshotQuery>();
        return services;
    }
}
