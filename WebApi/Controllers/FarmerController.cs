using Application.Implementations.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FarmerController : ControllerBase
{
    private readonly IMediator _mediatr;

    public FarmerController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }
 
    [HttpPost("RegisterFarmerAsync")]
    public async Task<IActionResult> RegisterFarmerAsync([FromBody] RegisterFarmerCommand command )
    {
       var response = await _mediatr.Send(command);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
}
