using FluentValidation;
using BookHaven.Domain.DTOs.Customers;

namespace BookHaven.API.Validations.Customers;
public class CustomerLoginValidator : AbstractValidator<CustomerLoginRequest>
{
    public CustomerLoginValidator()
    {
        Include(new LoginBaseRequestValidator());
    }
}