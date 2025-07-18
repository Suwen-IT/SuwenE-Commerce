using Application.Common.Models;
using Application.Features.CQRS.Categories.Queries;
using Application.Features.DTOs.Categories;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CQRS.Categories.Handlers
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQueryRequest, ResponseModel<CategoryDto>>
    {
        private readonly IReadRepository<Category> _repository;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(IReadRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResponseModel<CategoryDto>> Handle(GetCategoryByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id);

            if (category == null)
                return new ResponseModel<CategoryDto>($"Kategoi bulunamadı.(Id:{request.Id})", 204);

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return new ResponseModel<CategoryDto>(categoryDto, 200);
        }
    }
}
