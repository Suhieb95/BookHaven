namespace BookHaven.Domain.DTOs;
public record RefreshToken(
                            Guid Id,
                            string EmailAddress,
                            string UserName,
                            string Token,
                            string? Image
                            );
