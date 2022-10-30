using Application.Interfaces.Services.GatewayServices;
using Application.JSONResponseModel.OpenWeatherMapServiceResponseModels;
using Gateway.Extensions;
using Microsoft.Extensions.Configuration;

namespace Gateway.Services;

public class OpenWeatherMapService : IOpenWeatherMapService
{
    private readonly HttpClient _client;
    private readonly IConfigurationSection _configuration;
    private readonly string _apiKey;
    private readonly ICityService _cityService;

    public OpenWeatherMapService(IConfiguration configuration,ICityService cityService)
    {
        _client = new HttpClient();
        _configuration = configuration.GetSection("OpenWeatherMapService");
        _apiKey = _configuration.GetSection("ApiKey").Value;
        _cityService = cityService;
    }

    public async Task<GeocodingResponseModels> GetCityCoordinatesAsync(string cityName, int responseLimit = 1, string countryCode = "ng")
    {
       var response = await _cityService.GetAllCitiesAsync();
       countryCode = (response.Where(city => city.Cities.Contains(cityName)).SingleOrDefault()).CountryCode.ToLower();
        try
        {
            _client.BaseAddress = new Uri($"https://api.openweathermap.org/geo/1.0/direct?q={cityName},{countryCode}&limit={responseLimit}&appid={_apiKey}");
            var resp = await _client.GetAsync($"https://api.openweathermap.org/geo/1.0/direct?q={cityName},{countryCode}&limit={responseLimit}&appid={_apiKey}");
            if (!resp.IsSuccessStatusCode) throw new Exception($"The City Coordinates For The City Location:{cityName} Could Not Be Recovered");
            var responseReturned = await resp.ReadContentAs<GeocodingResponseModels>();
            return responseReturned;
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            Console.WriteLine("Function End...");
        }
    }

    public async Task<CurrentWeatherForecastResponseModel> GetCurrentWeatherForecastAsync(string cityName, string units = "metric")
    {
        try
        {
            var response = await GetCityCoordinatesAsync(cityName);
            var requestResponse = await _client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?lat={response.Latitude}&lon={response.Longitude}&appid={_apiKey}&units={units}");
            if (!requestResponse.IsSuccessStatusCode) throw new Exception($"The Current Weather Forecast For The City Location:{cityName} Could Not Be Recovered");
            var responseReturned = await requestResponse.ReadContentAs<CurrentWeatherForecastResponseModel>();
            responseReturned.Dates = new Dates
            {
                ForecastDate = CurrentWeatherForecastResponseModel.UnixTimestampToDateTime(responseReturned.Date),
                SunriseTime = CurrentWeatherForecastResponseModel.UnixTimestampToDateTime(responseReturned.TimeInformation.Sunrise),
                SunsetTime = CurrentWeatherForecastResponseModel.UnixTimestampToDateTime(responseReturned.TimeInformation.Sunset)

            };
            return responseReturned;
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            Console.WriteLine("Function End...");
        }
    }

    public async Task<List<DaysClimaticForecastResponseModel>> GetDaysClimaticForecastAsync( List<string> farmLocations, string units = "metric", int daysCount = 7)
    {
        try
        {
            var daysClimaticForecast = new List<DaysClimaticForecastResponseModel>();
            foreach (string farmLocation in farmLocations)
            {
                var response = await GetCityCoordinatesAsync(farmLocation);
                var requestResponse = await _client.GetAsync($"https://pro.openweathermap.org/data/2.5/forecast/climate?lat={response.Latitude}&lon={response.Longitude}&cnt={daysCount}&appid={_apiKey}&units={units}");
                if (!requestResponse.IsSuccessStatusCode) throw new Exception($"The Current Weather Forecast For The City Location:{farmLocation} Could Not Be Recovered");
                var responseReturned = await requestResponse.ReadContentAs<DaysClimaticForecastResponseModel>();

                //The Forecast Date Information for each daily forecast
                responseReturned.DailyForecasts.Select(df => df.Dates = new Dates
                {
                    ForecastDate = CurrentWeatherForecastResponseModel.UnixTimestampToDateTime(df.Date),
                    SunriseTime = CurrentWeatherForecastResponseModel.UnixTimestampToDateTime(df.Sunrise),
                    SunsetTime = CurrentWeatherForecastResponseModel.UnixTimestampToDateTime(df.Sunset)
                });
                daysClimaticForecast.Add(responseReturned);
            }
            return daysClimaticForecast;
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            Console.WriteLine("Function End...");
        }
    }

    public async  Task<List<DaysClimaticForecastResponseModel>> GetMonthClimaticForecastAsync( List<string> farmLocations, string units = "metric", int daysCount = 30)
    {
        try
        {
            var daysClimaticForecast = new List<DaysClimaticForecastResponseModel>();
            foreach (string farmLocation in farmLocations)
            {
                var response = await GetCityCoordinatesAsync(farmLocation);
                var requestResponse = await _client.GetAsync($"https://pro.openweathermap.org/data/2.5/forecast/climate?lat={response.Latitude}&lon={response.Longitude}&cnt={daysCount}&appid={_apiKey}&units={units}");
                if (!requestResponse.IsSuccessStatusCode) throw new Exception($"The Current Weather Forecast For The City Location:{farmLocation} Could Not Be Recovered");
                var responseReturned = await requestResponse.ReadContentAs<DaysClimaticForecastResponseModel>();

                //The Forecast Date Information for each daily forecast
                responseReturned.DailyForecasts.Select(df => df.Dates = new Dates
                {
                    ForecastDate = CurrentWeatherForecastResponseModel.UnixTimestampToDateTime(df.Date),
                    SunriseTime = CurrentWeatherForecastResponseModel.UnixTimestampToDateTime(df.Sunrise),
                    SunsetTime = CurrentWeatherForecastResponseModel.UnixTimestampToDateTime(df.Sunset)
                });
                daysClimaticForecast.Add(responseReturned);
            }
            return daysClimaticForecast;
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            Console.WriteLine("Function End...");
        }
    }
}
