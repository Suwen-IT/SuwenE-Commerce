using Application.Common.Models;
using Application.Features.CQRS.Baskets.Queries;
using Application.Features.DTOs.Baskets;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Baskets;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CQRS.Baskets.Handlers
{
    public class GetBasketByIdQueryHandler : IRequestHandler<GetBasketByIdQueryRequest, ResponseModel<BasketDto>>
    {
        private readonly IReadRepository<Basket> _repository;
        private readonly IMapper _mapper;

        public GetBasketByIdQueryHandler(IReadRepository<Basket> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResponseModel<BasketDto>> Handle(GetBasketByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var basket = await _repository.GetAsync(
               b => b.Id == request.BasketId,
               include: b => b.Include(x => x.BasketItems));

            if (basket == null)
            {
                return new ResponseModel<BasketDto>("Sepet bulunamadı.", 404);
            }

            var basketDto = _mapper.Map<BasketDto>(basket);
            return new ResponseModel<BasketDto>(basketDto, 200);
        }
    }
}
