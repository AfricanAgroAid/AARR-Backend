using System.Text.Json.Serialization;

public class Whisper
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = "Whisper Sms Activation test";
    [JsonPropertyName("contacts")]
    public List<string> Contacts { get; set; } = new List<string> { "2348169364288" };
    [JsonPropertyName("priority_route")]
    public bool PriorityRoute { get; set; } = false;
    [JsonPropertyName("campaign_name")]
    public string CampaignName { get; set; } = "Sms Campaign";
    [JsonPropertyName("sender_id")]
    public string SenderId { get; set; } = "AARR";

}