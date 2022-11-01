using Application.Abstractions;
using Application.Wrapper;

namespace Application.Implementation.Queries.Utility.WeatherForecatsBasedOnLocation;

public sealed record WeatherForeCastRequestModel(string farmLocation) : IQuery<PaginatedResult<WeatherForecastResponse>>;

