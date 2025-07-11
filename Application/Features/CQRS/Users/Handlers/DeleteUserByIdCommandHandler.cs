using Application.Common.Models;
using Application.Features.CQRS.Users.Commands;
using Application.Interfaces.Repositories;
using Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Users.Handlers
{
    public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommandRequest, ResponseModel<bool>>
    {
        private readonly IReadRepository<AppUser> _readRepository;
        private readonly IWriteRepository<AppUser> _writeRepository;

        public DeleteUserByIdCommandHandler(IReadRepository<AppUser> readRepository, IWriteRepository<AppUser> writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }
        public async Task<ResponseModel<bool>> Handle (DeleteUserByIdCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _readRepository.GetByIdAsync(request.Id,tracking:true);

            if (user is not null)
            {
                user.IsDeleted = true;
                await _writeRepository.UpdateAsync(user);
                var saveResult = await _writeRepository.SaveChangesAsync();

                if (saveResult)
                    return new ResponseModel<bool>(true, 200);
               
             return new ResponseModel<bool>("Failed to delete user", 500);
              
            }

            return new ResponseModel<bool>("User not found", 404);
        }
    }
}
