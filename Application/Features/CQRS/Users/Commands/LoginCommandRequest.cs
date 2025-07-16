using Application.Common.Models;
using Application.Features.DTOs.Identity;
using MediatR;


namespace Application.Features.CQRS.Users.Commands
{
    public class LoginCommandRequest:IRequest<ResponseModel<AuthResponseDto>>
    {
        public string Email { get; set; }=string.Empty;
        public string Password { get; set; }=string.Empty;
    }
}
