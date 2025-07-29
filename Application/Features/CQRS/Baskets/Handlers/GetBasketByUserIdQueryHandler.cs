using Application.Common.Models;
using Application.Features.CQRS.Baskets.Queries;
using Application.Features.DTOs.Baskets;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities.Baskets;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CQRS.Baskets.Handlers
{
    public class GetBasketByUserIdQueryHandler : IRequestHandler<GetBasketByUserIdQueryRequest, ResponseModel<BasketDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBasketByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<BasketDto>> Handle(GetBasketByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            var basket = await _unitOfWork.GetReadRepository<Basket>()
                .GetAsync(
                    predicate: b => b.AppUserId == request.AppUserId,
                    include: b => b.Include(b => b.BasketItems),
                    enableTracking: false);

            if (basket == null)
            {
                return new ResponseModel<BasketDto>("Kullanıcıya ait sepet bulunamadı.", 404);
            }

            var basketDto = _mapper.Map<BasketDto>(basket);
            return new ResponseModel<BasketDto>(basketDto, 200);
        }
    }
}