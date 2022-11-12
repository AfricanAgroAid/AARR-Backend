using Application.Extensions;
using Persistence.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace IOC.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddIOCService(this IServiceCollection services, IConfiguration configuration)
    {
        services.
        AddDbConfiguration(configuration)
        .AddRepositories()
        .AddApplication()
        .AddGateway();
        return services;
    }
   
}
