using Application.Common.Models;
using Application.Features.CQRS.ProductAttributes.Commands;
using Application.Interfaces.Repositories;
using Domain.Entities.Products;
using MediatR;

namespace Application.Features.CQRS.ProductAttributes.Handlers
{
    public class DeleteProductAttributeCommandHandler : IRequestHandler<DeleteProductAttributeCommandRequest, ResponseModel<NoContent>>
    {
        private readonly IReadRepository<ProductAttribute> _readRepository;
        private readonly IWriteRepository<ProductAttribute> _writeRepository;

        public DeleteProductAttributeCommandHandler(IReadRepository<ProductAttribute> readRepository, IWriteRepository<ProductAttribute> writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        public async Task<ResponseModel<NoContent>> Handle(DeleteProductAttributeCommandRequest request, CancellationToken cancellationToken)
        {
            var existingEntity = await _readRepository.GetByIdAsync(request.Id);
            if (existingEntity == null)
                return new ResponseModel<NoContent>("Silinmek istenen ürün niteliği bulunamadı.", 404);

            await _writeRepository.DeleteAsync(existingEntity);
            var saved = await _writeRepository.SaveChangesAsync();

            if (!saved)
                return new ResponseModel<NoContent>("Ürün özelliği silinirken bir hata oluştu.", 500);

            return new ResponseModel<NoContent>(new NoContent(), 204);
        }
    }
}
