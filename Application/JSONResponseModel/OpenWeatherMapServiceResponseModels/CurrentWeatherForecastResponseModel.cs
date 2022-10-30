using System.Text.Json.Serialization;

namespace Application.JSONResponseModel.OpenWeatherMapServiceResponseModels;

public class CurrentWeatherForecastResponseModel
{
    [JsonPropertyName("coord")]
    public Coordinates Coordinates { get; set; }
    [JsonPropertyName("weather")]
    public List<WeatherInformation> WeatherInformations { get; set; }
    [JsonPropertyName("base")]
    public string Base { get; set; }
    [JsonPropertyName("main")]
    public Main Main { get; set; }
    [JsonPropertyName("visibility")]
    public int Visibility { get; set; }
    [JsonPropertyName("wind")]
    public Wind Wind { get; set; }
    [JsonPropertyName("rain")]
    public Rain Rain { get; set; }
    [JsonPropertyName("dt")]
    public double Date { get; set; }
    [JsonPropertyName("clouds")]
    public Clouds Clouds { get; set; }
    [JsonPropertyName("sys")]
    public TimeInformation TimeInformation { get; set; }
    [JsonPropertyName("timezone")]
    public double TimeZone { get; set; }
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("cod")]
    public int Cod { get; set; }
    public Dates Dates {get; set;}


    public static DateTime UnixTimestampToDateTime(double unixTime)
    {
        DateTime unixStart = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
        return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);
    }

}
public class Dates
{
    public DateTime ForecastDate {get; set;}
    public DateTime SunriseTime{get; set;}
    public DateTime SunsetTime {get; set;}
}

public class Coordinates
{
    [JsonPropertyName("lon")]
    public double Longitude { get; set; }
    [JsonPropertyName("lat")]
    public double Latitude { get; set; }
}
public class Main
{
    [JsonPropertyName("temp")]
    public double Temperature { get; set; }
    [JsonPropertyName("feels_like")]
    public double FeelsLike { get; set; }
    [JsonPropertyName("temp_min")]
    public double TemperatureMinimum { get; set; }
    [JsonPropertyName("temp_max")]
    public double TemperatureMaximum { get; set; }
    [JsonPropertyName("pressure")]
    public int Pressure { get; set; }
    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }
    [JsonPropertyName("sea_level")]
    public int SeaLevel { get; set; }
    [JsonPropertyName("grnd_level")]
    public int GroundLevel { get; set; }
}

public class Wind
{
    [JsonPropertyName("speed")]
    public double Speed { get; set; }
    [JsonPropertyName("deg")]
    public int Degree { get; set; }
    [JsonPropertyName("gust")]
    public double Gust { get; set; }
}
public class Rain
{
    [JsonPropertyName("1h")]
    public double OneH { get; set; }
}
public class Clouds
{
    [JsonPropertyName("all")]
    public double All { get; set; }
}
public class TimeInformation
{
    [JsonPropertyName("country")]
    public string Country { get; set; }
    [JsonPropertyName("sunrise")]
    public double Sunrise { get; set; }
    [JsonPropertyName("sunset")]
    public double Sunset { get; set; }

}



public class WeatherInformation
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("main")]
    public string Main { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("icon")]
    public string Icon { get; set; }
}