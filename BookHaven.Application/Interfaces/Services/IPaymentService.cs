namespace BookHaven.Application.Interfaces.Services;
public interface IPaymentService<TResult, T>
{
    Task<TResult> ProcessPayment(T param);
}