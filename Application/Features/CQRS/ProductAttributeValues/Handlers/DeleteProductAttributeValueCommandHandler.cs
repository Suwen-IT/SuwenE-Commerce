using Application.Common.Models;
using Application.Features.CQRS.ProductAttributeValues.Commands;
using Application.Interfaces.Repositories;
using Domain.Entities.Products;
using MediatR;

public class DeleteProductAttributeValueCommandHandler : IRequestHandler<DeleteProductAttributeValueCommandRequest, ResponseModel<NoContent>>
{
    private readonly IReadRepository<ProductAttributeValue> _readRepository;
    private readonly IWriteRepository<ProductAttributeValue> _writeRepository;

    public DeleteProductAttributeValueCommandHandler(
        IReadRepository<ProductAttributeValue> readRepository,
        IWriteRepository<ProductAttributeValue> writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task<ResponseModel<NoContent>> Handle(DeleteProductAttributeValueCommandRequest request, CancellationToken cancellationToken)
    {
        var entity = await _readRepository.GetByIdAsync(request.Id);
        if (entity == null)
            return new ResponseModel<NoContent>("Silinmek istenen ürün niteliği değeri bulunamadı.", 404);

        await _writeRepository.DeleteAsync(entity);
        var saved = await _writeRepository.SaveChangesAsync();

        if (!saved)
            return new ResponseModel<NoContent>("Ürün niteliği değeri silinirken bir hata oluştu.", 500);

        return new ResponseModel<NoContent>(new NoContent(), 204);
    }
}