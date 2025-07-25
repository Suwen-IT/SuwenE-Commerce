using Application.Common.Models;
using MediatR;

namespace Application.Features.CQRS.Orders.Commands
{
    public class DeleteOrderCommandRequest:IRequest<ResponseModel<NoContent>>
    {
        public int Id { get; set; }
        public Guid AppUserId { get; set; }
    }
}
