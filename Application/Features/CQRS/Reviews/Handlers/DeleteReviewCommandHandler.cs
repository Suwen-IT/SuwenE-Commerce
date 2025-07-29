using Application.Common.Models;
using Application.Features.CQRS.Reviews.Commands;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;


namespace Application.Features.CQRS.Reviews.Handlers
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommandRequest, ResponseModel<NoContent>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReviewCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel<NoContent>> Handle(DeleteReviewCommandRequest request, CancellationToken cancellationToken)
        {
            var review = await _unitOfWork.GetReadRepository<Review>().GetByIdAsync(request.Id);
            if (review == null)
                return new ResponseModel<NoContent>("Yorum bulunamadı.", 404);

            if (review.AppUserId != request.AppUserId)
                return new ResponseModel<NoContent>("Bu yorumu silme yetkiniz yok.", 403);

            await _unitOfWork.GetWriteRepository<Review>().DeleteAsync(review);
            var result = await _unitOfWork.SaveChangesBoolAsync();

            if (!result)
                return new ResponseModel<NoContent>("Yorum silinemedi.", 500);

            return new ResponseModel<NoContent>(new NoContent(), 204);
        }
    }
}