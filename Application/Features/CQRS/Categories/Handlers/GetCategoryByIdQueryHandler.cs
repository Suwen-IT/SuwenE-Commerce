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
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQueryRequest, ResponseModel<CategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<CategoryDto>> Handle(GetCategoryByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.GetReadRepository<Category>()
                .GetAsync(c=>c.Id==request.Id, include:q=>q.Include(c=>c.SubCategories));

            if (category == null)
                return new ResponseModel<CategoryDto>($"Kategori bulunamadı. (Id: {request.Id})", 404);

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return new ResponseModel<CategoryDto>(categoryDto, 200);
        }
    }
}