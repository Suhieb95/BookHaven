using FluentValidation;
using BookHaven.API.Helpers;
using BookHaven.Domain.DTOs.Users;

namespace BookHaven.API.Validations.Users;
public class InternalUserLoginValidator : AbstractValidator<InternalUserLoginRequest>
{
    public InternalUserLoginValidator()
    {
        RuleFor(s => s.EmailAddress).EmailAddress()
                                                          .WithMessage("A valid email is required");
        RuleFor(x => x.Password).NotEmpty()
                                                        .WithMessage("Password Is required.")
                                                        .Password();
    }
}