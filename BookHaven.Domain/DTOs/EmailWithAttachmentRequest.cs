using Microsoft.AspNetCore.Http;
namespace BookHaven.Domain.DTOs;
public record EmailWithAttachmentRequest(
string To,
string Subject,
string Body,
IFormFile File,
string From
);
