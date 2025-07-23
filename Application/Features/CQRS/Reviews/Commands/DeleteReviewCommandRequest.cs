using Application.Common.Models;
using MediatR;

namespace Application.Features.CQRS.Reviews.Commands
{
    public class DeleteReviewCommandRequest:IRequest<ResponseModel<NoContent>>
    {
        public int Id { get; set; }
        public Guid AppUserId { get; set; }
    }
}
