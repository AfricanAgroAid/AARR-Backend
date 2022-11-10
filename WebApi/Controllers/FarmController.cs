using Application.DTOs.Farms;
using Application.Implementations.Commands.Farms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarmController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public FarmController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost("BulkFarmRegistration")]
        public async Task<IActionResult> BulkFarmRegistrationAsync([FromBody] List<CreateFarmRequestModel> requests)
        {
            var response = await _mediatr.Send(new RegisterBulkFarmsCommand(requests));
            return response.Succeeded ? Ok(response) : BadRequest(response);
        }
    }
}
