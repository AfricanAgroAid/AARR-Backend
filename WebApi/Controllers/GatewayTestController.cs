using Application.Interfaces.Services.GatewayServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class GatewaysTestController : ControllerBase
{
    private readonly ICityService _cityService;
    private readonly IOpenWeatherMapService _openWeatherMap;
    private readonly INumLookUpService _numLookUp;

    public GatewaysTestController(ICityService cityService, IOpenWeatherMapService openWeatherMap, INumLookUpService numLookUp)
    {
        _cityService = cityService;
        _openWeatherMap = openWeatherMap;
        _numLookUp = numLookUp;
    }

    [HttpGet("GetCityCoordinatesAsync")]
    public async Task<IActionResult> GetCityCoordinatesAsync(string cityName)
    {
        var response = await _openWeatherMap.GetCityCoordinatesAsync(cityName);
        return Ok(response);
    }

    [HttpGet("GetCurrentWeatherForecastAsync")]
    public async Task<IActionResult> GetCurrentWeatherForecastAsync(string cityName)
    {
        var response = await _openWeatherMap.GetCurrentWeatherForecastAsync(cityName);
        return Ok(response);
    }

    [HttpGet("GetMonthClimaticForecastAsync")]
    public async Task<IActionResult> GetMonthClimaticForecastAsync(List<string> farmLocations)
    {
        var response = await _openWeatherMap.GetMonthClimaticForecastAsync(farmLocations);
        return Ok(response);
    }

    [HttpGet("GetDaysClimaticForecastAsync")]
    public async Task<IActionResult> GetDaysClimaticForecastAsync(List<string> farmLocations)
    {
        var response = await _openWeatherMap.GetDaysClimaticForecastAsync(farmLocations);
        return Ok(response);
    }

    [HttpGet("GetNumLookUp")]
    public async Task<IActionResult> GetNumLookUp(string phoneNumber)
    {
        var response = await _numLookUp.VerifyPhoneNumber(phoneNumber, "ng");
        return Ok(response);
    }
}
