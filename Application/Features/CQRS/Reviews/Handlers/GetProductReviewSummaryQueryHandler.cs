using Application.Common.Models;
using Application.Features.CQRS.Reviews.Queries;
using Application.Features.DTOs.Reviews;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;

namespace Application.Features.CQRS.Reviews.Handlers
{
    public class GetProductReviewSummaryQueryHandler : IRequestHandler<GetProductReviewSummaryQueryRequest, ResponseModel<ProductReviewSummaryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductReviewSummaryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel<ProductReviewSummaryDto>> Handle(GetProductReviewSummaryQueryRequest request, CancellationToken cancellationToken)
        {
            var reviews = await _unitOfWork.GetReadRepository<Review>().GetAllAsync(
                predicate: r => r.ProductId == request.ProductId,
                enableTracking: false);

            if (reviews == null || !reviews.Any())
            {
                return new ResponseModel<ProductReviewSummaryDto>("Bu ürüne ait yorum bulunamadı", 404);
            }

            var summary = new ProductReviewSummaryDto
            {
                AverageRating = Math.Round(reviews.Average(r => r.Rating), 1),
                TotalReviews = reviews.Count
            };

            return new ResponseModel<ProductReviewSummaryDto>(summary, 200);
        }
    }
}