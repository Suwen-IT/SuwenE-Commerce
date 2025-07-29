using Application.Interfaces;
using Application.Interfaces.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Suwen.Infrastructure.Abstracts;
using Suwen.Infrastructure.Concretes;
using Suwen.Infrastructure.Services;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
    {
        // JWT Kimlik Doğrulaması
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme=JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtOptions:Issuer"],
                ValidAudience = configuration["JwtOptions:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOptions:Key"])),
                ClockSkew = TimeSpan.Zero 
            };
            options.Events = new JwtBearerEvents
            {
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    var result = System.Text.Json.JsonSerializer.Serialize(new { error = "Unauthorized" });
                    return context.Response.WriteAsync(result);
                }
            };

        });
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddHostedService<ExpiredReservationCleanupService>();

        services.AddScoped<IEmailSender>(provider =>
        {
            var config = configuration.GetSection("EmailSettings");
            var logger = provider.GetRequiredService<ILogger<EmailSender>>();
            
            var portString = config["SmtpServerPort"];
            int port;
            if (!int.TryParse(portString, out port))
            {
                port = 587;
            }
            
            return new EmailSender(
                config["SmtpServer"],
                port,
                config["SenderName"],
                config["UserName"],
                config["Password"],
                logger);
        });
        services.AddScoped<ITemplateService, TemplateService>();
        services.AddScoped<INotificationService, NotificationService>();


        return services;
    }
}