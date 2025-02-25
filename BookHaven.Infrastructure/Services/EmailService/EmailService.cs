using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Microsoft.AspNetCore.Http;
using MimeKit.Text;
using BookHaven.Domain.DTOs;
using BookHaven.Domain.Entities;
using BookHaven.Application.Interfaces.Services;

namespace BookHaven.Infrastructure.Services.EmailService;
public class EmailService(IOptions<EmailSettings> emailSettings) : IEmailService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;
    public async Task SendEmail(EmailRequest email, CancellationToken? cancellationToken)
        => await SendEmail(GenerateMimeMessage(email), cancellationToken);

    public async Task SendEmailWithAttachment(EmailWithAttachmentRequest request, CancellationToken? cancellationToken)
    {
        var message = GenerateMimeMessage(EmailRequest.ToDTO(request));

        (string fileType, string fileSubType) = GetFileFormat(request.File);

        var attachment = new MimePart(fileType, fileSubType)
        {
            Content = new MimeContent(request.File.OpenReadStream(), ContentEncoding.Default),
            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            ContentTransferEncoding = ContentEncoding.Base64,
            FileName = request.File.FileName
        };

        var multipart = new Multipart("mixed")
        {
            message.Body,
            attachment
        };

        message.Body = multipart;
        await SendEmail(message, cancellationToken);
    }
    private MimeMessage GenerateMimeMessage(EmailRequest emailModel, TextFormat format = TextFormat.Html)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("BookHaven", _emailSettings.EmailAddress));
        message.To.Add(MailboxAddress.Parse(emailModel.To.ToString()));
        message.Subject = emailModel.Subject;
        message.Body = new TextPart(format) { Text = emailModel.Body };
        return message;
    }
    private async Task SendEmail(MimeMessage message, CancellationToken? cancellationToken)
    {
        using var smtp = new SmtpClient();
        CancellationToken ct = cancellationToken ?? CancellationToken.None;
        await smtp.ConnectAsync(_emailSettings.SmtpHost, _emailSettings.SmtpPort, SecureSocketOptions.StartTls, ct);
        await smtp.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password, ct);
        await smtp.SendAsync(message, ct);
        await smtp.DisconnectAsync(true, ct);
    }
    private static (string, string) GetFileFormat(IFormFile file)
    {
        string format = file.ContentType switch
        {
            "image/png" => "image/png",
            "image/jpeg" => "image/jpeg",
            "image/jpg" => "image/jpg",
            "image/gif" => "image/gif",
            "application/pdf" => "application/pdf",
            _ => "application/octet-stream"
        };

        string[] formats = format.Split("/");
        return (formats[0], formats[1]);
    }
}
