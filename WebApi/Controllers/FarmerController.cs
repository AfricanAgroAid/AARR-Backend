using Application.DTOs.Farmers;
using Application.Implementations.Commands;
using Application.Implementations.Commands.Farmers.BulkFarmerRegistration;
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
    [HttpPost("BulkFarmerRegistration")]
    public async Task<IActionResult> BulkFarmerRegistrationAsync([FromBody] List<CreateFarmerRequestModel> memberRequests)
    {
       var response = await _mediatr.Send(new RegisterBulkFarmerCommand(memberRequests));
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
}
