using System.Text.Json.Serialization;

namespace Application.JSONResponseModel.OpenWeatherMapServiceResponseModels;

public class GeocodingResponseModels
{
    [JsonPropertyName("name")]
    public string CityName {get; set;}
    [JsonPropertyName("lat")]
    public decimal Latitude{get; set;}
    [JsonPropertyName("lon")]
    public decimal Longitude{get; set;}
    [JsonPropertyName("country")]
    public string CountryCode {get; set;}
    
    [JsonPropertyName("state")]
    public string State {get; set;}
}
