using Application.Common.Models;
using Application.Features.DTOs.Identity;
using MediatR;

namespace Application.Features.CQRS.Products.Queries
{
    public class GetUserForUpdateQueryRequest:IRequest<ResponseModel<UserUpdateDto>>
    {
        public Guid Id { get; set; }

        public GetUserForUpdateQueryRequest(Guid id)
        {
            Id = id;
        }
    }
}
