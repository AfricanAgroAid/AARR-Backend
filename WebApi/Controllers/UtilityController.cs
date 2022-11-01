using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UtilityController : ControllerBase
{
          public UtilityController()
          {

          }
          [HttpGet("SearchWeatherForcastByLocation")]
          public Task<IActionResult> SearchWeatherForcastByLocation
}
