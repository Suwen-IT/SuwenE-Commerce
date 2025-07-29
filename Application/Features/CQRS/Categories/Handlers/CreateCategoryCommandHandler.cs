using Application.Common.Models;
using Application.Features.CQRS.Categories.Commands;
using Application.Features.DTOs.Categories;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CQRS.Categories.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoyCommandRequest, ResponseModel<CategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<CategoryDto>> Handle(CreateCategoyCommandRequest request,
            CancellationToken cancellationToken)
        {
            if (request.ParentCategoryId.HasValue)
            {
                var parentCategory = await _unitOfWork.GetReadRepository<Category>()
                    .GetByIdAsync(request.ParentCategoryId.Value);

                if (parentCategory == null)
                {
                    return new ResponseModel<CategoryDto>("Ana kategori bulunamadı.", 400);
                }
            }

            var existingCategory = await _unitOfWork.GetReadRepository<Category>()
                .GetAsync(c => c.Name == request.Name && c.ParentCategoryId == request.ParentCategoryId);

            if (existingCategory != null)
            {
                return new ResponseModel<CategoryDto>("Bu isimde kategori zaten mevcut.", 400);
            }

            var category = _mapper.Map<Category>(request);

            await _unitOfWork.GetWriteRepository<Category>().AddAsync(category);
            var saveResult = await _unitOfWork.SaveChangesAsync();

            if (saveResult <= 0)
                return new ResponseModel<CategoryDto>("Kategori oluşturulamadı.", 500);

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return new ResponseModel<CategoryDto>(categoryDto, 201);
        }
    }
}