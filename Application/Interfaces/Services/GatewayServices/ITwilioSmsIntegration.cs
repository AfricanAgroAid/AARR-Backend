namespace Application.Interfaces.Services.GatewayServices;

public interface ITwilioSmsIntegration
{
    Task<bool> SendSms(string message, string phoneNumber);
}
