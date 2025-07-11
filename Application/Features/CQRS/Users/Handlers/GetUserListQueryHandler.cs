using Application.Common.Models;
using Application.Features.CQRS.Users.Queries;
using Application.Features.DTOs.Identity;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Users.Handlers
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQueryRequest, ResponseModel<List<UserDto>>>
    {

        private readonly IReadRepository<AppUser> _readRepository;
        private readonly IMapper _mapper;

        public GetUserListQueryHandler(IReadRepository<AppUser> readRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _mapper = mapper;
        }
        public async Task<ResponseModel<List<UserDto>>> Handle(GetUserListQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _readRepository.GetAllAsync(u=>u.IsDeleted==false);

            if(users is null)
                return new ResponseModel<List<UserDto>>("Users are empty");

            var mappedUsers = _mapper.Map<List<UserDto>>(users);
            return new ResponseModel<List<UserDto>>(mappedUsers, 200);
        }
    }
}
