using Application.Implementations.Queries.CropType.CropTypeToListOfString;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Utiity
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetCropTypeController:ControllerBase
    {
        private readonly IMediator _mediator;

        public GetCropTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetCropType")]
        public async Task<IActionResult> GetCropType()
        {
            var result = await _mediator.Send(new GetCropTypeRequest());
            return result.Succeeded ? Ok(result.Data) : NotFound(result.Messages);
        }
    }
}
