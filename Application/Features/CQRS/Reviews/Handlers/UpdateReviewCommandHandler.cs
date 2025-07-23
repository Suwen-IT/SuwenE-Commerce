using Application.Common.Models;
using Application.Features.CQRS.Reviews.Commands;
using Application.Features.DTOs.Reviews;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CQRS.Reviews.Handlers
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommandRequest, ResponseModel<ReviewDto>>
    {
        private IReadRepository<Review> _readRepository;
        private IWriteRepository<Review> _writeRepository;
        private IMapper _mapper;

        public UpdateReviewCommandHandler(IReadRepository<Review> readRepository, IWriteRepository<Review> writeRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<ReviewDto>> Handle(UpdateReviewCommandRequest request, CancellationToken cancellationToken)
        {
          var existingReview= await _readRepository.GetByIdAsync(request.Id);
            if (existingReview == null)
                return new ResponseModel<ReviewDto>("Güncellenecek yorum bulunamadı.", 404);

            if (existingReview.AppUserId != request.AppUserId)
                return new ResponseModel<ReviewDto>("Yorum güncellenemedi. Kullanıcı yetkisi yetersiz.", 403);

            existingReview.Rating = request.Rating;
            existingReview.Comment = request.Comment;
            existingReview.UpdatedDate = DateTime.UtcNow;

            await _writeRepository.UpdateAsync(existingReview);
            var saved=await _writeRepository.SaveChangesAsync();
            if(!saved)
                return new ResponseModel<ReviewDto>("Yorum güncellenmedi.", 500);

            var reviewDto = _mapper.Map<ReviewDto>(existingReview);
            return new ResponseModel<ReviewDto>(reviewDto, 200);
        }
    }
}
