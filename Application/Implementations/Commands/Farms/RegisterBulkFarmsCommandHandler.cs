using Application.Abstractions.Messaging;
using Application.DTOs.Farms;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.ApplicationServices;
using Application.Wrapper;
using Domain.Entities;

namespace Application.Implementations.Commands.Farms
{
    public class RegisterBulkFarmsCommandHandler : ICommandHandler<RegisterBulkFarmsCommand, IList<string>>
    {
        private readonly IFarmRepository _farmRepository;
        private readonly IMessageService _messageService;
        private readonly ICityService _cityService;


        public RegisterBulkFarmsCommandHandler(ICityService cityService, IFarmRepository farmRepository, IMessageService messageService)
            => (_cityService, this._farmRepository, _messageService) = (cityService, farmRepository, messageService);

        public async Task<Result<IList<string>>> Handle(RegisterBulkFarmsCommand requests, CancellationToken cancellationToken)
        {
            int invalidLocation = 0;
            int incompleteDetails = 0;
            var messages = new List<string>();
            var validFarms = new List<CreateFarmRequestModel>();
            var successfulRegistrations = new List<string>();

            foreach (var request in requests.farmRequests)
            {
                if (request.CropTypeId == 0 && request.FarmerId == null && request.FarmName == null && request.LocatedCity == null) 
                incompleteDetails += 1;

                var response = await _cityService.GetAllCitiesAsync();
                var validCitiy = response.SingleOrDefault(city => city.Cities.Contains(request.LocatedCity));
                if (validCitiy is null) invalidLocation += 1;

                else
                {
                    validFarms.Add(request);
                }
            }
            foreach (var validRequest in validFarms)
            {
                var farm = new Farm(validRequest.FarmName, validRequest.LocatedCity, (Domain.Enums.CropType)validRequest.CropTypeId, validRequest.FarmerId);

                var farmReturned = await _farmRepository.CreateAsync(farm);
                successfulRegistrations.Add(farmReturned.Id);
            }

            var message1 = $"Registration unsuccessful for {incompleteDetails} farms because of incomplete details";
            var message2 = $"Registration unsuccessful for {invalidLocation} farms due to invalid locations provided";
            var message3 = $"{successfulRegistrations.Count} Farms were sucessfully registered.";

            messages.Add(message1);
            messages.Add(message2);
            messages.Add(message3);
            await  _messageService.CreateMessageAsync();
            return await Result<IList<string>>.SuccessAsync(successfulRegistrations, messages);
        }
    }
}
