using Application.DTOs.Farmers;

namespace Application.Interfaces.Services.GatewayServices;

public interface ITwilioSmsIntegration
{
    //Task<bool> SendSms(string message, string phoneNumber);
    Task<bool> SendBulkSmsAsync(List<SendMessageModel> sendMessageModels);
    Task<bool> SendWithWhisper(string message, string phoneNumber);
}
