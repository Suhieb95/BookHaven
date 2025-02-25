using FluentValidation;
using BookHaven.API.Helpers;
using BookHaven.Domain.DTOs.Auth;
namespace BookHaven.API.Validations.Auth;
public class PasswordChangeRequestValidator : AbstractValidator<PasswordChangeRequest>
{
    public PasswordChangeRequestValidator()
    {
        RuleFor(s => s.UserId).NotEmpty()
                                                    .WithMessage("Invalid User Id.");
        RuleFor(x => x.Password).NotEmpty()
                                                    .WithMessage("Password Is required.")
                                                    .Password();
    }
}