using Application.Interfaces.Services.GatewayServices;
using Microsoft.Extensions.DependencyInjection;
using Gateway.Services;
namespace Gateway.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddGateway(this IServiceCollection services)
    {
        services.
        AddScoped<IOpenWeatherMapService,OpenWeatherMapService>()
        .AddScoped<INumLookUpService, NumberLookUpServices>()
        .AddScoped<ICityService, CityService>();
        return services;
    }
}
