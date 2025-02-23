using LibrarySystem.Domain.DTOs;

namespace LibrarySystem.Application.Interfaces.Services;
public interface INotificationService
{
    Task SendEmail(EmailRequest emailRequest, CancellationToken? cancellationToken = null);
}