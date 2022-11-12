using Application.Implementation.Queries.Utility.WeatherForecastBasedOnLocation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UtilityController : ControllerBase
{
    private readonly IMediator _mediator;
    public UtilityController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("SearchWeatherForcastByLocation")]
    public async Task<IActionResult> SearchWeatherForcastByLocation(string farmLocation)
    {
        var weather = await _mediator.Send(new WeatherForeCastRequestModel(farmLocation));
        return Ok(weather);
    }
}
