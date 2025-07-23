using Application.Common.Models;
using Application.Features.DTOs.Reviews;
using MediatR;

namespace Application.Features.CQRS.Reviews.Queries
{
    public class GetReviewsByProductIdQueryRequest:IRequest<ResponseModel<List<ReviewDto>>>
    {
        public int ProductId { get; set; }
   
    }
}
