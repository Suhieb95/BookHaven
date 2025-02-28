namespace BookHaven.Domain.Entities;
public class StripeSettings
{
    public const string SectionName = "Stripe";
    public string PublishableKey { get; set; } = default!;
    public string Secretkey { get; set; } = default!;
}