using System.Text.Json.Serialization;

namespace Application.JSONResponseModel.OpenWeatherMapServiceResponseModels;

public class DaysClimaticForecastResponseModel
{
    [JsonPropertyName("city")]
    public City City { get; set; }
    [JsonPropertyName("code")]
    public string Code { get; set; }
    [JsonPropertyName("message")]
    public decimal Message { get; set; }
    [JsonPropertyName("cnt")]
    public int Cnt { get; set; }
    [JsonPropertyName("list")]
    public List<DailyForecastInformation> DailyForecasts {get; set;} 

}

public class DailyForecastInformation
{
    [JsonPropertyName("dt")]
    public double Date { get; set; }
    [JsonPropertyName("sunrise")]
    public double Sunrise { get; set; }
    [JsonPropertyName("sunset")]
    public double Sunset { get; set; }
    [JsonPropertyName("temp")]
    public Temperature Temperature { get; set; }
    [JsonPropertyName("feels_like")]
    public HumanPerceptions HumanPerceptions { get; set; }
    [JsonPropertyName("pressure")]
    public double Pressure { get; set; }
    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }
    [JsonPropertyName("weather")]
    public List<WeatherInformation> WeatherInformations { get; set; }
    [JsonPropertyName("speed")]
    public double Speed { get; set; }
    [JsonPropertyName("deg")]
    public int Degree { get; set; }
    [JsonPropertyName("clouds")]
    public int Clouds { get; set; }

    [JsonPropertyName("rain")]
    public double Rain { get; set; }
    public Dates Dates {get; set;}
}

public class HumanPerceptions
{
    [JsonPropertyName("day")]
    public double Day { get; set; }
    [JsonPropertyName("night")]

    public double Night { get; set; }
    [JsonPropertyName("eve")]
    public double Evening { get; set; }
    [JsonPropertyName("morn")]
    public double Morning { get; set; }

}
public class Temperature
{
    [JsonPropertyName("day")]
    public double Day { get; set; }
    [JsonPropertyName("minimum")]
    public double Minimum { get; set; }
    [JsonPropertyName("maximum")]
    public double Maximum { get; set; }
    [JsonPropertyName("night")]

    public double Night { get; set; }
    [JsonPropertyName("eve")]
    public double Evening { get; set; }
    [JsonPropertyName("morn")]
    public double Morning { get; set; }

}

public class City
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("coord")]
    public Coordinates Coordinates { get; set; }
    [JsonPropertyName("timezone")]
    public double TimeZone { get; set; }
    [JsonPropertyName("country")]
    public string Country { get; set; }
    [JsonPropertyName("population")]
    public long Population { get; set; }
}
