using Application.Common.Models;
using Application.Features.DTOs.Reviews;
using MediatR;

namespace Application.Features.CQRS.Reviews.Queries
{
    public class GetReviewsByUserIdQueryRequest:IRequest<ResponseModel<List<ReviewDto>>>
    {
        public Guid AppUserId { get; set; }
    }  
}
