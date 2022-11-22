using Application.Implementations.Utilities;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.ApplicationServices;
using Application.Interfaces.Services.GatewayServices;
using Gateway.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Implementation.Repositories;
using Persistence.Repositories;

namespace Persistence.Extensions;

public static class ServiceCollectionExtension
{
     public static IServiceCollection AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AARRConnectionString");
        services.AddDbContext<ApplicationContext>(option => 
        option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
       services.AddScoped<IFarmRepository, FarmRepository>()
        .AddScoped<IFarmerRepository, FarmerRepository>()
         .AddScoped<IMessageRepository, MessageRepository>()
        .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        return services;
    }
       public static IServiceCollection AddGateway(this IServiceCollection services)
    {
        services.
        AddScoped<IOpenWeatherMapService,OpenWeatherMapService>()
        .AddScoped<INumLookUpService, NumberLookUpServices>()
        .AddScoped<ITwilioSmsIntegration, TwilioSmsIntegration>()
         .AddScoped<IMessageService, MessageService>()
        .AddScoped<ICityService, CityService>()
         .AddScoped<ISendMessageService, SendMessageService>();
        return services;
    }
   
}

