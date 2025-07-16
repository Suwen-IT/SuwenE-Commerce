using Application.Common.Models;
using Application.Features.DTOs.Identity;
using MediatR;


namespace Application.Features.CQRS.Users.Commands
{
    public class DeleteUserByIdCommandRequest:IRequest<ResponseModel<NoContentDto>>
    {
        public Guid Id { get; set; }

        public DeleteUserByIdCommandRequest(Guid id)
        {
            Id = id;
        }
    }
}
