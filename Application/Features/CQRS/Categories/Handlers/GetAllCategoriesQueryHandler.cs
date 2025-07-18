using Application.Common.Models;
using Application.Features.CQRS.Categories.Queries;
using Application.Features.DTOs.Categories;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Categories.Handlers
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQueryRequest, ResponseModel<List<CategoryDto>>>
    {
        private readonly IReadRepository<Category> _repository;
        private readonly IMapper _mapper;
        public GetAllCategoriesQueryHandler(IReadRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResponseModel<List<CategoryDto>>> Handle(GetAllCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAllAsync();

            if (categories == null || !categories.Any())
                return new ResponseModel<List<CategoryDto>>("Hiç kategori bulunamadı", 204);

            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            return new ResponseModel<List<CategoryDto>>(categoryDtos, 200);
        }
    }
}
