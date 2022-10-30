using Application.Interfaces.Services.GatewayServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Extensions;
namespace IOC.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddIOCService(this IServiceCollection services, IConfiguration configuration)
    {
        services.
        AddDbConfiguration(configuration)
        .AddRepositories()
        .AddGateway();
        return services;
    }
   
}
