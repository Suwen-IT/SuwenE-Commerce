using Application.Common.Models;
using Application.Features.CQRS.Users.Commands;
using Application.Features.DTOs.Identity;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Users.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, ResponseModel<UserLoginDto>>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<ResponseModel<UserLoginDto>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            UserLoginDto loginResult = await _authService.LoginAsync(request);

            if(loginResult.Errors.Any())
      
              return new ResponseModel<UserLoginDto>(loginResult.Errors, 400); 
              
         return new ResponseModel<UserLoginDto>(loginResult, 200);

        }
    }
}
