using Microsoft.AspNetCore.Http;
namespace LibrarySystem.Domain.DTOs;
public record EmailWithAttachmentRequest(
string To,
string Subject,
string Body,
IFormFile File,
string From
);
