using Application.Common.Models;
using Application.Features.CQRS.Users.Commands;
using Application.Features.DTOs.Identity;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Identity;
using MediatR;


namespace Application.Features.CQRS.Users.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, ResponseModel<AuthResponseDto>>
    {
        private readonly IAuthService _authService;
       private readonly IMapper _mapper;

        public RegisterCommandHandler(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }
        public async Task<ResponseModel<AuthResponseDto>> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            var authResponse=await _authService.RegisterAsync(request);

            return new ResponseModel<AuthResponseDto>
            {
                Data = authResponse,
                Success = authResponse.IsSuccess,
                Messages = authResponse.Errors?.ToArray(),
                StatusCode = authResponse.IsSuccess ? 200 : 400
            };

        }
    }
}
