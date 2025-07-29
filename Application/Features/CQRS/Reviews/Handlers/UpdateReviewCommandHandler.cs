using Application.Common.Models;
using Application.Features.CQRS.Reviews.Commands;
using Application.Features.DTOs.Reviews;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities;
using MediatR;


namespace Application.Features.CQRS.Reviews.Handlers
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommandRequest, ResponseModel<ReviewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateReviewCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<ReviewDto>> Handle(UpdateReviewCommandRequest request, CancellationToken cancellationToken)
        {
            var existingReview = await _unitOfWork.GetReadRepository<Review>().GetByIdAsync(request.Id);
            if (existingReview == null)
                return new ResponseModel<ReviewDto>("Güncellenecek yorum bulunamadı.", 404);

            if (existingReview.AppUserId != request.AppUserId)
                return new ResponseModel<ReviewDto>("Yorum güncellenemedi. Kullanıcı yetkisi yetersiz.", 403);

            existingReview.Rating = request.Rating;
            existingReview.Comment = request.Comment;
            existingReview.UpdatedDate = DateTime.UtcNow;

            await _unitOfWork.GetWriteRepository<Review>().UpdateAsync(existingReview);
            var saved = await _unitOfWork.SaveChangesBoolAsync();

            if (!saved)
                return new ResponseModel<ReviewDto>("Yorum güncellenmedi.", 500);

            var reviewDto = _mapper.Map<ReviewDto>(existingReview);
            return new ResponseModel<ReviewDto>(reviewDto, 200);
        }
    }
}