using Application.Common.Models;
using Application.Features.DTOs.Identity;
using MediatR;

namespace Application.Features.CQRS.Users.Commands
{
    public class UpdateUserCommandRequest:IRequest<ResponseModel<NoContentDto>>
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 
        public string? Email { get; set; } 
        public string? PhoneNumber { get; set; }
        public string? UserName { get; set; }

    }
}
