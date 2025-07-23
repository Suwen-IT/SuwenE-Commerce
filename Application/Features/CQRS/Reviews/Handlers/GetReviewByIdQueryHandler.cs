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
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQueryRequest, ResponseModel<ReviewDto>>
    {
        private readonly IReadRepository<Review> _readRepository;
        private readonly IMapper _mapper;

        public GetReviewByIdQueryHandler(IReadRepository<Review> readRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _mapper = mapper;
        }
        public async Task<ResponseModel<ReviewDto>> Handle(GetReviewByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var review = await _readRepository.GetAsync(
                predicate: r => r.Id == request.Id,
                include: q => q
                .Include(r => r.Product)
                .Include(r => r.AppUser),
                enableTracking: false
                );

            if (review == null)
                return new ResponseModel<ReviewDto>("Yorum bulunamadı.", 404);

            var dto = _mapper.Map<ReviewDto>(review);
            return new ResponseModel<ReviewDto>(dto, 200);
        }
    }
}
