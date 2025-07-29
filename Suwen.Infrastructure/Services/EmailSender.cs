using Application.Interfaces.Services;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Suwen.Infrastructure.Services;

public class EmailSender: IEmailSender
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _senderName;
    private readonly string _smtpUser;
    private readonly string _smtpPassword;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(string smtpServer, int smtpPort,string senderName,
        string smtpUser, string smtpPassword, ILogger<EmailSender> logger)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _senderName = senderName;
        _smtpUser = smtpUser;
        _smtpPassword = smtpPassword;
        _logger = logger;
    }
    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_senderName, _smtpUser));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = body
        };
        message.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();

        try
        {
            await client.ConnectAsync(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_smtpUser, _smtpPassword);
            await client.SendAsync(message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "E-posta gönderimi sırasında hata oluştu. Alıcı: {ToEmail}, Konu: {Subject}", toEmail, subject);
            throw new InvalidOperationException("E-posta gönderimi başarısız oldu.", ex);
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}