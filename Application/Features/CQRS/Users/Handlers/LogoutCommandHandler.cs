using Application.Common.Models;
using Application.Features.CQRS.Users.Commands;
using Application.Interfaces;
using Application.Interfaces.Services;
using MediatR;

namespace Application.Features.CQRS.Users.Handlers
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommandRequest, ResponseModel<object>>
    {
        private readonly IAuthService _authService;
        private readonly ICurrentUserService _currentUserSevice;

        public LogoutCommandHandler(IAuthService authService, ICurrentUserService currentUserSevice)
        {
            _authService = authService;
            _currentUserSevice = currentUserSevice;
        }
        public async Task<ResponseModel<object>> Handle(LogoutCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserSevice.UserId;

            if (string.IsNullOrEmpty(userId))
            {
                return new ResponseModel<object>("Kullanıcı oturumu bulunamadı", 401);
            }
            var success = await _authService.InvalidateRefreshTokenAsync(userId);

            if (!success)
            {
                return new ResponseModel<object>("Çıkış işlemi gerçekleştirilmedi. Lütfen tekrar deneyiniz", 400);
            }

            return new ResponseModel<object>(success, 200)
            {
                Messages = new[] { "Çıkış işlemi başarılı" }
            };
        }
    }
}
