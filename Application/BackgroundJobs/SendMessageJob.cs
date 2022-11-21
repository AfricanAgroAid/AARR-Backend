using Application.Interfaces.Services.ApplicationServices;
using Quartz;

namespace Application.BackgroundJobs;
[DisallowConcurrentExecution]
public class SendMessageJob : IJob
{
   private readonly ISendMessageService _sendMessageService; 
   public SendMessageJob(ISendMessageService sendMessageService) =>
    (_sendMessageService) = (sendMessageService);
    public async Task Execute(IJobExecutionContext context)
    {
      var response = await _sendMessageService.SendHazardNotificationAsync();
      Console.WriteLine(response);
    }
}
