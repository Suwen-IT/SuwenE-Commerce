using Application.Common.Models;
using Application.Features.CQRS.Baskets.Queries;
using Application.Features.DTOs.Baskets;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CQRS.Baskets.Handlers
{
    public class GetBasketByUserIdQueryHandler : IRequestHandler<GetBasketByUserIdQueryRequest, ResponseModel<BasketDto>>
    {
        private readonly IReadRepository<Basket> _basketReadRepository;
        private readonly IMapper _mapper;

        public GetBasketByUserIdQueryHandler(IReadRepository<Basket> basketReadRepository, IMapper mapper)
        {
            _basketReadRepository = basketReadRepository;
            _mapper = mapper;
        }
        public async Task<ResponseModel<BasketDto>> Handle(GetBasketByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            var basket = await _basketReadRepository.GetAsync(
                b => b.AppUserId == request.AppUserId,
                include: b => b.Include(x => x.BasketItems));

            if (basket == null)
            {
                return new ResponseModel<BasketDto>("Kullanıcıya ait sepet bulunamadı.", 404);
            }

            var basketDto = _mapper.Map<BasketDto>(basket);
            return new ResponseModel<BasketDto>(basketDto, 200);
        }
    }
}
