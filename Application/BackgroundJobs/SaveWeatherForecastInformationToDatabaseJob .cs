
using Application.BackgroundJobs;
using Application.Interfaces.Services.ApplicationServices;
using Quartz;

namespace Application.BackgroundJobs;
[DisallowConcurrentExecution]
public class SaveWeatherForecastInformationToDatabaseJob : IJob
{
    private readonly IMessageService _messageService;

    public SaveWeatherForecastInformationToDatabaseJob(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
      var response = await _messageService.CreateMessageAsync();
      Console.WriteLine(response);
    }
}
