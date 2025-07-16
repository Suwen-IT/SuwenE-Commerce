using Application.Common.Models;
using Application.Features.DTOs.Identity;
using MediatR;

namespace Application.Features.CQRS.Users.Queries;

public class GetUserByIdQueryRequest:IRequest<ResponseModel<UserDto>>
{
    public Guid Id { get; set; }

    public GetUserByIdQueryRequest( Guid id)
    {
        Id = id;
    }
}