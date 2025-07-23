using Application.Common.Models;
using Application.Features.DTOs.Reviews;
using MediatR;

namespace Application.Features.CQRS.Reviews.Queries
{
    public class GetProductReviewSummaryQueryRequest:IRequest<ResponseModel<ProductReviewSummaryDto>>
    {
        public int ProductId { get; set; }
    }
}
