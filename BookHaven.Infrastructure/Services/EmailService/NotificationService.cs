using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs;
namespace BookHaven.Infrastructure.Services.EmailService;
public class NotificationService(IEmailService _emailService) : INotificationService
{
  public async Task SendEmail(EmailRequest emailRequest, CancellationToken? cancellationToken)
    => await _emailService.SendEmail(emailRequest, cancellationToken);
}