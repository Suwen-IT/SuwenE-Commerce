using Application.Common.Models;
using Application.Features.CQRS.Categories.Queries;
using Application.Features.DTOs.Categories;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CQRS.Categories.Handlers
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQueryRequest, ResponseModel<List<CategoryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<CategoryDto>>> Handle(GetAllCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.GetReadRepository<Category>()
                .GetAllAsync(include:q=>q.Include(c=>c.SubCategories));

            if (categories == null || !categories.Any())
                return new ResponseModel<List<CategoryDto>>("Hiç kategori bulunamadı", 404);

            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            return new ResponseModel<List<CategoryDto>>(categoryDtos, 200);
        }
    }
}