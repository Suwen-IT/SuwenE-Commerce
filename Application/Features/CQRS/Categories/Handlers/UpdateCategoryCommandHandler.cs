using Application.Common.Models;
using Application.Features.CQRS.Categories.Commands;
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
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, ResponseModel<int>>
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
        public async Task<ResponseModel<int>> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _readRepository.GetByIdAsync(request.Id);

            if (category == null)
                return new ResponseModel<int>("Category not found", 404);
            _mapper.Map(request, category);

            await _writeRepository.UpdateAsync(category);
            await _writeRepository.SaveChangesAsync();

            return new ResponseModel<int>(category.Id, 200);

        }
    }
}
