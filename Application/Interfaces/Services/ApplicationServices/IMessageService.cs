namespace Application.Interfaces.Services.ApplicationServices;

public interface IMessageService
{
    Task<bool> CreateMessageAsync(string content, string farmerPhoneNumber, string farmLocation, DateTime dateOfIncidence);
}
