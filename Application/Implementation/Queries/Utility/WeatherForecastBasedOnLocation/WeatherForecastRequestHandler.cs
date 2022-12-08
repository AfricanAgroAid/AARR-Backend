using Application.Abstractions.Messaging;
using Application.Interfaces.Services.GatewayServices;
using Application.Wrapper;
using Mapster;

namespace Application.Implementation.Queries.Utility.WeatherForecastBasedOnLocation;

public class WeatherForecastRequestHandler : IQueryHandler<WeatherForeCastRequestModel, List<WeatherForecastResponse>>
{
    IOpenWeatherMapService _openWeatherMapService;
    public WeatherForecastRequestHandler(IOpenWeatherMapService openWeatherMapService)
    {
        _openWeatherMapService = openWeatherMapService;
    }
    public async Task<Result<List<WeatherForecastResponse>>> Handle(WeatherForeCastRequestModel request, CancellationToken cancellationToken)
    {
        var listOfFarmLocation = new List<string>();
        listOfFarmLocation.Add(request.farmLocation);
        var weatherForeCast = await _openWeatherMapService.GetDaysClimaticForecastAsync(listOfFarmLocation, "metric", 7);
        var mappedResult = weatherForeCast.Adapt<List<WeatherForecastResponse>>();
        return await Result<List<WeatherForecastResponse>>.SuccessAsync(mappedResult, "List of weather forecast within 7 days");
    }
}
