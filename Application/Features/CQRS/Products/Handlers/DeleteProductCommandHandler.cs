using Application.Common.Models;
using Application.Features.CQRS.Products.Commands;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities.Products;
using MediatR;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, ResponseModel<NoContent>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseModel<NoContent>> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.GetReadRepository<Product>().GetByIdAsync(request.Id);
        if (entity == null)
            return new ResponseModel<NoContent>("Silinmek istenen ürün bulunamadı.", 404);

        await _unitOfWork.GetWriteRepository<Product>().DeleteAsync(entity);
        var saved = await _unitOfWork.SaveChangesBoolAsync();

        if (!saved)
            return new ResponseModel<NoContent>("Ürün silinirken bir hata oluştu.", 500);

        return new ResponseModel<NoContent>(new NoContent(), 204);
    }
}