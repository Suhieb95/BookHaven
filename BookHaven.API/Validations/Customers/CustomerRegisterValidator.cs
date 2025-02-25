using FluentValidation;
using BookHaven.API.Helpers;
using BookHaven.Domain.DTOs.Customers;
namespace BookHaven.API.Validations.Customers;
public class CustomerRegisterValidator : AbstractValidator<CustomerRegisterRequest>
{
    public CustomerRegisterValidator()
    {

        RuleFor(s => s.EmailAddress).EmailAddress()
                                                        .WithMessage("A valid email is required");
        RuleFor(x => x.Password).NotEmpty()
                                                        .WithMessage("Password Is required.")
                                                        .Password();
        RuleFor(x => x.UserName).NotEmpty()
                                                        .WithMessage("User Name Is required.")
                                                        .MinimumLength(4)
                                                        .WithMessage("User Name has to be at Least 4 characters Long.")
                                                        .Matches(@"^\S+$").WithMessage("User Name cannot contain spaces.");

    }
}