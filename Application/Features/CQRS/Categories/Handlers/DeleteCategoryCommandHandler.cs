using Application.Common.Models;
using Application.Features.CQRS.Categories.Commands;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;

namespace Application.Features.CQRS.Categories.Handlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest, ResponseModel<NoContent>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel<NoContent>> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.GetReadRepository<Category>().GetByIdAsync(request.Id);

            if (category == null)
                return new ResponseModel<NoContent>("Kategori bulunamadı veya daha önce silinmiş.", 404);

            await _unitOfWork.GetWriteRepository<Category>().DeleteAsync(category);
            var saveResult = await _unitOfWork.SaveChangesAsync();

            if (saveResult <= 0)
                return new ResponseModel<NoContent>("Kategori silinirken hata oluştu.", 500);

            return new ResponseModel<NoContent>(new NoContent(), 204);
        }
    }
}