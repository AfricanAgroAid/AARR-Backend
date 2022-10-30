using Application.JSONResponseModel.OpenWeatherMapServiceResponseModels;

namespace Application.Interfaces.Services.GatewayServices;

public interface IOpenWeatherMapService
{
    Task<GeocodingResponseModels> GetCityCoordinatesAsync(string cityName, int responseLimit = 1,string countryCode = "ng");
    Task<List<DaysClimaticForecastResponseModel>> GetDaysClimaticForecastAsync(List<string> farmLocations, string units ="metric", int daysCount = 7);
    Task<List<DaysClimaticForecastResponseModel>> GetMonthClimaticForecastAsync(List<string> farmLocations, string units ="metric", int daysCount = 30);
    Task<CurrentWeatherForecastResponseModel> GetCurrentWeatherForecastAsync(string cityName, string units = "metric");
}
