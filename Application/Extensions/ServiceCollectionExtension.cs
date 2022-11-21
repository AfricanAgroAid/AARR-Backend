using System.Reflection;
using Application.BackgroundJobs;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return services
            .AddMediatR(assembly)
            .AddQuartz(q =>
               {
                   q.UseMicrosoftDependencyInjectionJobFactory();
                   q.AddJobAndTrigger<SaveWeatherForecastInformationToDatabaseJob>();
                   q.AddJobAndTriggerForCronSchedule<SendMessageJob>();
               }
            )
            .AddQuartzHostedService(
           q => q.WaitForJobsToComplete = true);
    }
}
