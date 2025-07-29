using Application.Common.Models;
using Application.Features.CQRS.Categories.Commands;
using Application.Features.DTOs.Categories;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CQRS.Categories.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, ResponseModel<CategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<CategoryDto>> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.GetReadRepository<Category>().GetByIdAsync(request.Id);

            if (category == null)
                return new ResponseModel<CategoryDto>("Kategori bulunamadı.", 404);
            
            if (request.ParentCategoryId.HasValue)
            {
                var parentCategory = await _unitOfWork.GetReadRepository<Category>()
                    .GetByIdAsync(request.ParentCategoryId.Value);

                if (parentCategory == null)
                    return new ResponseModel<CategoryDto>("Geçersiz üst kategori.", 400);
                
                if (request.ParentCategoryId == request.Id)
                    return new ResponseModel<CategoryDto>("Kategori kendisi üst kategori olamaz.", 400);
            }
            
            var existingCategory = await _unitOfWork.GetReadRepository<Category>()
                .GetAsync(c => c.Id != request.Id &&
                              c.Name == request.Name &&
                              c.ParentCategoryId == request.ParentCategoryId);

            if (existingCategory != null)
                return new ResponseModel<CategoryDto>("Aynı isimde kategori zaten mevcut.", 400);
            
            _mapper.Map(request, category);

            await _unitOfWork.GetWriteRepository<Category>().UpdateAsync(category);
            var saveResult = await _unitOfWork.SaveChangesAsync();

            if (saveResult <= 0)
                return new ResponseModel<CategoryDto>("Kategori güncellenirken hata oluştu.", 500);

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return new ResponseModel<CategoryDto>(categoryDto, 200);
        }
    }
}
