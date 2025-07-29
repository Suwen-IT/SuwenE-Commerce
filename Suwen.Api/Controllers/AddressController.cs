using Application.Features.CQRS.Addresses.Commands;
using Application.Features.CQRS.Addresses.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Suwen.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAddress(int id, Guid userId)
        {
            var request = new DeleteAddressCommandRequest{Id = id,AppUserId = userId};
            var result = await _mediator.Send(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetAddressById(int id, Guid userId)
        {
            var request = new GetAddressByIdQueryRequest { Id = id, AppUserId = userId };
            var result = await _mediator.Send(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAddresses(Guid userId)
        {
            var request = new GetAllAddressesQueryRequest { AppUserId = userId};
            var result = await _mediator.Send(request);
            return StatusCode(result.StatusCode, result);
        }


        
    }
}
