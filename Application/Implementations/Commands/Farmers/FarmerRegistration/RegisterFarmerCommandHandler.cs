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
        private readonly IOpenWeatherMapService _openWeatherMapService;
        private readonly ITwilioSmsIntegration _twilioSms;

        public RegisterFarmerCommandHandler(ICityService cityService, INumLookUpService numLookUp, IFarmerRepository farmerRepository,
         IOpenWeatherMapService openWeatherMapService, ITwilioSmsIntegration twilioSms)
        {
            _cityService = cityService;
            _numLookUp = numLookUp;
            _farmerRepository = farmerRepository;
            _openWeatherMapService = openWeatherMapService;
            _twilioSms = twilioSms;
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
            new Domain.Entities.Farmer(request.Name,request.PhoneNumber,
            request.Language,validatePhoneNumber.CountryPrefix,countryCode,request.PhoneNumber,request.PhoneNumber,request.Location);
            var farmerReturned = await _farmerRepository.CreateAsync(farmer);
             var currentForecast = await _openWeatherMapService.GetCurrentWeatherForecastAsync(farmer.FarmerCity);
            var smsMessage =
             $"The Current Weather Forecast of your location:{farmer.FarmerCity} is {currentForecast.WeatherInformations[0].Description} at the temperature of {currentForecast.Main.Temperature}";
            var sendSms = await _twilioSms.SendSms(smsMessage, validatePhoneNumber.CountryPrefix + farmer.PhoneNumber);
            var farmerResponse = farmerReturned.Adapt<FarmerResponseModel>();
            return await Result<FarmerResponseModel>.SuccessAsync(farmerResponse,"Farmer Registration Made Successfully");
        }
    }
}