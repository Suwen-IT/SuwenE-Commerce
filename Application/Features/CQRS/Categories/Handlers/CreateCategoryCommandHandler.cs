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
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoyCommandRequest, ResponseModel<int>>
    {

        private readonly IWriteRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IWriteRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResponseModel<int>> Handle(CreateCategoyCommandRequest request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);

            var addResult = await _repository.AddAsync(category);
            if (!addResult)
                return new ResponseModel<int>("Category could not be created", 400);

            await _repository.SaveChangesAsync();
            var categoryDto = _mapper.Map<Category>(category);
            return new ResponseModel<int>(category.Id, 200);
        }
    }
}
