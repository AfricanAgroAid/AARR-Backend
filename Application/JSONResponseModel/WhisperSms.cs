using System.Text.Json.Serialization;

public class Whisper
{
    [JsonPropertyName("message")]
    public string Message { get; set; }
    [JsonPropertyName("contacts")]
    public List<string> Contacts { get; set; }
    [JsonPropertyName("priority_route")]
    public bool PriorityRoute { get; set; } = false;
    [JsonPropertyName("campaign_name")]
    public string CampaignName { get; set; } = "Sms Campaign";
    [JsonPropertyName("sender_id")]
    public string SenderId { get; set; } = "AARR";

}