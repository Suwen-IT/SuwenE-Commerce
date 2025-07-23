using Application.Common.Models;
using Application.Features.CQRS.Reviews.Queries;
using Application.Features.DTOs.Reviews;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CQRS.Reviews.Handlers
{
    public class GetReviewsByProductIdQueryHandler : IRequestHandler<GetReviewsByProductIdQueryRequest, ResponseModel<List<ReviewDto>>>
    {
        private readonly IReadRepository<Review> _readRepository;
        private readonly IMapper _mapper;
        public GetReviewsByProductIdQueryHandler(IReadRepository<Review> readRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<ReviewDto>>> Handle(GetReviewsByProductIdQueryRequest request, CancellationToken cancellationToken)
        {
            var reviews= await _readRepository.GetAllAsync(
                predicate: r => r.ProductId == request.ProductId,
                include: q => q
                .Include(r=>r.Product)
                .Include(r => r.AppUser),
                orderBy: q => q.OrderByDescending(r => r.ReviewDate),
                enableTracking: false
            );

            if(reviews == null || !reviews.Any())
                return new ResponseModel<List<ReviewDto>>("Bu ürüne ait yorum bulunamadı", 404);

            var reviewDtos = _mapper.Map<List<ReviewDto>>(reviews);
            return new ResponseModel<List<ReviewDto>>(reviewDtos, 200);

        }
    }
}
