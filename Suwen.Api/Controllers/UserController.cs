using Application.Features.CQRS.Users.Commands;
using Application.Features.CQRS.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Suwen.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController:ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var query = new GetUserListQueryRequest();
        var response = await _mediator.Send(query);
        if (response.Success)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var query = new GetUserByIdQueryRequest(id);
        var response = await _mediator.Send(query);
        if (response.Success)
        {
            return Ok(response);
        }
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var command=new DeleteUserByIdCommandRequest(id);
        var response = await _mediator.Send(command);
        if (response.Success)
        {
            return NoContent();
        }
        return StatusCode(response.StatusCode, response);
    }
}