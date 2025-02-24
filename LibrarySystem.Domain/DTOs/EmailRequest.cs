namespace LibrarySystem.Domain.DTOs;
public class EmailRequest
{
  public EmailRequest(string to, string subject, string body, string? from = null)
    => (To, Subject, Body, From) = (to, subject, body, from);
  public string? From { get; set; }
  public string To { get; }
  public string Subject { get; }
  public string Body { get; }

  public static EmailRequest ToDTO(EmailWithAttachmentRequest request)
      => new(request.To, request.Subject, request.Body, request.From);
}