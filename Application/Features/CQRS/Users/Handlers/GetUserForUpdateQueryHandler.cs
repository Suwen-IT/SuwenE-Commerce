using Application.Common.Models;
using Application.Features.CQRS.Users.Queries;
using Application.Features.DTOs.Identity;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.CQRS.Users.Handlers
{
    public class GetUserForUpdateQueryHandler : IRequestHandler<GetUserForUpdateQueryRequest, ResponseModel<UserUpdateDto>>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetUserForUpdateQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ResponseModel<UserUpdateDto>> Handle(GetUserForUpdateQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                return new ResponseModel<UserUpdateDto>("Kullanıcı bulunamadı.", 404);
            }

            var userDto= new UserUpdateDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };
            return new ResponseModel<UserUpdateDto>(userDto, 200);
        }
    }
}
