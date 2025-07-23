using Application.Common.Models;
using Application.Features.CQRS.Products.Commands;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, ResponseModel<NoContent>>
{
    private readonly IReadRepository<Product> _readRepository;
    private readonly IWriteRepository<Product> _writeRepository;

    public DeleteProductCommandHandler(IReadRepository<Product> readRepository, IWriteRepository<Product> writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task<ResponseModel<NoContent>> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        var entity = await _readRepository.GetByIdAsync(request.Id);
        if (entity == null)
            return new ResponseModel<NoContent>("Silinmek istenen ürün bulunamadı.", 404);

        await _writeRepository.DeleteAsync(entity);
        var saved = await _writeRepository.SaveChangesAsync();

        if (!saved)
            return new ResponseModel<NoContent>("Ürün silinirken bir hata oluştu.", 500);

        return new ResponseModel<NoContent>(new NoContent(), 204);
    }
}