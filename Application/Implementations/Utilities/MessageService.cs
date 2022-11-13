using Application.Interfaces.Repositories;
using Application.Interfaces.Services.ApplicationServices;

namespace Application.Implementations.Utilities;

public class MessageService : IMessageService
{
    private readonly IFarmRepository _farmRepo;
    private readonly IMessageRepository _messageRepo;

    public MessageService(IFarmRepository farmRepo, IMessageRepository messageRepo)
    {
        _farmRepo = farmRepo;
        _messageRepo = messageRepo;
    }

    public async Task<bool> CreateMessageAsync(string content, string farmerPhoneNumber, 
    string farmLocation, DateTime dateOfIncidence)
    {
       var weatherResponses = await _farmRepo.Hazard();

       foreach (var item in weatherResponses)
       {
        //    var messageExists = await _messageRepo.ExistsAsync(p => p.FarmerPhoneNumber == item.FarmerPhoneNumber 
        //    && p.DateOfIncidence == item.DateOfIncidence);
       }
     
    }
}
