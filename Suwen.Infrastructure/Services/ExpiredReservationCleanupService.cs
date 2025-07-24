using Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Suwen.Infrastructure.Services
{
    public class ExpiredReservationCleanupService : BackgroundService
    {
        private readonly ILogger<ExpiredReservationCleanupService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(5);

        public ExpiredReservationCleanupService(ILogger<ExpiredReservationCleanupService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Geçmiş rezervasyonları temizleme servisi başlatıldı.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var reservationService = scope.ServiceProvider.GetRequiredService<IReservationService>();

                    var releasedCount = await reservationService.ReleaseExpiredReservationsAsync();
                    _logger.LogInformation("{Time} itibari ile {Count} süresi dolan rezervasyonlar serbest bırakıldı.", releasedCount, DateTime.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Süresi dolan rezervasyonlar serbest bırakılırken hata oluştu.");
                }
                await Task.Delay(_interval, stoppingToken);
            }
            _logger.LogInformation("Geçmiş rezervasyoları temizleme servisi durdu.");
        }
        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Geçmiş rezervasyoları temizleme servisi durduruluyor.");
            await base.StopAsync(stoppingToken);
        }
    }
}
