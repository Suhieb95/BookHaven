namespace BookHaven.Domain.Entities;
public class StripeSettings
{
    public const string SectionName = "Stripe";
    public required string PublishableKey { get; init; }
    public required string Secretkey { get; init; }
}