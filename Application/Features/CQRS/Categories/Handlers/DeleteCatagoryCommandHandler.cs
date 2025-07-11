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
    public class DeleteCatagoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest, ResponseModel<int>>
    {
        private readonly IReadRepository<Category> _readRepository;
        private readonly IWriteRepository<Category> _writeRepository;
        private readonly IMapper _mapper;

        public DeleteCatagoryCommandHandler(IReadRepository<Category> readRepository, IWriteRepository<Category> writeRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _mapper = mapper;
        }
        public async Task<ResponseModel<int>> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = _readRepository.GetByIdAsync(request.Id);

            if (category == null)
                return new ResponseModel<int>("Category not found or already removed", 400);

            await _writeRepository.DeleteAsync(category.Result);
            await _writeRepository.SaveChangesAsync();

            var categoryDto = _mapper.Map<Category>(category.Result);

            return new ResponseModel<int>(category.Result.Id, 200);
        }
    }
}
