using FluentValidation;
using LibrarySystem.API.Helpers;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs.Customers;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Specification.Customers;

namespace LibrarySystem.API.Validations.Customers;
public class CustomerRegisterValidator : AbstractValidator<CustomerRegisterRequest>
{
    public CustomerRegisterValidator()
    {

        RuleFor(s => s.EmailAddress).NotEmpty()
                                                            .WithMessage("Email address is required")
                                                            .EmailAddress()
                                                            .WithMessage("A valid email is required");
        RuleFor(x => x.Password).NotEmpty()
                                                        .WithMessage("Password Is required.")
                                                        .Password();
        RuleFor(x => x.UserName).NotEmpty()
                                                        .WithMessage("User Name Is required.")
                                                        .MinimumLength(4)
                                                        .WithMessage("User Name has to be at Least 4 characters Long."); ;
    }
}