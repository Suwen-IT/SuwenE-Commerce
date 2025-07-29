using Application.Common.Models;
using Application.Features.CQRS.ProductAttributeValues.Commands;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities.Products;
using MediatR;

public class DeleteProductAttributeValueCommandHandler : IRequestHandler<DeleteProductAttributeValueCommandRequest, ResponseModel<NoContent>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductAttributeValueCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseModel<NoContent>> Handle(DeleteProductAttributeValueCommandRequest request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.GetReadRepository<ProductAttributeValue>().GetByIdAsync(request.Id);
        if (entity == null)
            return new ResponseModel<NoContent>("Silinmek istenen ürün niteliği değeri bulunamadı.", 404);

        await _unitOfWork.GetWriteRepository<ProductAttributeValue>().DeleteAsync(entity);
        var saved = await _unitOfWork.SaveChangesBoolAsync();

        if (!saved)
            return new ResponseModel<NoContent>("Ürün niteliği değeri silinirken bir hata oluştu.", 500);

        return new ResponseModel<NoContent>(new NoContent(), 204);
    }
}