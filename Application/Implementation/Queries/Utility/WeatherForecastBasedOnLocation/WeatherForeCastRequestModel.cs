using Application.Abstractions;
using Application.Wrapper;

namespace Application.Implementation.Queries.Utility.WeatherForecastBasedOnLocation;

public sealed record WeatherForeCastRequestModel(string farmLocation) : IQuery<List<WeatherForecastResponse>>;

