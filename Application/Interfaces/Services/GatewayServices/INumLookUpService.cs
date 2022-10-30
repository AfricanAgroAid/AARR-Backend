using Application.JSONResponseModel.NumberLookUpServices;

namespace Application.Interfaces.Services.GatewayServices
{
    public interface INumLookUpService
    {
        Task<NumberLookUpResponseModel> VerifyPhoneNumber(string phoneNumber, string countryCode);
    }
}