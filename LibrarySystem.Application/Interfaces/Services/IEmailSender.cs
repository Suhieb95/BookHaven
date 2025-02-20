using LibrarySystem.Domain.DTOs;

namespace LibrarySystem.Application.Interfaces.Services;
public interface IEmailService
{
    Task SendEmail(EmailRequest email);
    Task SendEmailWithAttachment(EmailWithAttachmentRequest emailWithAttachmentRequest);
}
