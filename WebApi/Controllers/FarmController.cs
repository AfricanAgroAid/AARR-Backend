using Application.DTOs.Farms;
using Application.Implementations.Commands.Farms;
using Application.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarmController : ControllerBase
    {
        private readonly IMediator _mediatr;
        private readonly IFarmRepository _farmRepository;

        public FarmController(IMediator mediatr, IFarmRepository farmRepository)
        {
            _mediatr = mediatr;
            _farmRepository = farmRepository;
        }

        [HttpPost("BulkFarmRegistration")]
        public async Task<IActionResult> BulkFarmRegistrationAsync([FromBody] List<CreateFarmRequestModel> requests)
        {
            var response = await _mediatr.Send(new RegisterBulkFarmsCommand(requests));
            return response.Succeeded ? Ok(response) : BadRequest(response);
        }
        [HttpGet("WeatherResponse")]
        public async Task<IActionResult> WeatherResponse()
        {
            var response = await _farmRepository.Hazard();
            return Ok(response);
        }
    }
}
