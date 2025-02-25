using FluentValidation;
using BookHaven.API.Helpers;
using BookHaven.Domain.DTOs.Customers;

namespace BookHaven.API.Validations.Customers;
public class CustomerLoginValidator : AbstractValidator<CustomerLoginRequest>
{
    public CustomerLoginValidator()
    {
        RuleFor(s => s.EmailAddress).EmailAddress()
                                                        .WithMessage("A valid email is required");
        RuleFor(x => x.Password).NotEmpty()
                                                        .WithMessage("Password Is required.")
                                                        .Password();
    }
}