using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs;
using BookHaven.Domain.Entities;

namespace BookHaven.Infrastructure.Services;
public class StripePaymentService : IPaymentService<Result<StripeCheckoutResponse>, Product>
{
    public Task<Result<StripeCheckoutResponse>> ProcessPayment(Product param)
    {
        throw new NotImplementedException();
    }
}

public class Product
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public long Price { get; set; }
    public int Quantity { get; set; }
}