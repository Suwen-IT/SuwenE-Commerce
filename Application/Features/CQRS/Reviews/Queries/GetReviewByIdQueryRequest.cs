using Application.Common.Models;
using Application.Features.DTOs.Reviews;
using MediatR;

namespace Application.Features.CQRS.Reviews.Queries
{
    public class GetReviewByIdQueryRequest:IRequest<ResponseModel<ReviewDto>>
    {
        public int Id { get; set; }
    }
}
