using Application.Common.Models;
using Application.Features.CQRS.Users.Commands;
using Application.Features.DTOs.Identity;
using Application.Interfaces;
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
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, ResponseModel<UserRegisterDto>>
    {
        private readonly IAuthService _authService;
        private readonly IReadRepository<AppUser> _readRepository;
        private readonly IMediator _mediator;

        public RegisterCommandHandler(IAuthService authService, IReadRepository<AppUser> readRepository, IMediator mediator)
        {
            _authService = authService;
            _readRepository = readRepository;
            _mediator = mediator;
        }
        public async Task<ResponseModel<UserRegisterDto>> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            UserRegisterDto registerResult = await _authService.RegisterAsync(request);

            if (registerResult.Errors.Any())
              return new ResponseModel<UserRegisterDto>(registerResult.Errors, 400);
            
            var user= await _readRepository.GetByIdAsync(registerResult.UserId);

            return new ResponseModel<UserRegisterDto>(registerResult, 200);

        }
    }
}
