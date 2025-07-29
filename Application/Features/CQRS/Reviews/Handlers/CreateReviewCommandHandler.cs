using Application.Common.Models;
using Application.Features.CQRS.Reviews.Commands;
using Application.Features.DTOs.Reviews;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Entities.Products;
using MediatR;

namespace Application.Features.CQRS.Reviews.Handlers
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommandRequest, ResponseModel<ReviewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateReviewCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<ReviewDto>> Handle(CreateReviewCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.GetReadRepository<Product>().GetByIdAsync(request.ProductId);
            if (product == null)
                return new ResponseModel<ReviewDto>("Belirtilen ürün bulunamadı", 400);

            var appUser = await _unitOfWork.GetReadRepository<AppUser>().GetByIdAsync(request.AppUserId);
            if (appUser == null)
                return new ResponseModel<ReviewDto>("Belirtilen kullanıcı bulunamadı", 400);

            var review = _mapper.Map<Review>(request);

            await _unitOfWork.GetWriteRepository<Review>().AddAsync(review);
            var saved = await _unitOfWork.SaveChangesBoolAsync();

            if (!saved)
                return new ResponseModel<ReviewDto>("Yorum veritabanına kaydedilemedi", 500);

            var reviewDto = _mapper.Map<ReviewDto>(review);
            return new ResponseModel<ReviewDto>(reviewDto, 201);
        }
    }
}