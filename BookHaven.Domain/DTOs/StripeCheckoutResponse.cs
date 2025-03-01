namespace BookHaven.Domain.DTOs;
public record StripeCheckoutResponse(int SessionId, int PublishableKey);