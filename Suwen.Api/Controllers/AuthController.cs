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
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            var command = new RegisterCommandRequest()
            {
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                UserName = request.Username,
                ConfirmPassword = request.ConfirmPassword,
            };
            var response = await _mediator.Send(command);

            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var query = new LoginCommandRequest
            {
                Email = request.Email,
                Password = request.Password
            };
            
            var response = await _mediator.Send(query);
            if (response.Success)
            {
                return Ok(response);
            }
            return Unauthorized(response);
        }
        
    }
}
