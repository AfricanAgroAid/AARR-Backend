using Application.Abstraction;
using Microsoft.Extensions.Configuration;
using Quartz;

public static class ServiceCollectionQuartzConfiguratorExtensions
{
    public static void AddJobAndTrigger<T>(
        this IServiceCollectionQuartzConfigurator quartz)
        where T : IJob
    {
        string jobName = typeof(T).Name;

        var jobKey = new JobKey(jobName);
        quartz.AddJob<T>(opts => opts.WithIdentity(jobKey));
        CalendarIntervalScheduleBuilder calendarIntervalSchedule = new ApplicationCalendarIntervalScheduleBuilder();
        quartz.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity(jobName + "-trigger")
            .WithSimpleSchedule()
            .StartNow()
            .WithCalendarIntervalSchedule(calendarIntervalSchedule.WithIntervalInDays(20)));
    }
    public static void AddJobAndTriggerForCronSchedule<T>(
    this IServiceCollectionQuartzConfigurator quartz)
    where T : IJob
    {
        string jobName = typeof(T).Name;
        var jobKey = new JobKey(jobName);
        quartz.AddJob<T>(opts => opts.WithIdentity(jobKey));

        quartz.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity(jobName + "-trigger")
            .WithCronSchedule(CronScheduleBuilder.DailyAtHourAndMinute(0,00)));
    }
}