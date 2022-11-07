using Application.Abstractions.Messaging;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.GatewayServices;
using Application.Wrapper;
using Domain.Entities;
using Mapster;

namespace Application.Implementations.Commands
{
    public class RegisterFarmerCommandHandler : ICommandHandler<RegisterFarmerCommand, FarmerResponseModel>
    {
        private readonly ICityService _cityService;
        private readonly INumLookUpService _numLookUp;
        private readonly IFarmerRepository _farmerRepository;

        public RegisterFarmerCommandHandler(ICityService cityService, INumLookUpService numLookUp, IFarmerRepository farmerRepository)
        {
            _cityService = cityService;
            _numLookUp = numLookUp;
            _farmerRepository = farmerRepository;
        }

        public async Task<Result<FarmerResponseModel>> Handle(RegisterFarmerCommand request, CancellationToken cancellationToken)
        {
            var response = await _cityService.GetAllCitiesAsync();
            var countryCode = (response.Where(city => city.Cities.Contains(request.Location)).SingleOrDefault()).CountryCode;
            var validatePhoneNumber = await _numLookUp.VerifyPhoneNumber(request.PhoneNumber,countryCode);
            if(!validatePhoneNumber.Valid) return await Result<FarmerResponseModel>.FailAsync("phoneNumber Verification Failed");
            var farmerExists = await _farmerRepository.ExistsAsync(farmer => farmer.PhoneNumber == request.PhoneNumber);
            if(farmerExists) return await Result<FarmerResponseModel>.FailAsync($"Farmer with the same phone number:{request.PhoneNumber} already exists");
            var farmer = 
            new Farmer(request.Name,request.PhoneNumber, request.Password,
            request.Language,validatePhoneNumber.CountryPrefix,countryCode,request.PhoneNumber,request.PhoneNumber);
            var farmerReturned = await _farmerRepository.CreateAsync(farmer);
            var farmerResponse = farmerReturned.Adapt<FarmerResponseModel>();
            return await Result<FarmerResponseModel>.SuccessAsync(farmerResponse,"Farmer Registration Made Successfully");
        }
    }
}