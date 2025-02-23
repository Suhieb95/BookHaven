using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
namespace LibrarySystem.Infrastructure.Services.EmailService;
public class NotificationService(IEmailService _emailService) : INotificationService
{
    public async Task SendEmail(EmailRequest emailRequest)
      => await _emailService.SendEmail(emailRequest);
}