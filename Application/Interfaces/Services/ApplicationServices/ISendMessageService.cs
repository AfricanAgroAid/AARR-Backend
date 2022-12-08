namespace Application.Interfaces.Services.ApplicationServices;

public interface ISendMessageService
{
    Task<bool> SendHazardNotificationAsync();
}
