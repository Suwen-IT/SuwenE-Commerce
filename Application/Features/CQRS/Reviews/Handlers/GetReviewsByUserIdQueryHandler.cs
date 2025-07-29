using Application.Common.Models;
using Application.Features.CQRS.Reviews.Queries;
using Application.Features.DTOs.Reviews;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.Features.CQRS.Reviews.Handlers
{
    public class GetReviewsByUserIdQueryHandler : IRequestHandler<GetReviewsByUserIdQueryRequest, ResponseModel<List<ReviewDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetReviewsByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<ReviewDto>>> Handle(GetReviewsByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            var reviews = await _unitOfWork.GetReadRepository<Review>().GetAllAsync(
                predicate: r => r.AppUserId == request.AppUserId,
                include: q => q
                    .Include(r => r.Product)
                    .Include(r => r.AppUser),
                orderBy: q => q.OrderByDescending(r => r.ReviewDate),
                enableTracking: false);

            if (reviews == null || !reviews.Any())
                return new ResponseModel<List<ReviewDto>>("Bu kullanıcıya ait yorum bulunamadı", 404);

            var reviewDtos = _mapper.Map<List<ReviewDto>>(reviews);
            return new ResponseModel<List<ReviewDto>>(reviewDtos, 200);
        }
    }
}