using Application.Common.Models;
using Application.Features.CQRS.Users.Commands;
using Application.Features.CQRS.Users.Queries;
using Application.Features.DTOs.Identity;
using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Suwen.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITokenService _tokenService;

        public AuthController(IMediator mediator, ITokenService tokenService)
        {
            _mediator = mediator;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommandRequest request)
        {
            ResponseModel<UserRegisterDto> response = await _mediator.Send(request);

            if (response.Success is false)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommandRequest request)
        {
            ResponseModel<UserLoginDto> response = await _mediator.Send(request);
            if (response.Success is false)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("get-user-list")]

        public async Task<IActionResult> GetUserList()
        {
            var command = new GetUserListQueryRequest();
            var result = await _mediator.Send(command);

            return result.Success ? Ok(result)
                : BadRequest(result);
        }

        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUserById([FromRoute] Guid userId)
        {
            ResponseModel<bool> response = await _mediator.Send(new DeleteUserByIdCommandRequest(userId));

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
