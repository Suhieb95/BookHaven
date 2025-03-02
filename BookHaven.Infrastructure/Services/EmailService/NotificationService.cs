using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs;
namespace BookHaven.Infrastructure.Services.EmailService;
public class NotificationService(IEmailService emailService) : INotificationService
{
    private readonly IEmailService _emailService = emailService;
    public async Task SendEmail(EmailRequest emailRequest, CancellationToken? cancellationToken)
    => await _emailService.SendEmail(emailRequest, cancellationToken);
}