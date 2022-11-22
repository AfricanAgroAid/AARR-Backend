using Application.Interfaces.Repositories;
using Application.Interfaces.Services.ApplicationServices;
using Domain.Entities;

namespace Application.Implementations.Utilities;

public class MessageService : IMessageService
{
    private readonly IFarmRepository _farmRepository;
    private readonly IMessageRepository _messageRepository;

    public MessageService(IFarmRepository farmRepository, IMessageRepository messageRepository)
    {
        _farmRepository = farmRepository;
        _messageRepository = messageRepository;
    }

    public async Task<bool> CreateMessageAsync()
    {
        var hazards = await _farmRepository.Hazard();
        var output = false;
        foreach (var hazard in hazards)
        {
            hazard.FarmerPhoneNumber = hazard.FarmerCountryCode +  hazard.FarmerPhoneNumber.Replace(hazard.FarmerPhoneNumber[0].ToString(),"");
            var messageExists = await _messageRepository.ExistsAsync(message => message.FarmerPhoneNumber == hazard.FarmerPhoneNumber
            && message.DateOfIncidence.Date == hazard.DateOfIncidence.Date && message.FarmLocation == hazard.FarmLocation);
            if (messageExists) return output; 
            var messageContent = @$"Dear Farmer:{hazard.FarmerPhoneNumber}, On  ""{hazard.DateOfIncidence.ToShortDateString()}""  
            There Is Going To Be A Weather Hazard of ""{hazard.Description}"" In Your Farm At:{hazard.FarmLocation} Which Is Likely to affect your crops ";
            var message = new Message(messageContent, hazard.DateOfIncidence, hazard.FarmLocation, hazard.FarmerPhoneNumber);
            var createdMessage = await _messageRepository.CreateAsync(message);
            output = true;
        }
        return output;
    }
}
