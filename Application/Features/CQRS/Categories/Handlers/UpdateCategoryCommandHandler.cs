using Application.Common.Models;
using Application.Features.CQRS.Categories.Commands;
using Application.Features.DTOs.Categories;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;


namespace Application.Features.CQRS.Categories.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, ResponseModel<CategoryDto>>
    {
        private readonly IWriteRepository<Category> _writeRepository;
        private readonly IReadRepository<Category> _readRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IWriteRepository<Category> writeRepository,IReadRepository<Category> readRepository ,IMapper mapper)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _mapper = mapper;
        }
        public async Task<ResponseModel<CategoryDto>> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _readRepository.GetByIdAsync(request.Id);

            if (category == null)
                return new ResponseModel<CategoryDto>("Kategori bulunamadı.", 404);
            _mapper.Map(request, category);

            await _writeRepository.UpdateAsync(category);
            await _writeRepository.SaveChangesAsync();

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return new ResponseModel<CategoryDto>(categoryDto, 200);

        }
    }
}
