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
}