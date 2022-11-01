using Application.Abstractions.Messaging;
using Application.Interfaces.Services.GatewayServices;
using Application.Wrapper;

namespace Application.Implementation.Queries.Utility.WeatherForecatsBasedOnLocation;

public class WeatherForecastRequestHandler : IQueryHandler<WeatherForeCastRequestModel, PaginatedResult<WeatherForecastResponse>>
{
          IOpenWeatherMapService _openWeatherMapService;
          public WeatherForecastRequestHandler(IOpenWeatherMapService openWeatherMapService)
          {
                    _openWeatherMapService = openWeatherMapService;
          }
          public async Task<Result<PaginatedResult<WeatherForecastResponse>>> Handle(WeatherForeCastRequestModel request, CancellationToken cancellationToken)
          {
                    var listOfFarmLocation = new List<string>();
                    listOfFarmLocation.Add(request.farmLocation);
                   var weatherForeCast = await _openWeatherMapService.GetDaysClimaticForecastAsync(listOfFarmLocation, "metric", 5);
                    
          }
}
