namespace Application.Interfaces.Services.ApplicationServices;

public interface IMessageService
{
    Task<bool> CreateMessageAsync();
}
