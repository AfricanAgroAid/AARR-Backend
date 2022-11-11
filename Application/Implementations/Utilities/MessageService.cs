using Application.Interfaces.Services.ApplicationServices;

namespace Application.Implementations.Utilities;

public class MessageService : IMessageService
{
    public Task<bool> CreateMessageAsync(string content, string farmerPhoneNumber, string farmLocation, DateTime dateOfIncidence)
    {
        throw new NotImplementedException();
    }
}
