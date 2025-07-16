using Application.Common.Models;
using Application.Features.DTOs.Identity;
using MediatR;


namespace Application.Features.CQRS.Users.Commands
{
    public class DeleteUserByIdCommandRequest:IRequest<ResponseModel<NoContentDto>>
    {
        public string Id { get; set; }

        public DeleteUserByIdCommandRequest(string id)
        {
            Id = id;
        }
    }
}
