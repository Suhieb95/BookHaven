using FluentValidation;
using BookHaven.API.Helpers;
using BookHaven.Domain.DTOs.BaseModels;

namespace BookHaven.API.Validations.Base;
public class LoginBaseRequestValidator : AbstractValidator<LoginBaseRequest>
{
    public LoginBaseRequestValidator()
    {
        RuleFor(s => s.EmailAddress).EmailAddress()
                                                    .WithMessage("A valid email is required");
        RuleFor(x => x.Password).NotEmpty()
                                                    .WithMessage("Password Is required.")
                                                    .Password();
    }
}