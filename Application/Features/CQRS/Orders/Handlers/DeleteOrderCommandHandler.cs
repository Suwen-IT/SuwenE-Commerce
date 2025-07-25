using Application.Common.Models;
using Application.Features.CQRS.Orders.Commands;
using Application.Interfaces.Repositories;
using Domain.Entities.Orders;
using MediatR;

namespace Application.Features.CQRS.Orders.Handlers
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommandRequest, ResponseModel<NoContent>>
    {
        private readonly IReadRepository<Order> _orderReadRepository;
        private readonly IWriteRepository<Order> _orderWriteRepository;

        public DeleteOrderCommandHandler(
            IReadRepository<Order> orderReadRepository,
            IWriteRepository<Order> orderWriteRepository)
        {
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
        }
        public async Task<ResponseModel<NoContent>> Handle(DeleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderReadRepository.GetAsync(x => x.Id == request.Id && x.AppUserId == request.AppUserId);

            if (order == null)
                return new ResponseModel<NoContent>("Sipariş bulunamadı.", 404);

            await _orderWriteRepository.DeleteAsync(order);
            var result = await _orderWriteRepository.SaveChangesAsync();

            if (!result)
                return new ResponseModel<NoContent>("Sipariş silinemedi.", 500);

            return new ResponseModel<NoContent>(new NoContent(),204);
        }
    }
}
