using Application.Common.Models;
using Application.Features.CQRS.Reviews.Commands;
using Application.Features.DTOs.Reviews;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Identity;
using MediatR;

namespace Application.Features.CQRS.Reviews.Handlers
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommandRequest, ResponseModel<ReviewDto>>
    {
        private readonly IWriteRepository<Review> _writeRepository;
        private readonly IReadRepository<Product> _readProductRepository;
        private readonly IReadRepository<AppUser> _readAppUserRepository;
        private readonly IMapper _mapper;

        public CreateReviewCommandHandler( IWriteRepository<Review> writeRepository, IReadRepository<Product> readProductRepository,
            IReadRepository<AppUser> readAppUserRepository,IMapper mapper)
        {
            _writeRepository = writeRepository;
            _readProductRepository = readProductRepository;
            _readAppUserRepository = readAppUserRepository;
            _mapper = mapper;
        }
        public async Task<ResponseModel<ReviewDto>> Handle(CreateReviewCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _readProductRepository.GetByIdAsync(request.ProductId);
            if (product == null)
                return new ResponseModel<ReviewDto>("Belirtilen ürün bulunamadı", 400);

            var appUser = await _readAppUserRepository.GetByIdAsync(request.AppUserId);
            if (appUser == null)
                return new ResponseModel<ReviewDto>("Belirtilen kullanıcı bulunamadı", 400);

            var review=_mapper.Map<Review>(request);
            await _writeRepository.AddAsync(review);

            var saved = await _writeRepository.SaveChangesAsync();
            if (!saved)
                return new ResponseModel<ReviewDto>("Yorum veritabanına kaydedilemedi", 500);

            var reviewDto = _mapper.Map<ReviewDto>(review);
            return new ResponseModel<ReviewDto>(reviewDto, 201);

        }
    }
}
