using Application.Interfaces.Services.GatewayServices;
using Application.JSONResponseModel.NumberLookUpServices;
using Gateway.Extensions;
using Microsoft.Extensions.Configuration;

namespace Gateway.Services
{
    public class NumberLookUpServices : INumLookUpService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        public NumberLookUpServices(IConfiguration configuration)
        {
            _configuration = configuration.GetSection("NumLookUpService");
            _httpClient = new HttpClient();
            _apiKey = _configuration.GetSection("NumLookUpKey").Value;

        }
        public async Task<NumberLookUpResponseModel> VerifyPhoneNumber(string phoneNumber, string countryCode)
        {
            var link = $"https://api.numlookupapi.com/v1/validate/{phoneNumber}?apikey={_apiKey}&country_code={countryCode.ToUpper()}";
            _httpClient.BaseAddress = new Uri(link);
            var response = await _httpClient.GetAsync(link);
            if(!response.IsSuccessStatusCode) throw new Exception($"Number Verification Failed For Number:{phoneNumber}");
            var parsedResponse = await response.ReadContentAs<NumberLookUpResponseModel>();
            return (parsedResponse);
        }
    }
}