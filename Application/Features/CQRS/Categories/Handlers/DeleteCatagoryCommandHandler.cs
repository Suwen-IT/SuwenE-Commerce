using Application.Common.Models;
using Application.Features.CQRS.Categories.Commands;
using Application.Features.DTOs.Categories;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CQRS.Categories.Handlers
{
    public class DeleteCatagoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest, ResponseModel<NoContent>>
    {
        private readonly IReadRepository<Category> _readRepository;
        private readonly IWriteRepository<Category> _writeRepository;

        public DeleteCatagoryCommandHandler(IReadRepository<Category> readRepository, IWriteRepository<Category> writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }
        public async Task<ResponseModel<NoContent>> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category =await _readRepository.GetByIdAsync(request.Id);

            if (category == null)
                return new ResponseModel<NoContent>("Katagori bulunamadı veya daha önce silinmiş.", 204);

            _writeRepository.DeleteAsync(category);
            await _writeRepository.SaveChangesAsync();

            return new ResponseModel<NoContent>(new NoContent(), 204);
        }
    }
}
