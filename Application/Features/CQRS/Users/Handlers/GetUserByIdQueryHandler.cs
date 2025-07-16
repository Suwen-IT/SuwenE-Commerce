using Application.Common.Models;
using Application.Features.CQRS.Users.Queries;
using Application.Features.DTOs.Identity;
using AutoMapper;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.CQRS.Users.Handlers;

public class GetUserByIdQueryHandler:IRequestHandler<GetUserByIdQueryRequest,ResponseModel<UserDto>>
{
    private readonly UserManager<AppUser>_userManager;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
    
    public async Task<ResponseModel<UserDto>> Handle(GetUserByIdQueryRequest request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Id, out Guid userGuidId))
        {
            return new ResponseModel<UserDto>
            {
                Success = false,
                Messages = new[] { "Not a valid Id!" },
                StatusCode = 400
            };
        }
        
        var user = await _userManager.FindByIdAsync(userGuidId.ToString());

        if (user == null)
        {
            return new ResponseModel<UserDto>
            {
                Success = false,
                Messages = new[] { "User not found!" },
                StatusCode = 400
            };
        }
        
        var userDto = _mapper.Map<UserDto>(user);
        
        var roles = await _userManager.GetRolesAsync(user);
        userDto.Roles = roles.ToList();

        return new ResponseModel<UserDto>
        {
            Data = userDto,
            Success = true,
            Messages = new[] { "User successfully fetched!" },
            StatusCode = 200
        };
    }
}