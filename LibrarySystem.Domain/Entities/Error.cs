using System.Net;
namespace LibrarySystem.Domain.Entities;
public record Error(
        string Message,
        HttpStatusCode StatusCode,
        string? Title = "");
