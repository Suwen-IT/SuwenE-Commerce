using Application.Common.Models;
using Application.Features.DTOs.Identity;
using MediatR;


namespace Application.Features.CQRS.Users.Commands
{
    public class RegisterCommandRequest:IRequest<ResponseModel<AuthResponseDto>>
    {
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; }=string.Empty;
        public string UserName { get; set; }=string.Empty;
        public string Email { get; set; }=string.Empty;
        public string PhoneNumber { get; set; }=string.Empty;
        public string Password { get; set; }=string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
