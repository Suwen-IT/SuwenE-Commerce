using Application.Common.Models;
using Application.Features.CQRS.Categories.Commands;
using Application.Features.DTOs.Categories;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;


namespace Application.Features.CQRS.Categories.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoyCommandRequest, ResponseModel<CategoryDto>>
    {

        private readonly IWriteRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IWriteRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResponseModel<CategoryDto>> Handle(CreateCategoyCommandRequest request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);

            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return new ResponseModel<CategoryDto>(categoryDto, 201);
        }

      
    }
}
