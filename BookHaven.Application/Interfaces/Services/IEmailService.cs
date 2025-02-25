using BookHaven.Domain.DTOs;

namespace BookHaven.Application.Interfaces.Services;
public interface IEmailService
{
    Task SendEmail(EmailRequest email, CancellationToken? cancellationToken = null);
    Task SendEmailWithAttachment(EmailWithAttachmentRequest emailWithAttachmentRequest, CancellationToken? cancellationToken = null);
}
