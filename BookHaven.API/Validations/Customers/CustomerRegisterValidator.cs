using FluentValidation;
using BookHaven.API.Helpers;
using BookHaven.Domain.DTOs.Customers;
namespace BookHaven.API.Validations.Customers;
public class CustomerRegisterValidator : AbstractValidator<CustomerRegisterRequest>
{
    public CustomerRegisterValidator()
    {
        Include(new RegisterBaseRequestValidator());
        RuleFor(x => x.Password).NotEmpty()
                                                        .WithMessage("Password Is required.")
                                                        .Password();
    }
}