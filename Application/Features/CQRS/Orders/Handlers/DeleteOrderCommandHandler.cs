using Application.Common.Models;
using Application.Features.CQRS.Orders.Commands;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities.Orders;
using MediatR;

namespace Application.Features.CQRS.Orders.Handlers
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommandRequest, ResponseModel<NoContent>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel<NoContent>> Handle(DeleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.GetReadRepository<Order>().GetAsync(o => o.Id == request.Id && o.AppUserId == request.AppUserId);

            if (order == null)
                return new ResponseModel<NoContent>("Sipariş bulunamadı.", 404);

            await _unitOfWork.GetWriteRepository<Order>().DeleteAsync(order);

            var result = await _unitOfWork.SaveChangesAsync();
            if (result <= 0)
                return new ResponseModel<NoContent>("Sipariş silinemedi.", 500);

            return new ResponseModel<NoContent>(new NoContent(), 204);
        }
    }
}