using Application.Common.Models;
using Application.Features.CQRS.Users.Commands;
using Application.Features.DTOs.Identity;
using Application.Interfaces;
using Application.Interfaces.Services;
using Domain.Entities.Enums;
using MediatR;

namespace Application.Features.CQRS.Users.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, ResponseModel<AuthResponseDto>>
    {
        private readonly IAuthService _authService;
        private readonly INotificationService _notificationService;

        public RegisterCommandHandler(IAuthService authService, INotificationService notificationService)
        {
            _authService = authService;
            _notificationService = notificationService;
        }

        public async Task<ResponseModel<AuthResponseDto>> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            var authResponse = await _authService.RegisterAsync(request);

            if (authResponse.IsSuccess && Guid.TryParse(authResponse.UserId, out var userGuid))
            {
                var fullName = $"{request.FirstName} {request.LastName}";
                await _notificationService.CreateNotificationAsync(
                    userGuid,
                    $"Hoş geldiniz {fullName}",
                    "Sisteme başarıyla kayıt oldunuz.",
                    NotificationType.Email);
            }

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