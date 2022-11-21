using Application.DTOs.Farmers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.ApplicationServices;
using Application.Interfaces.Services.GatewayServices;

namespace Application.Implementations.Utilities;

public class SendMessageService : ISendMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly ITwilioSmsIntegration _twilioSmsIntegration;

    public SendMessageService(IMessageRepository messageRepository, ITwilioSmsIntegration twilioSmsIntegration) => 
    (_messageRepository,_twilioSmsIntegration) = (messageRepository, twilioSmsIntegration);

    public async Task<bool> SendHazardNotificationAsync()
    {
       var messages = await _messageRepository.GetByConditionAsync(mes => mes.DateOfIncidence.Date == DateTime.Now.Date);
       List<SendMessageModel> sendMessageModels = new List<SendMessageModel>();
       foreach(var message in messages)
       {
          var sendMessageModel = new SendMessageModel
          {
             Message = message.MessageContent,
             PhoneNumber = message.FarmerPhoneNumber
          };
          sendMessageModels.Add(sendMessageModel);
       } 
       var output = await _twilioSmsIntegration.SendBulkSmsAsync(sendMessageModels);
       return output;

    }
}
