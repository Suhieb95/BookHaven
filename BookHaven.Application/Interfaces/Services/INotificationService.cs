using BookHaven.Domain.DTOs;

namespace BookHaven.Application.Interfaces.Services;
public interface INotificationService
{
    Task SendEmail(EmailRequest emailRequest, CancellationToken? cancellationToken = null);
}