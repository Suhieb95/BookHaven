using BookHaven.Domain.DTOs;

namespace BookHaven.Application.Interfaces.Services;
public interface IPaymentService<TResult, T>
{
    Task<TResult> ProcessPayment(T param);
}

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