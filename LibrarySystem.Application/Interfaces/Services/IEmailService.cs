using LibrarySystem.Domain.DTOs;

namespace LibrarySystem.Application.Interfaces.Services;
public interface IEmailService
{
    Task SendEmail(EmailRequest email, CancellationToken? cancellationToken = null);
    Task SendEmailWithAttachment(EmailWithAttachmentRequest emailWithAttachmentRequest, CancellationToken? cancellationToken = null);
}
