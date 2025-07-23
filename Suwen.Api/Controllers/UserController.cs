using Application.Common.Models;
using Application.Features.CQRS.Users.Commands;
using Application.Features.CQRS.Users.Queries;
using Application.Features.DTOs.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

    [HttpGet("getall")]
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

    [HttpGet("get-by-id")]
   
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var currentUserId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(User.IsInRole("User") && currentUserId != id.ToString())
        {
            return Forbid();
        }
        var query = new GetUserByIdQueryRequest(id);
        var response = await _mediator.Send(query);
        if (response.Success)
        {
            return Ok(response);
        }
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("id/for-delete")]
 
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var command=new DeleteUserByIdCommandRequest(id);
        var response = await _mediator.Send(command);
        if (response.Success)
        {
            return NoContent();
        }
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("id/for-update")]
    public async Task<IActionResult> GetUserForUpdate(Guid id)
    {
        var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (User.IsInRole("User") && currentUserId != id.ToString())
        {
            return Forbid();
        }
        var query = new GetUserForUpdateQueryRequest(id);
        var response = await _mediator.Send(query);

        if (response.Success)
        {
            return Ok(response);
        }
        return StatusCode(response.StatusCode, response.Messages);
    }

    [HttpPut("update-by-id")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommandRequest command)
    {
        if (id != command.Id)
        {
          
            return BadRequest(new ResponseModel<NoContentDto>("URL'deki kullan�c� ID'si ile istek g�vdesindeki ID uyu�muyor.", 400));
        }
        var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (User.IsInRole("User") && currentUserId != id.ToString())
        {
            return Forbid();
        }
        var response = await _mediator.Send(command);
        if (response.Success)
        {
            return StatusCode(response.StatusCode);
        }
        else
        {
            return StatusCode(response.StatusCode, response.Messages);
        }
    }
}