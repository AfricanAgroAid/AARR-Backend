using Application.Interfaces.Services.GatewayServices;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Gateway.Services;

public class TwilioSmsIntegration : ITwilioSmsIntegration
{
    private readonly string _accountSid;

    private readonly string _twilioNumber;
    private readonly IConfigurationSection _configSection;
    private readonly string _authToken;
    public TwilioSmsIntegration(IConfiguration configuration)
    {
        _configSection = configuration.GetSection("TwilioSmsService");
        _accountSid = _configSection.GetSection("AccountSid").Value;
        _twilioNumber = _configSection.GetSection("TwilioPhoneNumber").Value;
        _authToken = _configSection.GetSection("AuthToken").Value;
    }
    public async Task<bool> SendSms(string message, string phoneNumber)
    {
        
        TwilioClient.Init(_accountSid, _authToken);
        var messageResource = await MessageResource.CreateAsync(
              body: "The Console App Test For The SMS",
              from: new Twilio.Types.PhoneNumber(_twilioNumber),
              to: new Twilio.Types.PhoneNumber(phoneNumber)
          );
        if (!(messageResource.Status == MessageResource.StatusEnum.Queued)) return false;
        return true;
    }
}
