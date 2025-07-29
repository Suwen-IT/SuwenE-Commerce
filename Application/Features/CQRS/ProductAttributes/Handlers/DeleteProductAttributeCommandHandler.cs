using Application.Common.Models;
using Application.Features.CQRS.ProductAttributes.Commands;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities.Products;
using MediatR;

namespace Application.Features.CQRS.ProductAttributes.Handlers
{
    public class DeleteProductAttributeCommandHandler : IRequestHandler<DeleteProductAttributeCommandRequest, ResponseModel<NoContent>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductAttributeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel<NoContent>> Handle(DeleteProductAttributeCommandRequest request, CancellationToken cancellationToken)
        {
            var existingEntity = await _unitOfWork.GetReadRepository<ProductAttribute>().GetByIdAsync(request.Id);
            if (existingEntity == null)
                return new ResponseModel<NoContent>("Silinmek istenen ürün niteliği bulunamadı.", 404);

            await _unitOfWork.GetWriteRepository<ProductAttribute>().DeleteAsync(existingEntity);
            var saved = await _unitOfWork.SaveChangesBoolAsync();

            if (!saved)
                return new ResponseModel<NoContent>("Ürün özelliği silinirken bir hata oluştu.", 500);

            return new ResponseModel<NoContent>(new NoContent(), 204);
        }
    }
}