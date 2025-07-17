using Application.Common.Models;
using Application.Features.CQRS.Users.Commands;
using Application.Features.DTOs.Identity;
using Application.Interfaces;
using AutoMapper;
using MediatR;


namespace Application.Features.CQRS.Users.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, ResponseModel<AuthResponseDto>>
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public LoginCommandHandler(IAuthService authService,IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }
        public async Task<ResponseModel<AuthResponseDto>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var authResponse = await _authService.LoginAsync(request);

            return new ResponseModel<AuthResponseDto>
            {
                Data = authResponse,
                Success = authResponse.IsSuccess,
                Messages = authResponse.Errors?.ToArray(),
                StatusCode = authResponse.IsSuccess ? 200 : 401
            };

        }
    }
}
