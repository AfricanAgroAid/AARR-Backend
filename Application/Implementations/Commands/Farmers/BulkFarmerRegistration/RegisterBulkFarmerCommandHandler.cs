using Application.Abstractions.Messaging;
using Application.DTOs.Farmers;
using Application.Implementations.Commands.Farmers.BulkFarmerRegistration;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.GatewayServices;
using Application.Wrapper;
using Domain.Entities;
using Mapster;

namespace Application.Implementations.Commands
{
    public class RegisterBulkFarmerCommandHandler : ICommandHandler<RegisterBulkFarmerCommand, List<FarmerResponseModel>>
    {
        private readonly ICityService _cityService;
        private readonly INumLookUpService _numLookUp;
        private readonly IFarmerRepository _farmerRepository;

        public RegisterBulkFarmerCommandHandler(ICityService cityService, INumLookUpService numLookUp, IFarmerRepository farmerRepository)
            => (_cityService, _numLookUp, _farmerRepository) = ( cityService, numLookUp, farmerRepository);
            
        

        public async Task<Result<List<FarmerResponseModel>>> Handle(RegisterBulkFarmerCommand request, CancellationToken cancellationToken)
        {
            int invalidPhoneNumbers = 0;
            int alreadyExist = 0;
            var messages = new List<string>();
            var validFarmers = new List<CreateFarmerRequestModel>();
            var successfulRegistrations = new List<FarmerResponseModel>();
            var response = await _cityService.GetAllCitiesAsync();
            foreach (var createFarmerRequest in request.farmerrRequests)
            {
                var countryCode = response.SingleOrDefault(city => city.Cities.Contains(createFarmerRequest.Location)).CountryCode;
                var validatePhoneNumber = await _numLookUp.VerifyPhoneNumber(createFarmerRequest.PhoneNumber,countryCode);
                if(!validatePhoneNumber.Valid) invalidPhoneNumbers += 1;

                var farmerExists = await _farmerRepository.ExistsAsync(farmer => farmer.PhoneNumber == createFarmerRequest.PhoneNumber);
                if(farmerExists) alreadyExist += 1;

                else
                {
                    validFarmers.Add(createFarmerRequest);
                }
            }
            foreach (var farmerRequest in validFarmers)
            {
                var countryCode = response.SingleOrDefault(city => city.Cities.Contains(farmerRequest.Location)).CountryCode;
                var validatePhoneNumber = await _numLookUp.VerifyPhoneNumber(farmerRequest.PhoneNumber, countryCode);
                var farmer = new Domain.Entities.Farmer(farmerRequest.Name, farmerRequest.PhoneNumber, farmerRequest.Language, validatePhoneNumber.CountryPrefix, countryCode, farmerRequest.PhoneNumber, farmerRequest.PhoneNumber);
                
                var farmerReturned = await _farmerRepository.CreateAsync(farmer);
                var farmerResponse = farmerReturned.Adapt<FarmerResponseModel>();
                successfulRegistrations.Add(farmerResponse);   
            }
            
            var message1 = $"Registration unsuccessful for the following because phone number(s) provided are invalid:{invalidPhoneNumbers}";
            var message2 = $"Registration unsuccessful for the following because farmers with the same phone number(s) were not found:{alreadyExist}";
            var message3 = $"{successfulRegistrations.Count} Farmer were sucessfully registered.";

            messages.Add(message1);
            messages.Add(message2);
            messages.Add(message3);
            return await Result<List<FarmerResponseModel>>.SuccessAsync(successfulRegistrations, messages);
        }
    }
}