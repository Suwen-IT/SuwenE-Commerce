using Application.Common.Models;
using Application.Features.CQRS.Users.Queries;
using Application.Features.DTOs.Identity;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Application.Features.CQRS.Users.Handlers
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQueryRequest, ResponseModel<List<UserDto>>>
    {

        private readonly UserManager<AppUser>_userManager;
        private readonly IMapper _mapper;

        public GetUserListQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<ResponseModel<List<UserDto>>> Handle(GetUserListQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.AsNoTracking().ToListAsync(cancellationToken);
            
            var userDtos = _mapper.Map<List<UserDto>>(users);

            foreach (var userDto in userDtos)
            {
                var user=users.FirstOrDefault(u =>u.Id.ToString()== userDto.Id);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    userDto.Roles = roles.ToList();
                }
            }

            return new ResponseModel<List<UserDto>>()
            {
                Data = userDtos,
                Success = true,
                Messages = new[] { "Users list successfully!" },
                StatusCode = 200,
            };
        }
    }
}
