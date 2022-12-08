using System.Text;
using System.Text.Json;
using Application.DTOs.Farmers;
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
    private readonly IConfigurationSection _configSection2;
    private readonly string _authTokenQ;
    public static decimal Balance;
    private readonly string _authTokenA;
    private readonly string _accountSidQ;
    private readonly string _twilioNumberQ;
    private readonly string _accountSidA;
    private readonly string _twilioNumberA;
    private readonly IConfigurationSection _configSection3;
    private readonly string _whisperApiKey;
    private readonly string BaseUrl;

    public TwilioSmsIntegration(IConfiguration configuration)
    {
        _configSection = configuration.GetSection("TwilioSmsService");
        _configSection2 = configuration.GetSection("BulkTwilioSmsService");
        _configSection3 = configuration.GetSection("WhisperSms");
        _accountSid = _configSection.GetSection("AccountSid").Value;
        _twilioNumber = _configSection.GetSection("TwilioPhoneNumber").Value;
        _authToken = _configSection.GetSection("AuthToken").Value;
        _accountSidQ = _configSection2.GetSection("AccountSidQ").Value;
        _authTokenQ = _configSection2.GetSection("AuthTokenQ").Value;
        _twilioNumberQ = _configSection2.GetSection("TwilioPhoneNumberQ").Value;
        _authTokenA = _configSection2.GetSection("AuthTokenA").Value;
        _accountSidA = _configSection2.GetSection("AccountSidA").Value;
        _twilioNumberA = _configSection2.GetSection("TwilioPhoneNumberA").Value;
        _whisperApiKey = _configSection3.GetSection("ApiKey").Value;
        BaseUrl = _configSection3.GetSection("BaseUrl").Value;
    }

    public async Task<bool> SendBulkSmsAsync(List<SendMessageModel> sendMessageModels)
    {
        TwilioClient.Init(_accountSidA, _authTokenA);

        var output = false;
        try
        {
            foreach (var messageModel in sendMessageModels)
            {
                decimal balanceA;
                decimal.TryParse((await BalanceResource.FetchAsync(_accountSidA)).Balance, out balanceA);
                Console.WriteLine(balanceA);
                if (balanceA <= 0.30m)
                {
                    output = await SendWithAccountSidQ(sendMessageModels);
                    return output;
                }
                if(messageModel.PhoneNumber == "+2348169364288")
                {
                    output = await SendSms(sendMessageModels);
                    return output;
                }
                else if(messageModel.PhoneNumber == "+2349134516158")
                {
                    output = await SendWithAccountSidQ(sendMessageModels);
                    return output;
                }
                var messageResource = await MessageResource.CreateAsync(
                    body: messageModel.Message,
                    from: new Twilio.Types.PhoneNumber(_twilioNumberA),
                    to: new Twilio.Types.PhoneNumber(messageModel.PhoneNumber),
                    applicationSid: "AARR",
                    contentSid: "Hazard Notification"
                  );
                if ((messageResource.Status == MessageResource.StatusEnum.Queued)) output = true;


            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            Console.WriteLine("Code End....");
        }
        return output;
    }

    private async Task<bool> SendWithAccountSidQ(List<SendMessageModel> sendMessageModels)
    {
        TwilioClient.Init(_accountSidQ, _authTokenQ);
        var output = false;
        try
        {
            foreach (var messageModel in sendMessageModels)
            {
                decimal balanceQ;
                decimal.TryParse((await BalanceResource.FetchAsync(_accountSidQ)).Balance, out balanceQ);
                Console.WriteLine(balanceQ);
                if (balanceQ <= 0.30m)
                {
                    await SendSms(sendMessageModels);
                }
                var messageResource2 = await MessageResource.CreateAsync(
                  body: messageModel.Message,
                  from: new Twilio.Types.PhoneNumber(_twilioNumberQ),
                  to: new Twilio.Types.PhoneNumber(messageModel.PhoneNumber),
                  applicationSid: "AARR",
                  contentSid: "Hazard Notification"
              );
                if ((messageResource2.Status == MessageResource.StatusEnum.Queued)) output = true;
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            Console.WriteLine("Code End....");
        }
        return output;
    }
    private async Task<bool> SendSms(List<SendMessageModel> sendMessageModels)
    {
        var output = false;
        TwilioClient.Init(_accountSid, _authToken);

        try
        {
            foreach (var messageModel in sendMessageModels)
            {

                var messageResource = await MessageResource.CreateAsync(
              body: messageModel.Message,
              from: new Twilio.Types.PhoneNumber(_twilioNumber),
              to: new Twilio.Types.PhoneNumber(messageModel.PhoneNumber),
              applicationSid: "AARR",
              contentSid: "Hazard Notification" );
                if (!(messageResource.Status == MessageResource.StatusEnum.Queued)) return output;
                if ((messageResource.Status == MessageResource.StatusEnum.Queued)) output = true;
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            Console.WriteLine("Code End....");
        }
        return output;

    }

    public async Task<bool> SendWithWhisper(string message, string phoneNumber)
    {
        try
        {

            HttpClient client = new HttpClient();
            var url = $"{BaseUrl}api/send_message/";
            var whisper = new Whisper();
            whisper.Message = message;
            whisper.Contacts = new List<string> { phoneNumber };
            var content = new StringContent(JsonSerializer.Serialize(whisper), Encoding.UTF8, "application/json");
            var requestMessage = new HttpRequestMessage();
            requestMessage.RequestUri = new Uri(url);
            requestMessage.Method = HttpMethod.Post;
            requestMessage.Headers.Add("Authorization", _whisperApiKey);
            requestMessage.Headers.Add("HTTP_USER_AGENT", "C#");
            requestMessage.Content = content;
            var response = await client.SendAsync(requestMessage);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            Console.WriteLine(response.IsSuccessStatusCode);
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            Console.WriteLine("End Of Whisper Sms Api Call...");
        }
    }
}
