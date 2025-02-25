using System.Net;
namespace BookHaven.Domain.Entities;
public record Error(
        string Message,
        HttpStatusCode StatusCode,
        string? Title = "");
