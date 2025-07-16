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
using Application.Features.DTOs.Identity;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.CQRS.Users.Handlers
{
    public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommandRequest, ResponseModel<NoContentDto>>
    {
        private readonly UserManager<AppUser> _userManager;

        public DeleteUserByIdCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ResponseModel<NoContentDto>> Handle (DeleteUserByIdCommandRequest request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.Id, out Guid userIdGuid))
            {
                return new ResponseModel<NoContentDto>
                {
                    Success = false,
                    Messages = new[] { "Invalid user ID format." },
                    StatusCode = 400,
                };
            }
            var user = await _userManager.FindByIdAsync(userIdGuid.ToString());
            if (user == null)
            {
                return new ResponseModel<NoContentDto>
                {
                    Success = false,
                    Messages = new[] { "User not found." },
                    StatusCode = 404,
                };
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return new ResponseModel<NoContentDto>
                {
                    Success = true,
                    Messages = new[] { "User successfully deleted." },
                    StatusCode = 200,
                };
            }
            else
            {
                List<string>errors=new List<string>();
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }

                return new ResponseModel<NoContentDto>
                {
                    Success = false,
                    Messages = errors.ToArray(),
                    StatusCode = 400,
                };
            }
           
        }
    }
}
