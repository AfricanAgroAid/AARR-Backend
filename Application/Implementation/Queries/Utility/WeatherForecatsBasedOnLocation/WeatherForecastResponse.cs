using Application.JSONResponseModel.OpenWeatherMapServiceResponseModels;

namespace Application.Implementation.Queries.Utility.WeatherForecatsBasedOnLocation;

public class WeatherForecastResponse
{
      public City City { get; set; }
    public string Code { get; set; }
    public decimal Message { get; set; }
    public int Cnt { get; set; }
    public List<DailyForecastInformation> DailyForecasts {get; set;}  = new List<DailyForecastInformation>();
}
